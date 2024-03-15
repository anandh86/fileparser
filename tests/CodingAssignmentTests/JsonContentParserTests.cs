using System.Text;
using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentTests;

public class JsonContentParserTests
{
    private JsonContentParser _sut = null!;

    [SetUp]
    public void Setup()
    {
        _sut = new JsonContentParser();
    }

    [Test]
    public void Parse_ReturnsData()
    {
        // Arrange
        string jsonContent = @"
        [
            {""Key"":""75knWnMBov"",""Value"":""FYADSziM6C""},
            {""Key"":""nz1hFteM9w"",""Value"":""4bZhnHCOub""},
            {""Key"":""ohtNQAzzS1"",""Value"":""SFSOwGaolU""}
        ]";

        string json = jsonContent.Trim();

        // Act
        IEnumerable<Data> result = _sut.Parse(json);

        // Assert
        List<Data> expectedData = new List<Data>
        {
            new Data("75knWnMBov", "FYADSziM6C"),
            new Data("nz1hFteM9w", "4bZhnHCOub"),
            new Data("ohtNQAzzS1", "SFSOwGaolU")
        };

        CollectionAssert.AreEqual(expectedData, result);
    }

    [Test]
    public void Parse_InvalidJson_ReturnsEmptyCollection()
    {
        // Arrange
        string invalidJsonContent = "Invalid JSON content";

        // Act
        IEnumerable<Data> result = _sut.Parse(invalidJsonContent);

        // Assert
        Assert.IsEmpty(result);
    }

}


