using System.IO.Abstractions;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib
{
    // TODO: This whole class can be removed
    public class SearchFiles
    {

        public void SearchKeyInFiles(string searchKey)
        {
            try
            {
                //string[] files = GetFilesInDataDirectory();
                //ProcessFiles(searchKey, files);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Error: The 'data' directory is not found.");
                Console.WriteLine(ex.Message);
            }
        }
#if false
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

        



        private static void ProcessFiles(string searchKey, string[] files)
        {
            // Process the files here
            Console.WriteLine("Processing files:");

            foreach (string file in files)
            {
                IEnumerable<Data>? dataList = GetDataList(file);
                if (dataList != null)
                {
                    // Get the relative path
                    string relativePath = GetRelativePath(file);

                    SearchDataList searchDataList = new SearchDataList();
                    var retVal = searchDataList.Search(searchKey, dataList);

                    if (retVal != null)
                    {
                        foreach (Data data in retVal)
                        {
                            Console.WriteLine($"Key:{data.Key} Value:{data.Value} FileName:{relativePath}");
                        }
                    }
                }
            }
        }

#endif
    }
}

