using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib;

public class CsvContentParser : IContentParser
{
    public IEnumerable<Data> Parse(string content)
    {
        return content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(line =>
        {
            var items = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return new Data(items[0], items[1]);
        });
    }
}