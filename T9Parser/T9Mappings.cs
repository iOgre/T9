using System;
using System.Collections.Generic;

namespace T9Parser
{
    public class T9Mappings
    {
        private static readonly Dictionary<int, string> _digitToChar = new Dictionary<int,string>
        {
            {2,"abc"},
            {3, "def"},
            {4, "ghi"},
            {5, "jkl"},
            {6, "mno"},
            {7, "pqrs"},
            {8, "tuv"},
            {9, "wxyz"}
        };

        static T9Mappings()
        {
            //map backward
            //this mapping should occur just once, so in static ctor
            foreach (var keyValuePair in _digitToChar)
            {
                for (int i = 0; i < keyValuePair.Value.Length; i++)
                {
                    var key = keyValuePair.Value[i];
                    var keypress = new Keypress(keyValuePair.Key, i+1);
                    CharToDigits.Add(key, keypress);
                }
            }
        }

        public static Dictionary<char, Keypress> CharToDigits { get; } = new Dictionary<char, Keypress>();
    }
}