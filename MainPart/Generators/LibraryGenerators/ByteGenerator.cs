using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainPart.Generators.LibraryGenerators
{
    public class ByteGenerator : IValueGenerator
    {
        bool IValueGenerator.CanGenerate(Type type)
        {
            return type == typeof(byte);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            return (byte)context.Random.Next(byte.MinValue, byte.MaxValue);
        }
    }
}
