using System;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentTests
{
    // This class is used for testing purpose.
    // We capture the output info in our internal data structure
    // instead of printing out to console for testing purpose.
	public class InterceptConsole : IOutputHandler
    {
        public List<Data>? FileContent { get; set; }
        public List<(Data, string)>? SearchResults { get; set; }

        public void OutputFileContents(IEnumerable<Data> datum)
        {
            if (FileContent is null)
                FileContent = new List<Data>();

            foreach (var data in datum)
            {
                FileContent.Add(new Data(data.Key, data.Value));
            }
        }

        public void OutputSearchResults(IEnumerable<Data> datum, string fileNameRelativePath)
        {
            if (SearchResults is null)
                SearchResults = new List<(Data, string)>();

            foreach (Data data in datum)
            {
                SearchResults.Add(new(new Data(data.Key, data.Value), fileNameRelativePath));
            }
        }
    }
}

