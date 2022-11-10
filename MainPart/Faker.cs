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
        //2. Try to generate value from this generators
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

        public Faker()
        {
            GetLibraryGenerators();
            _context = new GeneratorContext(this, new Random());
        }
    }
}
