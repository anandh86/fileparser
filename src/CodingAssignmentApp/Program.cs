using System.IO.Abstractions;
using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;

public class Program
{
    // Entry point
    public static void Main(string[] args)
    {
        // Initial setup
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
            Console.WriteLine("dataList is null. No data to process.");
        }
    }

    // Helper function to fetch data list based on fileName
    private static IEnumerable<Data>? GetDataList(string fileName)
    {
        var fileUtility = new FileUtility(new FileSystem());
        var dataList = Enumerable.Empty<Data>();

        var fileExtension = fileUtility.GetExtension(fileName);

        try
        {
            // Factory creation for parsers
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

        SearchFiles searchFiles = new SearchFiles();
        searchFiles.SearchKeyInFiles(keyName);
    }
}

