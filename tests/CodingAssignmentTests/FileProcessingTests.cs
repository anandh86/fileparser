using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;
using Moq;
namespace CodingAssignmentTests;

public class FileProcessingTests
{
    private FileProcessService _fileProcessService = null!;
    private Mock<IFileUtility> _mockFileUtility = null!;
    private IFileUtility fileUtility = null!;
    // TODO : This may not be needed
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
        //_mockFileUtility.Setup(x => x.GetFilesInDirectory("C:\\Temp")).Returns(new[] { "file1.txt", "file2.pdf" });

        // Act
        _fileProcessService.DisplayFileContents(fileName);

        // Assert
        List<Data>? outputContent = outputHandler.FileContent;
        Assert.IsNull(outputContent);

        // Cleanup
        outputHandler.FileContent = null;
    }
}