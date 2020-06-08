using System.Xml;
using Newtonsoft.Json;

namespace CSVReaderTask
{
    public class Helper
    {
        /// <summary>
        /// Converts XML string to JSON
        /// </summary>
        /// <param name="xml">string of XML</param>
        /// <returns></returns>
        public static string ConvertXmlToJson(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return JsonConvert.SerializeXmlNode(doc); 
        }

        /// <summary>
        /// Converts JSON string to XML
        /// </summary>
        /// <param name="json">string of JSON</param>
        /// <returns></returns>
        public static string ConvertJsonToXml(string json)
        {
            var xml = JsonConvert.DeserializeXmlNode("{\"root\":" + json + "}", "root");
            return xml.InnerXml;
        }
    }
}
