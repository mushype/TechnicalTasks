using System;
using System.IO;

namespace CSVReaderTask
{
    class Program
    {
        /// <summary>
        /// This console application is run with three parameters.
        /// example
        /// >CSVReaderTask.exe C:\Users\gooch\source\repos\CSVReaderTask\CSVReaderTask\data\csvData.csv c:\temp\output.txt  xml
        /// </summary>
        /// <param name="args">Source - path to csv file</param>
        /// <param name="args">Destination - file to be output.</param>
        /// <param name="args">Output Format - Either 'json' or 'xml'</param>
        static void Main(string[] args)
        {
            if (args.Length < 3)
                ShowHelpText();

            try
            {
                //Get Parameters from Arguments passed in
                var parameters = new Parameters(args);

                //Convert to XML and JSON
                var inputObject = new InputObject(parameters.Source);
                var json = inputObject.GetJson();
                var xml = inputObject.GetXml();

                //Output file to destination (as per requested format)
                WriteOutputFile(parameters.Destination, parameters.Format.ToLower() == "json" ? json : xml );

                //Convert JSON to XML
                var newXML = Helper.ConvertJsonToXml(json);

                //Convert XML to JSON
                var newJSON = Helper.ConvertXmlToJson(xml);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private static void ShowHelpText()
        {
            Console.WriteLine("You must specify three parameters");
            Console.WriteLine("Source File - Path to csv file to be read");
            Console.WriteLine("Destination File - Formatted file that is output");
            Console.WriteLine("Format - format of file to be output ('json' or 'xml'");
            Environment.Exit(1);
        }

        private static void WriteOutputFile(string destination, string contents)
        {
            try
            {
                File.WriteAllText(destination, contents);
            }
            catch (Exception)
            {
                throw new Exception("There was a problem writing the output file");
            }
        }
    }
}
