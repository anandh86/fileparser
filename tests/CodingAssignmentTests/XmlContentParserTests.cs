using System.Text;
using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentTests;

public class XmlContentParserTests
{
    private XmlContentParser _sut = null!;

    [SetUp]
    public void Setup()
    {
        _sut = new XmlContentParser();
    }

    [Test]
    public void Parse_ReturnsData()
    {
        // Arrange
        string xmlContent = @"
            <Datas>
                <Data>
                    <Key>YvXYLQtn7V</Key>
                    <Value>CTDhy3riwv</Value>
                </Data>
                <Data>
                    <Key>aUlIcPeSWo</Key>
                    <Value>z41HQfrXIL</Value>
                </Data>
                <Data>
                    <Key>pXuhB1R9Lz</Key>
                    <Value>mkU7lg9xJT</Value>
                </Data>                
            </Datas>";

        // Act
        IEnumerable<Data> result = _sut.Parse(xmlContent);

        // Assert
        List<Data> expectedData = new List<Data>
        {
            new Data("YvXYLQtn7V", "CTDhy3riwv"),
            new Data("aUlIcPeSWo", "z41HQfrXIL"),
            new Data("pXuhB1R9Lz", "mkU7lg9xJT")
        };

        CollectionAssert.AreEqual(expectedData, result);
    }

    [Test]
    public void Parse_InvalidXml_ReturnsEmptyCollection()
    {
        // Arrange
        string invalidXmlContent = "Invalid XML content";

        // Act
        IEnumerable<Data> result = _sut.Parse(invalidXmlContent);

        // Assert
        Assert.IsEmpty(result);
    }

}