using System;

namespace T9Parser
{
    public class T9ConversionException : InvalidCastException
    {
        public T9ConversionException(string message) : base(message)
        {
           
        }
        
        public T9ConversionException(string message, Exception inner) : base(message, inner)
        {
           
        }
    }
}