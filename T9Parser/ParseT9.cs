using System.Linq;
using System.Text;

namespace T9Parser
{
    public static class ParseT9
    {
        public static string Parse(string input)
        {
            StringBuilder sb = new StringBuilder();
            int previous = -1;
            for (var i = 0; i < input.Length; i++)
            {
                var character = input[i];
                var keypress = T9Mappings.CharToDigits[character];
                var pressings = Enumerable.Repeat(keypress.Digit, keypress.Pressings);
                var current = string.Join("", pressings);
                if (keypress.Digit == previous)
                {
                    sb.Append(" ");
                }
                sb.Append(current);
                previous = keypress.Digit;
            }

            return sb.ToString();
        }
    }
}