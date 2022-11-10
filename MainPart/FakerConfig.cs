using MainPart.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MainPart
{
    public class FakerConfig
    {
        private Dictionary<Type, Dictionary<string, IUserGenerator>> _userGenerators;
    
        public void Add<TypeName, FieldType, Generator>(Expression<Func<TypeName, FieldType>> lambda)
        {
            if (!_userGenerators.ContainsKey(typeof(TypeName)))
            {
                _userGenerators.TryAdd(typeof(TypeName), new Dictionary<string, IUserGenerator>());
            }

            var member = lambda.Body as MemberExpression;
            if (member != null)
            {   
                var memberName = member.Member.Name;
                var generator = (IUserGenerator)Activator.CreateInstance(typeof(Generator));
                _userGenerators[typeof(TypeName)].TryAdd(memberName, generator);
            }
        }
  
        public bool CheckGenerator(Type type, string name)
        {
            return _userGenerators.ContainsKey(type) && _userGenerators[type].ContainsKey(name);
        }

        public IUserGenerator ObtaionGenerator(Type type, string name)
        {
            return CheckGenerator(type, name) ? _userGenerators[type][name] : null;   
        }

        public FakerConfig()
        {
            _userGenerators = new Dictionary<Type, Dictionary<string, IUserGenerator>>();
        }
    }
}
