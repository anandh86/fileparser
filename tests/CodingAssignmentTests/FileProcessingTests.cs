using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;
using Moq;
namespace CodingAssignmentTests;

public class FileProcessingTests
{
    private FileProcessService _fileProcessService = null!;
    private Mock<IFileUtility> _mockFileUtility = null!;
    private IFileUtility fileUtility = null!;
    private InterceptConsole outputHandler = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        // Register new content parsers here
        ContentParserFactory.RegisterContentParser(".csv", typeof(CsvContentParser));
        ContentParserFactory.RegisterContentParser(".json", typeof(JsonContentParser));
        ContentParserFactory.RegisterContentParser(".xml", typeof(XmlContentParser));

        // Create a mock object
        _mockFileUtility = new Mock<IFileUtility>();

        fileUtility = _mockFileUtility.Object;
        outputHandler = new InterceptConsole();

        _fileProcessService = new FileProcessService(fileUtility, outputHandler);
    }

    [Test]
    public void TestUnsupportedFileFormat()
    {
        // Arrange
        string fileName = "myfile.txt";

        _mockFileUtility.Setup(x => x.GetExtension(fileName)).Returns(".txt");

        // Act
        _fileProcessService.DisplayFileContents(fileName);

        // Assert
        List<Data>? outputContent = outputHandler.FileContent;
        Assert.IsNull(outputContent);

        // Cleanup
        outputHandler.FileContent = null;
    }

    [Test]
    public void Search_ReturnsValidData()
    {
        // Arrange
        string csvData = "aaaaa,bbbbb" + Environment.NewLine + "ccccc,ddddd" + Environment.NewLine + "eeeee,fffff" + Environment.NewLine + "ggggg,hhhhh" + Environment.NewLine;
        List<(Data, string)> expectedResult = new List<(Data, string)>()
        {   new (new Data("aaaaa", "bbbbb"), "data\\data.csv"),
            new (new Data("aaaaa", "bbbbb"), "data\\data2\\data2.csv")
        };

        _mockFileUtility.Setup(x => x.GetFilesInDirectory("data"))
                       .Returns(new[] { "data\\data.csv", "data\\data2\\data2.csv" });

        _mockFileUtility.Setup(x => x.GetRelativePath(It.IsAny<string>()))
                       .Returns((string path) => path);

        _mockFileUtility.Setup(x => x.GetExtension(It.IsAny<string>()))
                       .Returns(".csv");

        _mockFileUtility.Setup(x => x.GetContent("data\\data.csv"))
                       .Returns(csvData);

        _mockFileUtility.Setup(x => x.GetContent("data\\data2\\data2.csv"))
                       .Returns(csvData);

        // Act
        _fileProcessService.SearchForKey("aAaAa");

        // Assert
        List<(Data, string)>? actualResult = outputHandler.SearchResults;
        CollectionAssert.AreEqual(expectedResult, actualResult);

        // Cleanup
        outputHandler.SearchResults = null;
    }

    [Test]
    public void Search_ReturnsNullData()
    {
        // Arrange
        string csvData = "aaaaa,bbbbb" + Environment.NewLine + "ccccc,ddddd" + Environment.NewLine + "eeeee,fffff" + Environment.NewLine + "ggggg,hhhhh" + Environment.NewLine;        

        _mockFileUtility.Setup(x => x.GetFilesInDirectory("data"))
                       .Returns(new[] { "data\\data.csv" });

        _mockFileUtility.Setup(x => x.GetRelativePath(It.IsAny<string>()))
                       .Returns((string path) => path);

        _mockFileUtility.Setup(x => x.GetExtension(It.IsAny<string>()))
                       .Returns(".csv");

        _mockFileUtility.Setup(x => x.GetContent("data\\data.csv"))
                       .Returns(csvData);

        // Act
        _fileProcessService.SearchForKey("qqqq");

        // Assert
        List<(Data, string)>? actualResult = outputHandler.SearchResults;
        Assert.IsNull(actualResult);

        // Cleanup
        outputHandler.SearchResults = null;
    }
}