using System.IO.Abstractions;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib;

public class FileUtility : IFileUtility
{
    private readonly IFileSystem _fileSystem;

    public FileUtility(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }
    
    public string GetExtension(string fileName)
    {
        return _fileSystem.FileInfo.New(fileName).Extension;
    }

    public string GetContent(string fileName)
    {
        return _fileSystem.File.ReadAllText(fileName);
    }

    public string GetRelativePath(string fullPath)
    {
        // Get the current directory
        string currentDirectory = Directory.GetCurrentDirectory();

        // Make the file path relative to the current directory
        Uri fullUri = new Uri(fullPath);
        Uri relativeUri = new Uri(currentDirectory + Path.DirectorySeparatorChar);
        return Uri.UnescapeDataString(relativeUri.MakeRelativeUri(fullUri).ToString());
    }

    public string[] GetFilesInDirectory(string directory)
    {
        // Get the current directory
        string currentDirectory = Directory.GetCurrentDirectory();
        string dataDirectory = Path.Combine(currentDirectory, directory);

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

    private void CollectFilesRecursively(string directory, List<string> fileList)
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
}