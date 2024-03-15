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

                var dataList = xmlDoc?.Root?.Elements("Data")
                    .Select(element =>
                    {
                        string? key = element?.Element("Key")?.Value;
                        string? value = element?.Element("Value")?.Value;

                        if (key is not null && value is not null)
                        {
                            return new Data(key, value);
                        }
                        else
                        {
                            Console.WriteLine("Missing data. Returning default value for now.");
                            return default;
                        }
                    });

                return dataList ?? new List<Data>();
            }
            catch (Exception)
            {
                return new List<Data>();
            }
        }
    }
}

