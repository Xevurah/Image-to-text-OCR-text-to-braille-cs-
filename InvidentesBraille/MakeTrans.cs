﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvidentesBraille
{
    public class MakeTrans
    {
        private readonly Dictionary<char, char> d;
        public MakeTrans(string intab, string outab)
        {
            d = Enumerable.Range(0, intab.Length).ToDictionary(i => intab[i], i => outab[i]);
            //d = new Dictionary<char, char>();
            //for (int i = 0; i < intab.Length; i++)
            //    d[intab[i]] = outab[i];
        }
        public string Translate(string src)
        {
            System.Text.StringBuilder sb = new StringBuilder(src.Length);
            foreach (char src_c in src)
                sb.Append(d.ContainsKey(src_c) ? d[src_c] : src_c);
            return sb.ToString();
        }
    }
}
