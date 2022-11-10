using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainPart.Generators.LibraryGenerators
{
    public class IntegerGenerator : IValueGenerator
    {
        bool IValueGenerator.CanGenerate(Type type)
        {
            return type == typeof(int);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            return context.Random.Next(int.MinValue, int.MaxValue);
        }
    }
}
