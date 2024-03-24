using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib;

public class FileProcessService
{
    private readonly IFileUtility _fileUtility;
    private readonly IOutputHandler _outputHandler;
    private readonly string rootSearchDirectory = "data";

    public FileProcessService(IFileUtility fileUtility, IOutputHandler outputHandler)
    {
        _fileUtility = fileUtility;
        _outputHandler = outputHandler;
    }

    public void DisplayFileContents(string fileName)
    {
        // 1. File handling
        string fileExtension = _fileUtility.GetExtension(fileName);

        if (string.IsNullOrEmpty(fileExtension))
        {
            // Early return for invalid conditions
            Console.WriteLine("Invalid file type");
            return;
        }

        // 2. Parsing
        // Factory creation for parsers
        IContentParser? contentParser = ContentParserFactory.CreateContentParser(fileExtension);
        if (contentParser is null)
        {
            Console.WriteLine("Unsupported file extension");
            return;
        }

        IEnumerable<Data> dataList = contentParser.Parse(_fileUtility.GetContent(fileName));

        // 3. Display contents
        _outputHandler.OutputFileContents(dataList);
    }

    public void SearchForKey(string key)
    {
        // File handling and looping
        string[] files = _fileUtility.GetFilesInDirectory(rootSearchDirectory);

        foreach (string fileName in files)
        {
            HandleSingleFile(key, fileName);
        }
    }

    private void HandleSingleFile(string key, string fileName)
    {
        // 1. File handling
        string fileExtension = _fileUtility.GetExtension(fileName);

        if (string.IsNullOrEmpty(fileExtension))
            return;

        // 2. Parsing
        // Factory creation for parsers
        IContentParser? contentParser = ContentParserFactory.CreateContentParser(fileExtension);
        if (contentParser is null)
            return;

        IEnumerable<Data> dataList = contentParser.Parse(_fileUtility.GetContent(fileName));

        // 3. Searching
        var retList = SearchDataList(key, dataList);

        if (retList.Count() == 0)
            return;

        // 4. Displaying
        string relativePath = _fileUtility.GetRelativePath(fileName);
        _outputHandler.OutputSearchResults(retList, relativePath);
    }

    private IEnumerable<Data> SearchDataList(string searchKey, IEnumerable<Data> dataList)
    {
        var retList = new List<Data>();

        foreach (Data data in dataList)
        {
            // Check if searchKey is present in the value (case-insensitive)
            if(data.Key.Equals(searchKey, StringComparison.OrdinalIgnoreCase))
            {
                retList.Add(data);
            }
        }

        return retList;
    }
}