using System.IO.Abstractions;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib
{
    public class SearchFiles
    {

        public void SearchKeyInFiles(string searchKey)
        {
            try
            {
                string[] files = GetFilesInDataDirectory();
                ProcessFiles(searchKey, files);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Error: The 'data' directory is not found.");
                Console.WriteLine(ex.Message);
            }
        }

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

        private static string[] GetFilesInDataDirectory()
        {
            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();
            string dataDirectory = Path.Combine(currentDirectory, "data");

            // Check if 'data' directory exists
            if (!Directory.Exists(dataDirectory))
            {
                throw new DirectoryNotFoundException($"The '{dataDirectory}' directory is not found.");
            }

            // Store files in 'data' directory in an array
            List<string> fileList = new List<string>();
            CollectFilesRecursively(dataDirectory, fileList);
            return fileList.ToArray();
        }

        private static void CollectFilesRecursively(string directory, List<string> fileList)
        {
            // Collect files in directory
            string[] files = Directory.GetFiles(directory);
            fileList.AddRange(files);

            // Recursively collect files in subdirectories
            string[] subDirectories = Directory.GetDirectories(directory);
            foreach (string subDirectory in subDirectories)
            {
                CollectFilesRecursively(subDirectory, fileList);
            }
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

        private static string GetRelativePath(string fullPath)
        {
            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Make the file path relative to the current directory
            Uri fullUri = new Uri(fullPath);
            Uri relativeUri = new Uri(currentDirectory + Path.DirectorySeparatorChar);
            return Uri.UnescapeDataString(relativeUri.MakeRelativeUri(fullUri).ToString());
        }
    }
}

