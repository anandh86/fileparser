namespace CodingAssignmentLib.Abstractions;

public interface ISearchService
{
    IEnumerable<Data>? SearchForKeyInFolder(string folderPath, string key);
}