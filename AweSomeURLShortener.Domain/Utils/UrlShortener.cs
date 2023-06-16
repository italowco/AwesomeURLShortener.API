using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AweSomeURLShortener.Domain.Utils
{
    public class UrlShortener
    {
        private static readonly string Alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly int Base = Alphabet.Length;

        public static string Encode(int i)
        {
            if (i == 0) return Alphabet[0].ToString();

            var s = new StringBuilder();
            while (i > 0)
            {
                s.Insert(0, Alphabet[i % Base]);
                i = i / Base;
            }

            return s.ToString();
        }

        public static int Decode(string s)
        {
            var i = 0;
            foreach (var c in s)
            {
                i = (i * Base) + Alphabet.IndexOf(c);
            }
            return i;
        }
    }

}
