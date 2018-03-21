using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using QueryBuilder.SqlGenerators;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace NovenaLibrary.Config
{
    [Serializable, XmlRoot(DataType ="WorkbookProperties", Namespace = "http://www.w3.org/2001/XMLSchema"), XmlType("WorkbookProperties")]
    public class WorkbookPropertiesConfig
    {
        private Excel.Workbook activeWorkbook;
        [XmlElement] public long Limit { get; set; }
        [XmlElement] public string CurrentSQL { get; set; }
        [XmlElement] public string SelectedTable { get; set; }
        [XmlArray("Criteria")] public List<Criteria> Criteria { get; set; }
        [XmlArray] public List<string> AdditionalQueries { get; set; }
        //public Dictionary<string, string> AdditionalQueriesDict { get; set; }
        [XmlElement] public string DrilldownSql { get; set; }
        [XmlElement] public bool RefreshColumnHeaders { get; set; }
        [XmlArray] public List<string> SelectedColumns { get; set; }
        [XmlElement] public string Type { get; set; }
        [XmlIgnore] public Query LastMainQuery { get; set; }

        //generic constructor for unit testing so that workbook object does not need to be mocked or initialized in tests
        //TODO:  is this needed?  Can the constructor below be used in tests?  Just mock up a workbook.  The workbook isn't used anywhere else anyway.
        public WorkbookPropertiesConfig()
        {
            Criteria = new List<Criteria>();
            AdditionalQueries = new List<string>();
            SelectedColumns = new List<string>();
        } 

        public WorkbookPropertiesConfig(Excel.Workbook workbook)
        {
            this.activeWorkbook = workbook;
            Criteria = new List<Criteria>();
            AdditionalQueries = new List<string>();
            SelectedColumns = new List<string>();
        }

        /// <summary>
        /// Deserializes XML and creates WorkbookPropertiesConfig object.
        /// </summary>
        /// <returns>WorkbookPropertiesConfig</returns>
        /// <exception cref="InvalidOperationException">Thrown if an error occurs when deserializing the XML string 
        /// into a WorkbookPropertiesConfig object</exception>
        public WorkbookPropertiesConfig DeserializeXML(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(WorkbookPropertiesConfig));

            var stringReader = new StringReader(xml);

            // Use the Deserialize method to restore the object's state.
            try
            {
                var bookPropertiesConfig = (WorkbookPropertiesConfig)serializer.Deserialize(stringReader);
                if (bookPropertiesConfig.Limit == 0) bookPropertiesConfig.Limit = 1000000; // 1 million.
                return bookPropertiesConfig;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            
        }

        /// <summary>
        /// Serializes the WorkbookPropertiesConfig object into XML.
        /// </summary>
        /// <returns>XML string</returns>
        public string SerializeXML()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(WorkbookPropertiesConfig));
            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }

        }

        public Dictionary<string, string> AdditionalQueriesAsDictionary()
        {
            var dict = new Dictionary<string, string>();

            foreach (var item in AdditionalQueries)
            {
                // split into key and value
                var startIndex = item.IndexOf('|') + 1;
                var key = item.Substring(0, item.IndexOf('|'));
                var value = item.Substring(startIndex, item.Length - startIndex);

                dict.Add(key, value);
            }

            return dict;
        }

        private Dictionary<string, string> ConvertListIntoDictionary(List<string> list)
        {
            var dict = new Dictionary<string, string>();

            foreach (var item in list)
            {
                // split into key and value
                var startIndex = item.IndexOf('|') + 1;
                var key = item.Substring(0, item.IndexOf('|'));
                var value = item.Substring(startIndex, item.Length - 1 - startIndex);

                dict.Add(key, value);
            }

            return dict;
        }

        private List<string> ConvertDictionaryToList(Dictionary<string, string> dict)
        {
            var list = new List<string>();

            foreach (var item in dict)
            {
                var newItem = item.Key + "|" + item.Value;
                list.Add(newItem);
            }

            // return list
            return list;
        }

    }
}
