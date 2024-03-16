namespace CodingAssignmentLib.Abstractions;

// Interface defining the contract for displaying the output result
// Note : This can be used to extend to other formats like printing
// the result to a file system, console etc.
public interface IOutputHandler
{
    void OutputFileContents(IEnumerable<Data> datum);
    void OutputSearchResults(IEnumerable<Data> datum, string fileNameRelativePath);
}
