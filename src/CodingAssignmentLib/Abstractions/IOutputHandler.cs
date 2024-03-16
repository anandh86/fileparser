namespace CodingAssignmentLib.Abstractions;

public interface IOutputHandler
{
    void OutputFileContents(IEnumerable<Data> datum);
    void OutputSearchResults(IEnumerable<Data> datum, string fileNameRelativePath);
}
