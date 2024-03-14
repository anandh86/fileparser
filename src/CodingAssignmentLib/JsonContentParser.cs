using CodingAssignmentLib.Abstractions;
using Newtonsoft.Json.Linq;

namespace CodingAssignmentLib;

public class JsonContentParser : IContentParser
{
    public IEnumerable<Data> Parse(string content)
    {
        // Parse JSON string
        JArray jsonArray = JArray.Parse(content);

        List<Data> dataList = new List<Data>();
        foreach (JObject item in jsonArray)
        {
            string key = item["Key"].ToString();
            string value = item["Value"].ToString();
            dataList.Add(new Data(key, value));
        }

        return dataList;
    }
}