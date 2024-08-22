namespace Gis.Net.Core.Tasks.FileProcessing;

/// <inheritdoc />
public class FileTask : IFileTask
{
    /// <inheritdoc />
    public required string FilePath { get; set; }

    /// <inheritdoc />
    public required Func<string, CancellationToken, Task> Process { get; set; }
}