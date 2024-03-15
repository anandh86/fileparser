using CodingAssignmentLib.Abstractions;
using Newtonsoft.Json.Linq;

namespace CodingAssignmentLib;

public class JsonContentParser : IContentParser
{
    public IEnumerable<Data> Parse(string content)
    {
        List<Data> dataList = new List<Data>();

        try
        {
            // Parse JSON string
            JArray jsonArray = JArray.Parse(content);

            foreach (JObject item in jsonArray)
            {
                if (item is not null)
                {
                    var key = item["Key"]?.ToString();
                    var value = item["Value"]?.ToString();

                    dataList.Add(new Data(key ?? String.Empty, value ?? String.Empty));
                }
            }

            return dataList;
        }
        catch (Exception)
        {
            return dataList;
        }
    }
}