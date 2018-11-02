using System;
using System.IO;

namespace DocumentMerger2
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("DocumentMerger2 <input_file_1> <input_file_2> ... <input_file_n> <output_file>");
                Console.WriteLine("Supply a list of text files to merge followed by the name of the resulting merged text file as command line arguments.");
                Console.WriteLine("A minimum of two files to merge and and an output file must be provided.");
                System.Environment.Exit(1);
            }

            string[] inputFilePaths = new string[args.Length - 1];
            Array.Copy(args, 0, inputFilePaths, 0, args.Length - 1);

            var outputFilePath = args[args.Length - 1];
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(outputFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error opening to write to {0}: {1}", outputFilePath, e.Message);
                System.Environment.Exit(2);
            }

            ulong characterCount = 0;
            StreamReader reader = null;
            string currentInputFilePath = null;

            try
            {
                foreach (string inputFilePath in inputFilePaths)
                {
                    currentInputFilePath = inputFilePath;
                    reader = new StreamReader(currentInputFilePath);
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        writer.WriteLine(line);
                        characterCount = characterCount + (ulong)line.Length;
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while processing {0}: {1}", currentInputFilePath, e.Message);
                System.Environment.Exit(2);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (writer != null)
                {
                    writer.Close();
                }
            }

            Console.WriteLine("{0} was successfully saved. The document contains {1} characters", outputFilePath, characterCount);
        }
    }
}
