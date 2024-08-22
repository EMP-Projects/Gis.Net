namespace Gis.Net.Core.Tasks.FileProcessing;

/// <summary>
/// Interface for defining a task that operates on a file.
/// </summary>
public interface IFileTask
{
    /// <summary>
    /// Gets or sets the path of the file to be processed.
    /// </summary>
    /// <value>
    /// The file path.
    /// </value>
    string? FilePath { get; set; }
    
    /// <summary>
    /// Gets or sets the delegate that defines the process to be performed on the file.
    /// </summary>
    /// <value>
    /// The function that takes a file path and a cancellation token and returns a Task.
    /// </value>
    Func<string, CancellationToken, Task>? Process { get; set; } 
}