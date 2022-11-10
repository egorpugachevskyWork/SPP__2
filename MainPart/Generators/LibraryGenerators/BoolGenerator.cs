using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainPart.Generators.LibraryGenerators
{
    public class BoolGenerator : IValueGenerator
    {
        bool IValueGenerator.CanGenerate(Type type)
        {
            return type == typeof(bool);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            return context.Random.Next(short.MinValue, short.MaxValue) > (short)(0.8 * short.MaxValue);
        }
    }
}
