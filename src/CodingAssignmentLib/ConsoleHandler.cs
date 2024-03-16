using System;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib;

public class ConsoleHandler : IOutputHandler
{
    public void OutputFileContents(IEnumerable<Data> datum)
    {
        foreach (var data in datum)
        {
            Console.WriteLine($"Key:{data.Key} Value:{data.Value}");
        }
    }

    public void OutputSearchResults(IEnumerable<Data> datum, string fileNameRelativePath)
    {
        foreach (Data data in datum)
        {
            Console.WriteLine($"Key:{data.Key} Value:{data.Value} FileName:{fileNameRelativePath}");
        }
    }
}