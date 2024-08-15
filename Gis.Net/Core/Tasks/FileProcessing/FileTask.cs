namespace Gis.Net.Core.Tasks.FileProcessing;

/// <inheritdoc />
public class FileTask : IFileTask
{
    /// <inheritdoc />
    public string FilePath { get; set; }

    /// <inheritdoc />
    public Func<string, CancellationToken, Task> Process { get; set; }
}