using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public class A
    {
        public B B { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        private short _tel;


        private A() { }
        public A(short tel)
        {
            _tel = tel; 
        }
    }
}
