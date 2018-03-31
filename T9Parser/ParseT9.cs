using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using log4net;

namespace T9Parser
{
    
    public  class ParseT9
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static ILog Log
        {
            get { return _log; }
        }

        private ConversionSettings? _cs = null;

        public ParseT9()
        {
            
        }
        public ParseT9(ConversionSettings cs)
        {
            _cs = cs;
        }
        public  string Parse(string sentence, ConversionSettings? conversionSettings = null)
        {
            var cs = conversionSettings ?? _cs ?? ConversionSettings.Default;
            if (cs.HasFlag(ConversionSettings.LowercaseAll))
            {
                sentence = sentence.ToLower();
            }
            string result = null;
           
                var charsConverted = sentence.ToCharArray().AsParallel().AsOrdered()
                    .Select(t => ParseCharToKeypress(t, cs));
                result = CreateString(charsConverted);
           
            
            return result;
        }

        private  string CreateString(IEnumerable<Keypress> keypr)
        {
            var sb = new StringBuilder();
            int? previous = null;
            foreach (var keypress in keypr)
            {
                if (keypress.Digit != -1)
                {
                    var pressings = Enumerable.Repeat(keypress.Digit, keypress.Pressings);
                    var current = string.Join("", pressings);
                    if (keypress.Digit == previous)
                    {
                        sb.Append(" ");
                    }

                    sb.Append(current);
                    previous = keypress.Digit;
                }
                else
                {
                    var current = keypress.RawChar;
                    sb.Append(current);
                }
            }

            return sb.ToString();
        }


        private  Keypress ParseCharToKeypress(char input, ConversionSettings cs = ConversionSettings.Default)
        {
            //ConversionSettings cs = ConversionSettings.Default;
            if (T9Mappings.CharToDigits.TryGetValue(input, out var keypress))
            {
                return keypress;
            }
            else
            {
                if (cs.HasFlag(ConversionSettings.FailOnUnconvertable))
                {
                    var message = $"Cannot convert character {input} to T9";
                    Log.Warn(message);
                    throw new T9ConversionException(message);
                }
                else
                {
                    Log.Warn($"Unsuccesful attempt to convert {input} into T9 sequence");
                }
                keypress = new Keypress(-1, -1, input);
                return keypress;
            }

        }
    }
}