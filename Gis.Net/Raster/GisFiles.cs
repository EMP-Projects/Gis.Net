using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Gis.Net.Vector;

/// <summary>
/// 
/// </summary>
public static class GisFiles
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="stream"></param>
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
    /// 
    /// </summary>
    /// <param name="uploadDirectory"></param>
    /// <param name="subDirectory"></param>
    /// <returns></returns>
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
    /// Controlla se il file sia un tiff
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static string? GetPathTiffFile(string path) => CheckPathShapeFile(path, ".tiff");

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="extension"></param>
    /// <returns></returns>
    private static string? CheckPathShapeFile(string path, string extension) => Directory
        .EnumerateFiles(path)
        .Select(Path.GetFileName).First(fn 
            => Path.GetExtension(fn)!.ToUpper().Equals(extension.ToUpper()));
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="file"></param>
    /// <param name="path"></param>
    private static async Task CopyFiles(IFormFile file, string path)
    {
        await using var stream = File.Create(path);
        await file.CopyToAsync(stream);
        stream.Close();
    }

    /// <summary>
    /// Copia asincrona di un file
    /// </summary>
    /// <param name="source"></param>
    /// <param name="path"></param>
    private static async Task CopyFilesAsync(Stream source, string path)
    {
        await using var stream = File.Create(path);
        await source.CopyToAsync(stream);
        stream.Close();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
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
    /// Decomprime un file esri e lo salva
    /// </summary>
    /// <param name="file"></param>
    /// <param name="path"></param>
    /// <param name="replace"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="path"></param>
    /// <param name="replace"></param>
    /// <param name="extension"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    private static async Task<string> UploadFileAndSave(string url, string path, bool replace, string extension)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(url);
        return await ReadStream(response, path, replace, extension);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="body"></param>
    /// <param name="path"></param>
    /// <param name="replace"></param>
    /// <param name="extension"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
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
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="path"></param>
    /// <param name="replace"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
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
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="path"></param>
    /// <param name="replace"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    public static async Task<string?> UploadTiffFileAndSave(string url, string path, bool replace, string? body)
    {
        var tmpFile = body is null
            ? await UploadFileAndSave(url, path, replace, "tiff")
            : await UploadFileAndSave(url, body, path, replace, "tiff");
        
        return tmpFile;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="pathFile"></param>
    public static void DeleteFile(string pathFile)
    {
        var f = new FileInfo(pathFile);
        f.Delete();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="recursive"></param>
    public static void DeleteFolder(string path, bool recursive) 
        => Directory.Delete(path, recursive);
}