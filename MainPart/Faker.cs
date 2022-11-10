]using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainPart
{
    public class Faker
    {
        private GeneratorContext _context;
        public T Create<T>()
        {
            return (T) Create(typeof(T));
        }

        public object Create(Type t)
        {
            //TO DO
            //1. Load all generators from current Assembly
            //2. Try to generate value from this generators
            //3. Try to generate not dto with recursion
            //3_a. Forget about loading methods from other dlls
            //3a. First with user generators
            //3b. Second with library generators

            return new object();
        }

        public Faker()
        {
            _context = new GeneratorContext(this, new Random());
        }
    }
}
