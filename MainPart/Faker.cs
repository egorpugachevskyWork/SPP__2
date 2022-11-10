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

        private FakerConfig _config;

        private string _nameDll = "RemoteLib1.dll";

        private string[] _namesClass = new string[] { "RemoteLib1.IntGenerator", "RemoteLib1.StringGenerator" };


        public T Create<T>()
        {
            return (T) Create(typeof(T));
        }

        public object Create(Type t)
        {
            object obj = null;

            obj =TryRemoteLibraryGenerators(t);

            obj = obj ?? _generators.Find(g => g.CanGenerate(t))?.Generate(t, _context);

            if ((obj == null || obj.Equals(GetDefaultValue(t))) && !t.IsPrimitive && !isTypeGenerated(t)){
                _types.Add(t);
                obj = CreateClass(t);
                _types.Remove(t);
            }
            

            return obj ?? GetDefaultValue(t);
        }

        private object TryRemoteLibraryGenerators(Type appropriateType)
        {
            var asm = Assembly.LoadFrom(_nameDll);
            var types = asm.GetTypes();
            object obj = null;
            foreach (var type in types)
            {
                if (_namesClass.Contains(type.FullName))
                {
                    var method = type.GetMethod("CanGenerate", BindingFlags.Public  | BindingFlags.Static);
                    if (method is not null && (bool?)method.Invoke(null, new object[] { appropriateType}) == true)
                    {
                        var generateMethod = type.GetMethod("Generate", BindingFlags.Public | BindingFlags.Static);
                        if(generateMethod is not null)
                        {
                            obj = generateMethod.Invoke(null, new object[] { appropriateType });
                            break;
                        }
                    }
                    
                }
            }

            return obj;

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

        private bool isTypeGenerated(Type t)
        {
            return _types.Contains(t);
        }
        private object CreateClass(Type t)
        {
            object obj = CreateInstanceOfClass(t);
            FillFields(obj, t);
            FillUserFields(obj, t);
            FillProperties(obj, t);
            FillUserProperties(obj , t);

            return obj;
        }


        private void FillFields(object obj, Type t)
        {
            var fields = t.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
            {
                var fieldValue = field.GetValue(obj);
                if (fieldValue == null || fieldValue.Equals(GetDefaultValue(field.FieldType)))
                {
                    field.SetValue(obj, Create(field.FieldType));
                }
            }
        }

        private void FillUserFields(object obj, Type t)
        {
            var fields = t.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
            {

                if (_config.CheckGenerator(t, field.Name))
                {
                    var foundUserGen = _config.ObtaionGenerator(t, field.Name);
                    field.SetValue(obj, foundUserGen.Generate(field.FieldType, _context));
                }
            }
        }

        private void FillProperties(object obj, Type t)
        {
            var properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach(var property in properties)
            {
                var propertyValue = property.GetValue(obj);
                if (property.SetMethod is not null && (propertyValue == null || propertyValue.Equals(GetDefaultValue(property.PropertyType)))){
                    property.SetMethod.Invoke(obj, new object[] { Create(property.PropertyType) });
                }
            }
        }
        private void FillUserProperties(object obj, Type t)
        {
            var properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach(var property in properties)
            {

                if (property.SetMethod is not null && _config.CheckGenerator(t, property.Name))
                {
                    var foundUserGen = _config.ObtaionGenerator(t, property.Name);
                    property.SetMethod.Invoke(obj, new object[] { foundUserGen.Generate(property.PropertyType, _context) });
                }
            }
        }
        private object CreateInstanceOfClass(Type t)
        {
            var infoContructors = t.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            Array.Sort(infoContructors, (c1, c2) =>
            {
                if (c1.GetParameters().Length > c2.GetParameters().Length)
                    return -1;
                else if (c1.GetParameters().Length < c2.GetParameters().Length)
                    return 1;
                return 0;
            });

            object obj = null;
            foreach (var info in infoContructors)
            {
                object[] parametrs = GetParametrs(info, t);
                obj = info.Invoke(parametrs);
                if (obj != null)
                    break;
            }

            return obj;
        }

        private object[] GetParametrs(ConstructorInfo info, Type classType)
        {
            if (info.GetParameters().Length == 0)
                return null;

            object[] parametrs = new object[info.GetParameters().Length];
            int i = 0;
            foreach(var param in info.GetParameters())
            {
                var paramName = ""; 
                var field = classType.GetField(param.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                var property = classType.GetProperty(param.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if ((field != null && field.FieldType == param.ParameterType) ||
                    (property != null && property.PropertyType == param.ParameterType))
                {
                    paramName = (field != null) ? field.Name : property.Name;
                }

                if (_config.CheckGenerator(classType, paramName))
                {
                    var foundUserGen = _config.ObtaionGenerator(classType, paramName);
                    parametrs[i] = foundUserGen.Generate(param.ParameterType, _context);
                }
                else 
                { 
                    parametrs[i] = Create(param.ParameterType);
                    
                }
                i++;

            }

            return parametrs;
        }

        

        public Faker()
        {
            GetLibraryGenerators();
            _types = new List<Type>();
            _context = new GeneratorContext(this, new Random());
        }

        public Faker(FakerConfig config) : this()
        {
            _config = config;
        }
    }
}
