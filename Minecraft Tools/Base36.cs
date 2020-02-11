using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Tools
{
    public class Base36
    {
        private const string Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private const ulong Base = 36uL;

        public static string Encode(ulong value)
        {
            string result = "";
            if (value == 0uL)
            {
                return "0";
            }
            while (value != 0uL)
            {
                int digit = (int)(value % 36uL);
                value /= 36uL;
                result = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[digit].ToString() + result;
            }
            return result;
        }

        public static ulong Decode(string value)
        {
            value = value.ToUpper();
            ulong result = 0uL;
            string text = value;
            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];
                result *= 36uL;
                int digit = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(ch);
                if (digit == -1)
                {
                    throw new FormatException(value);
                }
                result += (ulong)((long)digit);
            }
            return result;
        }
    }
}
