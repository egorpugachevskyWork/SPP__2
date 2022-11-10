using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainPart.Generators.LibraryGenerators
{
    public class LongGenerator : IValueGenerator
    {
        bool IValueGenerator.CanGenerate(Type type)
        {
            return type == typeof(long);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            return context.Random.NextInt64(long.MinValue, long.MaxValue);
        }
    }
}
