// See https://aka.ms/new-console-template for more information

using System.IO.Abstractions;
using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;

public class Program
{
    // Entry point
    public static void Main(string[] args)
    {
        Init();

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
                    Console.WriteLine("Invalid operation, try again!");
                    break;
            }
        } while (true);
    }

    private static void Init()
    {
        // Register new content parsers here
        ContentParserFactory.RegisterContentParser(".csv", typeof(CsvContentParser));
        ContentParserFactory.RegisterContentParser(".json", typeof(JsonContentParser));
        ContentParserFactory.RegisterContentParser(".xml", typeof(XmlContentParser));
    }

    private static void Display()
    {
        Console.WriteLine("Enter the name of the file to display its content:");

        var fileName = Console.ReadLine()!;

        var dataList = GetDataList(fileName);

        Console.WriteLine("Data:");

        if (dataList != null)
        {
            foreach (var data in dataList)
            {
                Console.WriteLine($"Key:{data.Key} Value:{data.Value}");
            }
        }
        else
        {
            // Handle the case where dataList is null (e.g., log a message or skip processing)
            Console.WriteLine("dataList is null. No data to process.");
        }
    }

    private static IEnumerable<Data>? GetDataList(string fileName)
    {
        var fileUtility = new FileUtility(new FileSystem());
        var dataList = Enumerable.Empty<Data>();

        var fileExtension = fileUtility.GetExtension(fileName);

        try
        {
            IContentParser? contentParser = ContentParserFactory.CreateContentParser(fileExtension);
            dataList = contentParser?.Parse(fileUtility.GetContent(fileName));
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid file extension");
        }

        return dataList;
    }

    private static void Search()
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

    private static string[] GetFilesInDataDirectory()
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

    private static void CollectFilesRecursively(string directory, List<string> fileList)
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

    private static void ProcessFiles(string searchKey, string[] files)
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

    private static string GetRelativePath(string fullPath)
    {
        // Get the current directory
        string currentDirectory = Directory.GetCurrentDirectory();

        // Make the file path relative to the current directory
        Uri fullUri = new Uri(fullPath);
        Uri relativeUri = new Uri(currentDirectory + Path.DirectorySeparatorChar);
        return Uri.UnescapeDataString(relativeUri.MakeRelativeUri(fullUri).ToString());
    }
}

