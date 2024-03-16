using System.IO.Abstractions;
using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;

public class Program
{
    private static FileProcessService _fileProcessService = null!;
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

        // Our functionality depends upon file processing and printing out to console
        // Inject these dependencies to our file processing service
        IFileUtility fileUtility = new FileUtility(new FileSystem());
        IOutputHandler outputHandler = new ConsoleHandler();

        _fileProcessService = new FileProcessService(fileUtility, outputHandler);
    }

    private static void Display()
    {
        Console.WriteLine("Enter the name of the file to display its content:");

        var fileName = Console.ReadLine()!;

        _fileProcessService.DisplayFileContents(fileName);
    }

    private static void Search()
    {
        Console.WriteLine("Enter the key to search.");
        var keyName = Console.ReadLine()!;

        _fileProcessService.SearchForKey(keyName);
    }
}

