using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainPart
{
    public class GeneratorContext
    {
        public Random Random { get; }

        public Faker Faker { get; }

        public GeneratorContext(Faker faker, Random random)
        {
            Random = random;
            Faker = faker;
        }
    }
}
