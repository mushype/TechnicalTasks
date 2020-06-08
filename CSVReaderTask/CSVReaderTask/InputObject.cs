using CsvHelper;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CSVReaderTask
{
    public class InputObject
    {
        private DataTable _input;

        public InputObject(string filename)
        {
            _input = ReadCSVFile(filename);
        }

        /// <summary>
        /// Gets the JSON string for the input file
        /// </summary>
        /// <returns></returns>
        public string GetJson()
        {
            var jArray = new JArray();
            foreach (DataRow inputRow in _input.Rows)
            {
                var jObject = new JObject();
                for (int i = 0; i < _input.Columns.Count; i++)
                {
                    var name = _input.Columns[i].ColumnName;
                    var value = inputRow.ItemArray[i];
                    var jProperty = addJProperty(name, value, jObject);
                    if (jProperty!=null)
                        jObject.Add(addJProperty(name, value, jObject));
                    

                }
                jArray.Add(jObject);
            }
            
            return JsonConvert.SerializeObject(jArray, Formatting.Indented);
        }

        /// <summary>
        /// Gets the XML string for the input file
        /// </summary>
        /// <returns></returns>
        public string GetXml()
        {
            StringWriter stringWriter = new StringWriter();
            _input.WriteXml(stringWriter);
            return stringWriter.ToString();
        }

        private DataTable ReadCSVFile(string filename)
        {
            try
            {
                using (var reader = new StreamReader(filename))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    using (var dr = new CsvDataReader(csv))
                    {
                        var dt = new DataTable();
                        dt.TableName = "csvDataTable";
                        dt.Load(dr);
                        return dt;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("There is an issue reading the input (CSV) file");
                throw;
            }

        }

        private JProperty addJProperty(string name, object value, JObject thisobject)
        {
            if (name.Contains("_"))
            {
                var charIndex = name.IndexOf("_");
                var initialName = name.Substring(0, charIndex);
                var endName = name.Substring(charIndex + 1);
                var Jobject = new JObject();
                
                //Does parent already exist?
                JObject existing = thisobject[initialName] as JObject;
                if (existing != null)
                {
                    existing.Add(endName, value.ToString());
                    return null;
                }
                else
                {
                    Jobject.Add(addJProperty(endName, value, Jobject));
                }

                return new JProperty(initialName, Jobject);
            }
            return new JProperty(name, value);
        }
    }
}
