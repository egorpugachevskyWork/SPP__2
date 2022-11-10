using MainPart;
using MainPart.Generators.UserGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var fakerConfig = new FakerConfig();
            fakerConfig.Add<A, string, NameGenerator>(a => a.Name);
            fakerConfig.Add<A, int, AgeGenerator>(a => a.Age);
            fakerConfig.Add<B, string, NameGenerator>(b => b.Name);
            fakerConfig.Add<B, int, AgeGenerator>(b => b.hello);

            var faker = new Faker(fakerConfig);
            var a = faker.Create<A>();

            Console.WriteLine();
        }
    }
}
