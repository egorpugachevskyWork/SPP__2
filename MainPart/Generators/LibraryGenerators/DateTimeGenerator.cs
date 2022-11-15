using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainPart.Generators.LibraryGenerators
{
    public class DateTimeGenerator : IValueGenerator
    {
        bool IValueGenerator.CanGenerate(Type type)
        {
            return type == typeof(DateTime);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            var amountOfDays = (short)context.Random.Next(1, short.MaxValue);
            var resultDate = new DateTime(1990, 1, 1);
            return resultDate.AddDays(amountOfDays);
        }
    }
}
