using System.IO.Abstractions;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib;

// TODO: Maybe remove this and combine with FileProcessService?
public class SearchService : ISearchService
{
    private readonly IFileUtility _fileUtility;
    private readonly string rootSearchDirectory = "data";

    public SearchService(IFileUtility fileUtility)
    {
        _fileUtility = fileUtility;
    }

    public IEnumerable<Data>? SearchForKeyInFolder(string folderPath, string key)
    {
        if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(key))
            return null;

        var files = _fileUtility.GetFilesInDirectory(rootSearchDirectory);

        foreach (var file in files)
        {
            var fileExtension = _fileUtility.GetExtension(file);

            try
            {
                // Factory creation for parsers
                IContentParser? contentParser = ContentParserFactory.CreateContentParser(fileExtension);
                var dataList = contentParser?.Parse(_fileUtility.GetContent(file));

                if (dataList is not null)
                {
                    // Get the relative path of the file
                    string relativePath = _fileUtility.GetRelativePath(file);

                    var retVal = Search(key, dataList);

                    if(retVal is not null)
                    {
                        foreach (Data data in retVal)
                        {
                            Console.WriteLine($"Key:{data.Key} Value:{data.Value} FileName:{relativePath}");
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid file extension");
            }
        }

        return null;
    }

    private IEnumerable<Data> Search(string searchKey, IEnumerable<Data> dataList)
    {
        var retList = new List<Data>();

        if (dataList != null)
        {
            foreach (Data data in dataList)
            {
                // Check if searchKey is present in the value (case-insensitive)
                if (data.Key.IndexOf(searchKey, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    retList.Add(data);
                }
            }
        }

        return retList;
    }
}