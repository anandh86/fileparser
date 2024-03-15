using CodingAssignmentLib.Abstractions;
using System.Xml.Linq;

namespace CodingAssignmentLib
{
    public class XmlContentParser : IContentParser
    {
        public IEnumerable<Data> Parse(string content)
        {
            try
            {
                XDocument xmlDoc = XDocument.Parse(content);

                var dataList = from element in xmlDoc.Root.Elements("Data")
                               select new Data(
                                   (string)element.Element("Key"),
                                   (string)element.Element("Value"));

                return dataList;
            }
            catch (Exception ex)
            {
                return new List<Data>();
            }
        }
    }
}

