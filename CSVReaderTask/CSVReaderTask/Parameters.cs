using System;

namespace CSVReaderTask
{
    public class Parameters
    {
        /// <summary>
        /// CSV source file path
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Output file destination path
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Format of output file (xml or json)
        /// </summary>
        public string Format { get; set; }

        public Parameters(string[] arguments)
        {
            Source = arguments[0];
            Destination = arguments[1];
            Format = arguments[2];

            if (Format.ToLower() != "json" && Format.ToLower() != "xml")
            {
                throw new Exception("Invalid file type specified");
            }
        }
    }
}
