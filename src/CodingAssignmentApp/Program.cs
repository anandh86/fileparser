// See https://aka.ms/new-console-template for more information

using System.IO.Abstractions;
using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;

Console.WriteLine("Coding Assignment!");

do
{
    Console.WriteLine("\n---------------------------------------\n");
    Console.WriteLine("Choose an option from the following list:");
    Console.WriteLine("\t1 - Display");
    Console.WriteLine("\t2 - Search");
    Console.WriteLine("\t3 - Exit");

    switch (Console.ReadLine())
    {
        case "1":
            Display();
            break;
        case "2":
            Search();
            break;
        case "3":
            return;
        default:
            return;
    }
} while (true);


void Display()
{
    Console.WriteLine("Enter the name of the file to display its content:");

    var fileName = Console.ReadLine()!;

    var dataList = GetDataList(fileName);

    Console.WriteLine("Data:");

    foreach (var data in dataList)
    {
        Console.WriteLine($"Key:{data.Key} Value:{data.Value}");
    }
}

IEnumerable<Data>? GetDataList(string fileName)
{
    var fileUtility = new FileUtility(new FileSystem());
    var dataList = Enumerable.Empty<Data>();

    var fileExtension = fileUtility.GetExtension(fileName);

    switch (fileExtension)
    {
        case ".csv":
            dataList = new CsvContentParser().Parse(fileUtility.GetContent(fileName));
            break;

        case ".json":
            dataList = new JsonContentParser().Parse(fileUtility.GetContent(fileName));
            break;

        case ".xml":
            dataList = new XmlContentParser().Parse(fileUtility.GetContent(fileName));
            break;

        default:
            break;
    }

    return dataList;
}

void Search()
{
    Console.WriteLine("Enter the key to search.");
    var keyName = Console.ReadLine()!;

    try
    {
        string[] files = GetFilesInDataDirectory();
        ProcessFiles(keyName, files);
    }
    catch (DirectoryNotFoundException ex)
    {
        Console.WriteLine("Error: The 'data' directory is not found.");
        Console.WriteLine(ex.Message);
    }
}

string[] GetFilesInDataDirectory()
{
    // Get the current directory
    string currentDirectory = Directory.GetCurrentDirectory();
    string dataDirectory = Path.Combine(currentDirectory, "data");

    // Check if 'data' directory exists
    if (!Directory.Exists(dataDirectory))
    {
        throw new DirectoryNotFoundException($"The '{dataDirectory}' directory is not found.");
    }

    // Store files in 'data' directory in an array
    List<string> fileList = new List<string>();
    CollectFilesRecursively(dataDirectory, fileList);
    return fileList.ToArray();
}

void CollectFilesRecursively(string directory, List<string> fileList)
{
    // Collect files in directory
    string[] files = Directory.GetFiles(directory);
    fileList.AddRange(files);

    // Recursively collect files in subdirectories
    string[] subDirectories = Directory.GetDirectories(directory);
    foreach (string subDirectory in subDirectories)
    {
        CollectFilesRecursively(subDirectory, fileList);
    }
}

void ProcessFiles(string searchKey, string[] files)
{
    // Process the files here
    Console.WriteLine("Processing files:");
    
    foreach (string file in files)
    {
        IEnumerable<Data>? dataList = GetDataList(file);
        if (dataList != null)
        {
            foreach (Data data in dataList)
            {
                // Check if searchKey is present in the value (case-insensitive)
                if (data.Key.IndexOf(searchKey, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Get the relative path
                    string relativePath = GetRelativePath(file);
                    Console.WriteLine($"Key: {data.Key}, Value: {data.Value}, FileName:{relativePath}");
                }
            }
        }
    }

    Console.WriteLine("Processing completed");
    
}

string GetRelativePath(string fullPath)
{
    // Get the current directory
    string currentDirectory = Directory.GetCurrentDirectory();

    // Make the file path relative to the current directory
    Uri fullUri = new Uri(fullPath);
    Uri relativeUri = new Uri(currentDirectory + Path.DirectorySeparatorChar);
    return Uri.UnescapeDataString(relativeUri.MakeRelativeUri(fullUri).ToString());
}