using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteLib1
{
    public class StringGenerator
    {
        public static bool CanGenerate(Type type)
        {
            return type == typeof(string);
        }

        public static string Generate(Type typeToGenerate)
        {
            var stringLength = (byte)new Random().Next(byte.MinValue, byte.MaxValue);
            var strBuilder = new StringBuilder(stringLength);
            for (int i = 0; i < stringLength; i++)
            {
                strBuilder.Append('X');
            }

            return strBuilder.ToString();
        }
    }
}
