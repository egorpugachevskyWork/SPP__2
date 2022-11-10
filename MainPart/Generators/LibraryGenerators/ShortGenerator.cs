using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainPart.Generators.LibraryGenerators
{
    public class ShortGenerator : IValueGenerator
    {
        bool IValueGenerator.CanGenerate(Type type)
        {
            return type == typeof(short);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            return (short)context.Random.Next(short.MinValue, short.MaxValue);
        }
    }
}
