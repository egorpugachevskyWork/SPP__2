using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainPart.Generators.UserGenerators
{
    public class NameGenerator : IUserGenerator
    {
        public string[] _names = new string[] { "Egor", "Maria", "Danila", "Robbin", "Halacost" };

        bool IUserGenerator.CanGenerate(Type type)
        {
            return type == typeof(string);
        }

        object IUserGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            return _names[context.Random.Next(0, _names.Length)];
        }
    }
}
