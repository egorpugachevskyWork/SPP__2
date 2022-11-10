using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainPart.Generators.LibraryGenerators
{
    public class ListGenerator : IValueGenerator
    {
        bool IValueGenerator.CanGenerate(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            var length = context.Random.Next(1, 15);
            var genericArgs = typeToGenerate.GenericTypeArguments;
            var listType = typeof(List<>).MakeGenericType(genericArgs);
            var resultList = (IList)Activator.CreateInstance(listType);

            for (int i = 0; i< length; i++)
            {
                resultList.Add(context.Faker.Create(genericArgs[0]));
            }

            return resultList;
        }
    }
}
