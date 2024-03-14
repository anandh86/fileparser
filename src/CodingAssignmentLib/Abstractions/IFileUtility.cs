namespace CodingAssignmentLib.Abstractions;

public interface IFileUtility
{
    string GetExtension(string fileName);
    string GetContent(string fileName);
}