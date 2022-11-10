using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainPart.Generators.LibraryGenerators
{
    public class DoubleGenerator : IValueGenerator
    {
        bool IValueGenerator.CanGenerate(Type type)
        {
            return type == typeof(double);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            return context.Random.NextDouble();
        }
    }
}
