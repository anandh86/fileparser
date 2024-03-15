using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentTests;

public class CsvContentParserTests
{
    private CsvContentParser _sut = null!;
    
    [SetUp]
    public void Setup()
    {
        _sut = new CsvContentParser();
    }

    [Test]
    public void Parse_ReturnsData()
    {
        var content = "a,b" + Environment.NewLine + "c,d" + Environment.NewLine;
        var dataList = _sut.Parse(content).ToList();
        CollectionAssert.AreEqual(new List<Data>
        {
            new("a", "b"),
            new("c", "d")
        }, dataList);
    }

    [Test]
    public void Parse_InvalidCsv_ReturnsEmptyCollection()
    {
        // Arrange
        string invalidCsvContent = "Invalid CSV content";

        // Act
        IEnumerable<Data> result = _sut.Parse(invalidCsvContent);

        // Assert
        Assert.IsEmpty(result);
    }
}