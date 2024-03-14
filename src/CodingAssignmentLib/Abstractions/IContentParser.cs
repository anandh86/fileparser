namespace CodingAssignmentLib.Abstractions;

public interface IContentParser
{
    IEnumerable<Data> Parse(string content);
}