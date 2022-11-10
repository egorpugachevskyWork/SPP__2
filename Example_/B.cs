using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public class B
    {
        private uint _nope;

        public int hello;

        public string Name { get; set; }

        public C C { get; set; }

        public B() { }
        private B(uint nope)
        {
            _nope = nope;   
        }
    }
}
