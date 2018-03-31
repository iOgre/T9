using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using T9Parser;

namespace T9.Terminal
{
    internal class Program
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static void Help()
        {
            Console.WriteLine();
            Console.WriteLine("Converts string lines into T9 representation");
            Console.WriteLine("T9.Terminal.exe -i input file path -o output file path [/overwrite]");
            Console.WriteLine("If /overwrite is used, output file will be rewritten");
        }
        
        public static async Task Main(string[] args)
        {
            if (args == null)
            {
                Help();
                return;
            }

           
            var inputFileIndex = Array.IndexOf(args, "-i")+1;
            var outputFileIndex = Array.IndexOf(args, "-o") + 1;
            if (inputFileIndex == 0 || outputFileIndex == 0)
            {
                Help();
                return;
            }
            var inputFile = new FileInfo(args[inputFileIndex]);
            if (!inputFile.Exists)
            {
                Console.WriteLine($"Input file not found on path {inputFile.FullName}");
                return;

            }
            var outputFile = new FileInfo(args[outputFileIndex]);
            bool overwrite = Array.IndexOf(args, "/overwrite") != -1;
            if (outputFile.Exists && !overwrite)
            {
                Console.WriteLine($"Output file already exists on path {outputFile.FullName}");
                Console.WriteLine("Use /overwrite switch if you want to overwrite existing file");
                return;
            }
            ConversionSettings cs = ConversionSettings.Default;
            if (args.Any(t => t == "/lowercase"))
            {
                cs = cs | ConversionSettings.LowercaseAll;
            }
            
            if (args.Any(t => t == "/strict"))
            {
                cs = cs | ConversionSettings.FailOnUnconvertable;
            }
            var parser = new ParseT9(cs);
            bool success = true;
            try
            {
                using (var stream = inputFile.OpenText())
                {
                    Console.WriteLine($"Conversion started at {DateTime.Now.ToLongTimeString()}");
                    using (var writer = new StreamWriter(outputFile.FullName))
                    {
                        string currentString;
                        int step = 0;
                        while ((currentString = await stream.ReadLineAsync()) != null)
                        {
                            // first line (step == 0) is number of test cases
                            if (step > 0)
                            {
                                var parsed = parser.Parse(currentString);
                                var output = $"Case #{step}: {parsed}";

                                await writer.WriteLineAsync(output);
                            }

                            step++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                _log.Error($"Exception occured during conversion");
                _log.Error($"{ex.Message}");
                _log.Error($"{ex.StackTrace}");
            }
            finally
            {
                if (success)
                {
                    Console.WriteLine($"Conversion ended at {DateTime.Now.ToLongTimeString()}");
                }
                else
                {
                    Console.WriteLine($"Conversion failed at {DateTime.Now.ToLongTimeString()}");
                    Console.WriteLine("Check logfile for detailed information");
                }
            }
           
        }
    }
}