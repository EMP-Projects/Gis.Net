using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Gis.Net.Raster;

/// <summary>
/// Saves the contents of a file from a <see cref="Stream"/> to the specified file path asynchronously.
/// </summary>
/// <returns>A <see cref="Task"/> representing the asynchronous save operation.</returns>
public static class GisFiles
{
    /// <summary>
    /// Saves the contents of a file from a stream asynchronously.
    /// </summary>
    /// <param name="filePath">The path to the file where the contents will be saved.</param>
    /// <param name="stream">The stream containing the file contents.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SaveFileContentsAsync(string filePath, Stream stream)
    {
        await using (var destinationStream = new FileStream(filePath, FileMode.Create))
        {
            await stream.CopyToAsync(destinationStream);
            destinationStream.Close();
        }
        stream.Close();
    }

    /// <summary>
    /// Retrieves the path to the upload folder for raster files.
    /// </summary>
    /// <param name="uploadDirectory">The name of the upload directory. Defaults to "uploads" if not specified.</param>
    /// <returns>The full path to the upload folder for raster files.</returns>
    /// <remarks>
    /// This method constructs the path by combining the parent directory of the current working directory with the specified upload directory.
    /// If the parent directory cannot be determined, an empty string is returned.
    /// </remarks>
    public static string GetUploadFolderRaster(string uploadDirectory = "uploads")
    {
        var tmpDir = string.Empty;
        var currentDir = Directory.GetParent(Directory.GetCurrentDirectory());
        return currentDir is null 
            ? tmpDir : 
            $"{currentDir.FullName}/data/rise-gis/{uploadDirectory}";
    }

    /// <summary>
    /// Creates a new folder for uploading raster files in the specified upload directory.
    /// </summary>
    /// <param name="uploadDirectory">The name of the upload directory. Defaults to "uploads" if not specified.</param>
    /// <param name="subDirectory">The name of the subdirectory to be created inside the upload folder. Can be null or empty.</param>
    /// <returns>The full path to the created upload folder for raster files.</returns>
    /// <remarks>
    /// This method constructs the path by combining the parent directory of the current working directory
    /// with the specified upload directory. If the parent directory cannot be determined or the folder creation fails,
    /// an empty string is returned.
    /// </remarks>
    public static string CreateUploadsFolderRaster(string uploadDirectory, string? subDirectory)
    {
        var tmpDir = GetUploadFolderRaster(uploadDirectory);
        return CreateTempFolder(tmpDir, subDirectory);
    }
    
    /// <summary>
    /// Create temp folder
    /// </summary>
    /// <returns></returns>
    public static string CreateUploadsFolderToSave(string uploadDirectory, string? subDirectory)
    {
        var tmpDir = Path.Combine(Directory.GetCurrentDirectory(), uploadDirectory);
        return CreateTempFolder(tmpDir, subDirectory);
    }

    private static string CreateTempFolder(string path, string? subDirectory)
    {
        
        // creo directory di upload se non esiste
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        if (subDirectory is not null)
            path = Path.Combine(path, subDirectory);
        
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        
        return path;
    }

    /// <summary>
    /// Determines whether the given file path corresponds to a TIFF file.
    /// </summary>
    /// <param name="path">The path of the file to check.</param>
    /// <returns>
    /// <c>true</c> if the file is a TIFF file; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsTiff(string path)
        => FileType(path).Equals("TIFF");

    /// <summary>
    /// Internally, the file header information should help. if you do a low-level file open,
    /// such as StreamReader() or FOPEN()
    /// 
    /// Info: https://stackoverflow.com/questions/2731917/how-to-detect-if-a-file-is-pdf-or-tiff
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static string FileType(string path)
    {
        var sr = new StreamReader(path);
        var buf = new char[5];
        sr.Read( buf, 0, 4);
        sr.Close();
        var hdr = buf[0].ToString()
                  + buf[1].ToString()
                  + buf[2].ToString()
                  + buf[3].ToString()
                  + buf[4].ToString();

        if (hdr.StartsWith("%PDF"))
            return "PDF";
        
        if (hdr.StartsWith("MZ"))
            return "EXE or DLL";
        
        if (hdr.StartsWith("BM"))
            return "BMP";
        
        if (hdr.StartsWith("MM"))
            return "TIFF";
        
        return hdr.StartsWith("?_") 
            ? "HLP (help file)" 
            : "Unknown";
    }

    /// <summary>
    /// Search ESRI file
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static string? GetPathShapeFile(string path) => CheckPathShapeFile(path, ".shp");

    /// <summary>
    /// Gets the path of a TIFF file within the specified directory.
    /// </summary>
    /// <param name="path">The directory path where the TIFF file is located.</param>
    /// <returns>The path of the TIFF file.</returns>
    private static string? GetPathTiffFile(string path) => CheckPathShapeFile(path, ".tiff");

    /// <summary>
    /// Checks the path of a shape file within the specified directory and returns the path if found.
    /// </summary>
    /// <param name="path">The directory path where the shape file is located.</param>
    /// <param name="extension">The extension of the shape file, including the dot (e.g., ".shp").</param>
    /// <returns>The path of the shape file if found; otherwise, null.</returns>
    private static string? CheckPathShapeFile(string path, string extension) => Directory
        .EnumerateFiles(path)
        .Select(Path.GetFileName).First(fn 
            => Path.GetExtension(fn)!.ToUpper().Equals(extension.ToUpper()));

    /// <summary>
    /// Copies the contents of a stream to another stream asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static async Task CopyFiles(IFormFile file, string path)
    {
        await using var stream = File.Create(path);
        await file.CopyToAsync(stream);
        stream.Close();
    }

    /// <summary>
    /// Copies the contents of a stream to a file asynchronously.
    /// </summary>
    /// <param name="source">The source stream.</param>
    /// <param name="path">The path to the destination file.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static async Task CopyFilesAsync(Stream source, string path)
    {
        await using var stream = File.Create(path);
        await source.CopyToAsync(stream);
        stream.Close();
    }

    /// <summary>
    /// Copies the contents of a source stream to a destination stream asynchronously.
    /// </summary>
    /// <param name="source">The source stream containing the contents to be copied.</param>
    /// <param name="destination">The destination stream where the contents will be copied to.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task CopyFiles(Stream source, Stream destination)
    {
        var buffer = new byte[4096]; 
        int numRead; 
        while ((numRead = await source.ReadAsync(buffer)) != 0) 
        {
            await destination.WriteAsync(buffer.AsMemory(0, numRead));
        } 
    }

    /// <summary>
    /// Unzips a shapefile and saves it to the specified path.
    /// </summary>
    /// <param name="file">The shapefile to unzip.</param>
    /// <param name="path">The path where the unzipped shapefile will be saved.</param>
    /// <param name="replace">A flag indicating whether to replace an existing file with the same name in the specified path.</param>
    /// <returns>The path of the unzipped shapefile.</returns>
    public static async Task<string?> UnzipShapeFileAndSave(IFormFile file, string path, bool replace)
    {
        var tmpFile = Path.Combine(path, file.FileName);
        if (replace && File.Exists(tmpFile)) 
            File.Delete(tmpFile);
        await CopyFiles(file, tmpFile);
        ZipFile.ExtractToDirectory(tmpFile, path, true);
        var newPath = Path.Combine(path, GetPathShapeFile(path)!);
        return newPath;
    }

    /// <summary>
    /// Extracts a TIFF file from a zip archive and saves it to the specified directory.
    /// </summary>
    /// <param name="path">The directory where the TIFF file will be saved.</param>
    /// <param name="fileName">The file name of the zip archive.</param>
    /// <returns>The path of the extracted TIFF file.</returns>
    public static string UnzipTiffFile(string path, string fileName)
    {
        ZipFile.ExtractToDirectory(fileName, path, true);
        var newPath = Path.Combine(path, GetPathTiffFile(path)!);
        return newPath;
    }

    private static async Task<string> ReadStream(HttpResponseMessage responseHttp, string path, bool replace, string extension)
    {
        if (!responseHttp.IsSuccessStatusCode) 
            throw new FileNotFoundException();
        
        var content = responseHttp.Content;
        var contentStream = await content.ReadAsStreamAsync();
        
        var tmpFileName = Guid.NewGuid().ToString();
        var tmpFile = $"{Path.Combine(path, tmpFileName)}.{extension}";
        
        if (replace && File.Exists(tmpFile)) 
            File.Delete(tmpFile);

        // copia stream del download nella cartella temporanea
        await CopyFilesAsync(contentStream, tmpFile);
        
        return tmpFile;
    }

    /// <summary>
    /// Uploads a file from a specified URL and saves the file contents to a specified file path asynchronously.
    /// </summary>
    /// <param name="url">The URL of the file to upload.</param>
    /// <param name="path">The path to save the file contents to.</param>
    /// <param name="replace">A flag indicating whether to replace an existing file if it already exists.</param>
    /// <param name="extension">The file extension of the uploaded file.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous upload and save operation.</returns>
    /// <exception cref="FileNotFoundException">Thrown when the file at the specified URL is not found.</exception>
    private static async Task<string> UploadFileAndSave(string url, string path, bool replace, string extension)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(url);
        return await ReadStream(response, path, replace, extension);
    }

    /// <summary>
    /// Uploads a file to the specified URL and saves the response stream to a file asynchronously.
    /// </summary>
    /// <param name="url">The URL where the file will be uploaded.</param>
    /// <param name="body">The body of the request.</param>
    /// <param name="path">The path where the response stream will be saved.</param>
    /// <param name="replace">Specifies whether to replace the existing file at the specified path.</param>
    /// <param name="extension">The extension of the file to be saved.</param>
    /// <returns>A task representing the asynchronous upload and save operation.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the uploaded file is not found on the server.</exception>
    private static async Task<string> UploadFileAndSave(string url, string body, string path, bool replace, string extension)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("Accept", "*/*");
        request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
        var content = new StringContent(body, Encoding.UTF8, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await ReadStream(response, path, replace, extension);
    }

    /// <summary>
    /// Extracts and saves a shapefile from a zip file asynchronously.
    /// </summary>
    /// <param name="url">The URL of the zip file.</param>
    /// <param name="path">The path where the extracted shapefile will be saved.</param>
    /// <param name="replace">Specifies whether to replace the existing shapefile if it already exists.</param>
    /// <param name="body">The zip file contents as a string. Set null if the zip file is provided via URL.</param>
    /// <returns>
    /// The path to the extracted shapefile. Returns null if the shapefile extraction fails.
    /// </returns>
    /// <exception cref="FileNotFoundException">Thrown when the zip file specified by the URL is not found.</exception>
    public static async Task<string?> UnzipShapeFileAndSave(string url, string path, bool replace, string? body)
    {
        var tmpFile = body is null
            ? await UploadFileAndSave(url, path, replace, "zip")
            : await UploadFileAndSave(url, body, path, replace, "zip");
        
        ZipFile.ExtractToDirectory(tmpFile, path, true);
        var newPath = Path.Combine(path, GetPathShapeFile(path)!);
        return newPath;
    }

    /// <summary>
    /// Uploads a TIFF file from a URL or body and saves it to the specified path asynchronously.
    /// </summary>
    /// <param name="url">The URL of the TIFF file to upload. If the body is not null, this parameter is ignored.</param>
    /// <param name="path">The path where the TIFF file will be saved.</param>
    /// <param name="replace">Specifies whether to replace an existing file with the same name at the destination path. Default is true.</param>
    /// <param name="body">The string representing the body of the TIFF file to upload. If provided, it will be used instead of the URL.</param>
    /// <returns>A task representing the asynchronous upload and save operation. The resulting file path is returned.</returns>
    public static async Task<string?> UploadTiffFileAndSave(string url, string path, bool replace, string? body)
    {
        var tmpFile = body is null
            ? await UploadFileAndSave(url, path, replace, "tiff")
            : await UploadFileAndSave(url, body, path, replace, "tiff");
        
        return tmpFile;
    }

    /// <summary>
    /// Reads a TIFF file and returns its contents as a byte array.
    /// </summary>
    /// <param name="path">The path to the TIFF file.</param>
    /// <returns>The contents of the TIFF file as a byte array.</returns>
    public static byte[] ReadTiff(string path)
    {
        var stm = File.Open(path, FileMode.Open);
        var size = (int)stm.Length; // size of the file.
        var data = new byte[size];
        var totalByte = 0;

        while (size > 0) // loop until file' size is not 0.
        {
            var read = stm.Read(data, totalByte, size); // reading file's data.
            size -= read;
            totalByte += read;
        }

        return data;
    }

    /// <summary>
    /// Deletes a file at the specified path.
    /// </summary>
    /// <param name="pathFile">The path to the file to be deleted.</param>
    public static void DeleteFile(string pathFile)
    {
        var f = new FileInfo(pathFile);
        f.Delete();
    }

    /// <summary>
    /// Deletes a folder and its contents recursively.
    /// </summary>
    /// <param name="path">The path of the folder to be deleted.</param>
    /// <param name="recursive">Specifies whether to delete subfolders and files recursively.</param>
    public static void DeleteFolder(string path, bool recursive)
        => Directory.Delete(path, recursive);
}