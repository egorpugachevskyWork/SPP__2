using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainPart.Generators.LibraryGenerators
{
    public class FloatGenerator : IValueGenerator
    {
        bool IValueGenerator.CanGenerate(Type type)
        {
            return type == typeof(float);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            return context.Random.NextSingle();
        }
    }
}
