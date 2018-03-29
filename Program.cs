using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using T9Parser;

namespace T9.Terminal
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"h is {ParseT9.Parse("h")}");
            
        }

        
        
    }

    
}