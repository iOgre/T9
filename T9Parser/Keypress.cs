namespace T9Parser
{
    public struct Keypress
    {
        public Keypress(int digit,int pressings)
        {
            Digit = digit;
            Pressings = pressings;
        }
        public int Digit { get; private set; }
        public int Pressings { get; private set; }
    }
}