using System;

namespace T9Parser
{
    public struct Keypress
    {
        public Keypress(int digit,int pressings, char raw = Char.MinValue)
        {
            Digit = digit;
            Pressings = pressings;
            RawChar = raw;
        }
        public int Digit { get; private set; }
        public int Pressings { get; private set; }
        public char RawChar { get; private set; }
        public override string ToString()
        {
            return $"{Digit}.{Pressings}";
        }
    }
}