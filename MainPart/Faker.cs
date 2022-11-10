using MainPart.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MainPart
{
    public class Faker
    {
        private GeneratorContext _context;
        
        private List<Type> _types;

        private List<IValueGenerator> _generators;
        public T Create<T>()
        {
            return (T) Create(typeof(T));
        }


        //TO DO
        //1. Load all generators from current Assembly +
        //2. Try to generate value from this generators+ and try to generate from user generator and from dll methods
        //3. Try to generate not dto with recursion
        //3_a. Forget about loading methods from other dlls
        //3a. First with user generators
        //3b. Second with library generators
        public object Create(Type t)
        {
            object obj = _generators.Find(g => g.CanGenerate(t))?.Generate(t);

            if ((obj == null || obj.Equals(GetDefaultValue(t)) && !t.IsPrimitive){
                //Trying to fill not DTO type
                _types.Add(t);
                //Fill class
                _types.Remove(t);
            }
            

            return new object();
        }

        private void GetLibraryGenerators()
        {
            _generators = Assembly.GetExecutingAssembly().GetTypes()
                    .Where(type => type.GetInterfaces().Contains(typeof(IValueGenerator)))
                    .Select(type => (IValueGenerator)Activator.CreateInstance(type)).ToList();
        }

        private object GetDefaultValue(Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }

        //find constructors and create object+
        //fill fields
        //fill properties
        private object CreateClass(Type t)
        {
            object obj = CreateInstanceOfClass(t);
            
        }

        private object CreateInstanceOfClass(Type t)
        {
            var infoContructors = t.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            Array.Sort(infoContructors, (c1, c2) =>
            {
                if (c1.GetParameters().Length > c2.GetParameters().Length)
                    return -1;
                else if (c1.GetParameters().Length > c2.GetParameters().Length)
                    return 1;
                return 0;
            });

            object obj = null;
            foreach (var info in infoContructors)
            {
                object[] parametrs = GetParametrs(info);
                obj = info.Invoke(parametrs);
                if (obj != null)
                    break;
            }

            return obj;
        }

        private object[] GetParametrs(ConstructorInfo info)
        {
            if (info.GetParameters().Length == 0)
                return null;

            object[] parametrs = new object[info.GetParameters().Length];
            int i = 0;
            foreach(var param in info.GetParameters())
            {
                //user generator first
                //generator second +
                //dll generator
                var foundLibGen = _generators.Find(g => g.CanGenerate(param.ParameterType));
                parametrs[i] = foundLibGen?.Generate(param.ParameterType, _context) ?? Create(param.ParameterType);
                i++;
            }

            return parametrs;
        }

        public Faker()
        {
            GetLibraryGenerators();
            _context = new GeneratorContext(this, new Random());
        }
    }
}
