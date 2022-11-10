using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainPart.Generators.UserGenerators
{
    public class AgeGenerator : IUserGenerator
    {
        bool IUserGenerator.CanGenerate(Type type)
        {
            return type == typeof(int);
        }

        object IUserGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            return context.Random.Next(20, 100);
        }
    }
}
