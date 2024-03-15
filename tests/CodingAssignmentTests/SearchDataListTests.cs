using System.Text;
using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentTests;

public class SearchDataListTests
{
    private SearchDataList _sdl = null!;

    [SetUp]
    public void Setup()
    {
        _sdl = new SearchDataList();
    }

    [Test]
    public void Search_ReturnsData()
    {
        // Arrange
        List<Data> inputData = new List<Data>
        {
            new Data("key1","value1"),
            new Data("kEy1","value2"),
            new Data("key2","value3"),
            new Data("KEY1","value4"),
            new Data("key3","value5")
        };

        List<Data> expectedData = new List<Data>
        {
            new Data("key1","value1"),
            new Data("kEy1","value2"),
            new Data("KEY1","value4")
        };

        // Act
        var result = _sdl.Search("key1", inputData);

        // Assert
        Assert.IsNotEmpty(result);
        Assert.That(expectedData, Is.EqualTo(result));
    }

    [Test]
    public void Search_ReturnsEmpty()
    {
        // Arrange
        List<Data> inputData = new List<Data>
        {
            new Data("key1","value1"),
            new Data("kEy1","value2"),
            new Data("key2","value3"),
            new Data("KEY1","value4"),
            new Data("key3","value5")
        };        

        // Act
        var result = _sdl.Search("key4", inputData);

        // Assert
        Assert.IsEmpty(result);
    }


}

