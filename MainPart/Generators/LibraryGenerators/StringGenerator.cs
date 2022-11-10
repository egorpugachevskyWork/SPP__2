using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainPart.Generators.LibraryGenerators
{
    public class StringGenerator : IValueGenerator
    {
        bool IValueGenerator.CanGenerate(Type type)
        {
            return type == typeof(string);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            var stringLength = (byte)context.Random.Next(byte.MinValue, byte.MaxValue);
            var strBuilder = new StringBuilder(stringLength);
            for (int i = 0; i < stringLength; i++)
            {
                strBuilder.Append((char)(context.Random.Next(byte.MinValue, byte.MaxValue) % 40 + '0'));
            }

            return strBuilder.ToString();
        }
    }
}
