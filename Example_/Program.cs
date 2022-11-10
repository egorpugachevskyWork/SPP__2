using MainPart;
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
            var faker = new Faker();
            var a = faker.Create<A>();

            Console.WriteLine();
        }
    }
}
