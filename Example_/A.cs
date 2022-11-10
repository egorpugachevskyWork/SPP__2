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

        public int Age { get; }

        public string Name { get;  }

        private short _tel;

        public int hello;

        private string _forDll;

        public List<List<int>> Numbers { get; set; }

        private A() { }
        public A(short tel, int age, string name, List<List<int>> numbers, string forDll)
        {
            Age = age;
            Name = name;    
            _tel = tel; 
            Numbers = numbers;
            _forDll = forDll;
        }
    }
}
