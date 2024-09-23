using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Gis.Net.Aws.AWSCore.Exceptions;
using Gis.Net.Aws.AWSCore.S3.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeTypes;

namespace Gis.Net.Aws.AWSCore.S3.Services;

/// <summary>
/// AWS S3 Service
/// </summary>
public class AwsBucketService : IAwsBucketService
{
    private readonly IAmazonS3 _s3;
    private readonly ILogger<AwsBucketService> _logger;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// AWS S3 Service
    /// </summary>
    public AwsBucketService(IAmazonS3 s3,
        ILogger<AwsBucketService> logger,
        IConfiguration configuration)
    {
        _s3 = s3;
        _logger = logger;
        _configuration = configuration;
    }
    
    /// <summary>
    /// Checks if the specified S3 bucket exists.
    /// </summary>
    /// <param name="bucketName">The name of the bucket to check.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the bucket exists.</returns>
    /// <exception cref="AwsExceptions">Thrown when the bucket name is null or empty.</exception>
    private async Task<bool> IfExistBucket(string? bucketName)
    {
        if (string.IsNullOrEmpty(bucketName)) 
            throw new AwsExceptions("Bucket name is null or empty.");
    
        return await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3, bucketName);
    }

    /// <inheritdoc />
    public async Task CheckExistBucket(string? bucketName)
    {
        if (!await IfExistBucket(bucketName))
            throw new AwsExceptions($"Bucket {bucketName} does not exist.");
    }

    /// <summary>
    /// Create Bucket
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <exception cref="AwsExceptions"></exception>
    public async Task<bool> CreateBucket(AwsS3BucketRootDto options, CancellationToken cancel)
    {
        if (await IfExistBucket(options.BucketName))
        {
            _logger.LogInformation($"Bucket {options.BucketName} already exists.");
            return true;
        }

        options.Region ??= _configuration["AWS_REGION"];
        
        // create bucket
        // Info: https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TPutBucketRequest.html
        var request = new PutBucketRequest
        {
            BucketName = options.BucketName,
            UseClientRegion = true,
            BucketRegionName = options.Region,
            CannedACL = S3CannedACL.Private, // Set the ACL to Private to avoid problems with BlockPublicAccess
            ObjectOwnership = ObjectOwnership.ObjectWriter
        };
        
        // Configure Block Public Access settings
        var blockPublicAccess = new PublicAccessBlockConfiguration
        {
            BlockPublicAcls = true,
            IgnorePublicAcls = true,
            BlockPublicPolicy = true,
            RestrictPublicBuckets = true
        };
        
        // Apply block public access settings to the bucket
        await _s3.PutPublicAccessBlockAsync(new PutPublicAccessBlockRequest
        {
            BucketName = options.BucketName,
            PublicAccessBlockConfiguration = blockPublicAccess
        }, cancel);

        var response = await _s3.PutBucketAsync(request, cancel);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    /// <summary>
    /// Create folder
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <exception cref="AwsExceptions"></exception>
    public async Task<bool> CreateFolder(AwsS3BucketDto options, CancellationToken cancel)
    {
        await CheckExistBucket(options.BucketName);
        
        var request = new PutObjectRequest
        {
            BucketName = options.BucketName,
            StorageClass = S3StorageClass.Standard, 
            ServerSideEncryptionMethod = ServerSideEncryptionMethod.None,
            Key = options.Key,
            ContentBody = string.Empty
        };

        // add try catch in case you have exceptions shield/handling here 
        var response = await _s3.PutObjectAsync(request, cancel);
        _logger.LogDebug($"Create Folder -> {response.HttpStatusCode}");
        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    /// <summary>
    /// Gel list of all buckets
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<string>> GetAllBuckets(CancellationToken cancel)
    {
        var data = await _s3.ListBucketsAsync(cancel);
        var buckets = data.Buckets.Select(b => b.BucketName);
        return buckets;
    }

    /// <summary>
    /// Create url request
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    private static GetPreSignedUrlRequest GetUrlRequest(AwsS3BucketFileDto options) => new()
        {
            BucketName = options.BucketName,
            Key = options.Key,
            Expires = DateTime.UtcNow.AddMinutes(options.Minutes)
        };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    /// <exception cref="AwsExceptions"></exception>
    private async Task<ListObjectsV2Response?> GetListFiles(AwsS3BucketDto options, CancellationToken cancel)
    {
        await CheckExistBucket(options.BucketName);
        
        var request = new ListObjectsV2Request
        {
            BucketName = options.BucketName
        };
        
        if (options.ContinuationToken is not null)
            request.ContinuationToken = options.ContinuationToken;

        if (options.Prefix is not null)
            request.Prefix = options.Prefix;

        return await _s3.ListObjectsV2Async(request, cancel);
    }

    /// <summary>
    /// Creates a list of files from the provided list of objects retrieved from a bucket.
    /// </summary>
    /// <param name="listFilesFromBucket">The list of objects retrieved from the bucket.</param>
    /// <param name="options">The options for filtering the files.</param>
    /// <param name="withPresignedUrl">Flag to indicate whether to generate presigned URLs for the files.</param>
    /// <returns>A collection of AWS S3 object DTOs representing the files.</returns>
    private IEnumerable<AwsS3ObjectDto?>? CreateListFiles(ListObjectsV2Response? listFilesFromBucket,
        AwsS3BucketFileDto options,
        bool withPresignedUrl = false) 
        => listFilesFromBucket?.S3Objects.Select(s =>
        {
            var files = s.Key.Split("/");
            var fileName = files.LastOrDefault(f => f != options.Prefix);
            
            var s3Obj = new AwsS3ObjectDto
            {
                Prefix = s.Key.ToString(),
                Size = s.Size,
                LastModified = s.LastModified,
                FileName = fileName,
                PresignedUrl = withPresignedUrl ? _s3.GetPreSignedURL(GetUrlRequest(options)) : string.Empty
            };

            if (options.Key is null) return s3Obj;
            return files.Contains(options.Key) ? s3Obj : null;
            
        }).Where(s => s is not null)
          .Where(s => s?.Size > 0).AsEnumerable();


    /// <summary>
    /// Moves a file from one AWS S3 bucket to another.
    /// </summary>
    /// <param name="options">The options for moving the file, including the source bucket name, source key, destination bucket name, and destination key.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task MoveFile(AwsS3MoveFileDto options, CancellationToken cancel)
    {
        var requestToCopy = new CopyObjectRequest
        {
            SourceBucket = options.BucketName,
            SourceKey = options.Key,
            DestinationBucket = options.DestinationBucketName,
            DestinationKey = options.DestinationKey
        };

        var responseToCopy = await _s3.CopyObjectAsync(requestToCopy, cancel);
        if (responseToCopy.HttpStatusCode == HttpStatusCode.OK)
            _logger.LogInformation($"File {options.Key} moved successfully to {options.DestinationKey}");
            
        var requestToDel = new DeleteObjectRequest
        {
            BucketName = options.BucketName,
            Key = options.Key
        };

        var responseToDel = await _s3.DeleteObjectAsync(requestToDel, cancel);
        if (responseToDel.HttpStatusCode == HttpStatusCode.OK)
            _logger.LogInformation($"File {options.Key} deleted successfully");
    }

    /// <inheritdoc />
    public async Task<bool> IsExistFile(string bucketName, string key, CancellationToken cancel)
    {
        try
        {
            await _s3.GetObjectMetadataAsync(bucketName, key, cancel);
            return true;
        }
        catch (AmazonS3Exception e) when (e.StatusCode == HttpStatusCode.NotFound)
        {
            return false;
        }
    }

    /// <summary>
    /// Check if exists file in bucket
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    /// <exception cref="AwsExceptions"></exception>
    public async Task<bool> IsExistFile(AwsS3BucketFileDto options, CancellationToken cancel)
    {
        await CheckExistBucket(options.BucketName);
        
        if (options.Prefix is null)
            throw new AwsExceptions("Prefix is required.");

        bool exitCheck;
        bool isFile;
        var continuationToken = string.Empty;
        
        do
        {
            var listFiles = await GetListFiles(new AwsS3BucketDto
            {
                BucketName = options.BucketName,
                ContinuationToken = continuationToken,
                Prefix = $"{options.Prefix}/"
            }, cancel);
            
            continuationToken = listFiles?.ContinuationToken;
            // list of files present in the bucket
            var s3Objects = CreateListFiles(listFiles, options)?.ToList();
            isFile = s3Objects?.Count > 0;
            exitCheck = continuationToken is null;
        } while (!exitCheck && !isFile);

        return isFile;
    }

    /// <summary>
    /// Get list of files with pre-signed url from bucket
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    /// <exception cref="AwsExceptions"></exception>
    public async Task<IEnumerable<AwsS3ObjectDto?>?> GetAllFiles(AwsS3BucketFileDto options, CancellationToken cancel)
    {
        var listFiles = await GetListFiles(new AwsS3BucketDto
        {
            BucketName = options.BucketName,
            Prefix = options.Prefix
        }, cancel);
        
        return CreateListFiles(listFiles, options, true);
        
    }

    /// <summary>
    /// Delete bucket
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    public async Task DeleteBucket(AwsS3BucketRootDto options, CancellationToken cancel) 
        => await _s3.DeleteBucketAsync(options.BucketName, cancel);

    /// <summary>
    /// Delete Folder
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <exception cref="AwsExceptions"></exception>
    public async Task DeleteFolder(AwsS3BucketDto options, CancellationToken cancel)
    {
        var deleteObjectRequest = new DeleteObjectRequest
        {
            BucketName = options.BucketName,
            Key = options.Key
        };

        _logger.LogInformation("Deleting an object {KeyName}", options.Key);
        await _s3.DeleteObjectAsync(deleteObjectRequest, cancel);
    }

    /// <summary>
    /// create url to share documents
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    /// <exception cref="AwsExceptions"></exception>
    public async Task<string> GetSharingUrl(AwsS3BucketUploadDto options, CancellationToken cancel)
    {
        await CheckExistBucket(options.BucketName);
        var urlRequest = GetUrlRequest(options);
        return await _s3.GetPreSignedURLAsync(urlRequest);
    }

    /// <summary>
    /// Uploads a file or stream to an AWS S3 bucket.
    /// </summary>
    /// <param name="options">The options for the upload, including bucket name, file, stream, key, and other settings.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the uploaded S3 object DTO as the result.</returns>
    /// <exception cref="AwsExceptions">
    /// Thrown when the bucket does not exist, the file/stream is not specified, or the key is not specified.
    /// </exception>
    public async Task<AwsS3ObjectDto> Upload(AwsS3BucketUploadDto options, CancellationToken cancel)
    {
        await CheckExistBucket(options.BucketName);

        if (options.File is null)
        {
            // check if stream is not null
            if (options.Stream is null)
                throw new AwsExceptions("File/Stream is required.");
            
            // check if prefix is not null
            if (options.Prefix is null)
                throw new AwsExceptions("File/Prefix is required.");
        }
        else
        {
            // check if file and stream are not specified at the same time
            if (options.Stream is not null)
                throw new AwsExceptions("File and Stream cannot be specified at the same time.");
            
            // check if file is empty
            if (options.File.Length == 0)
                throw new AwsExceptions($"File {options.File.FileName} is empty.");
        }
        
        options.Key ??= options.Filename;
        
        // check if key is not null
        if (options.Key is null)
            throw new AwsExceptions("Key is required.");
        
        // check if prefix is not null
        options.Prefix ??= "/";
        
        // create path key
        var pathKey = Path.Combine(options.Prefix, options.Key);
        
        if (options.Replace is not null && options.Replace.Value)
            // delete file if exists and replace = true
            await DeleteFile(new AwsS3BucketDto
            {
                BucketName = options.BucketName,
                Key = pathKey
            }, cancel);
        
        // get stream from file or memory stream
        var stream = options.File is null ? options.Stream : options.File.OpenReadStream();
        
        // get content type
        var contentType = options.File?.ContentType is not null
            ? options.File?.ContentType
            : MimeTypeMap.GetMimeType(new FileInfo(pathKey).Extension);
        
        // create request
        var request = new PutObjectRequest
        {
            BucketName = options.BucketName,
            Key = pathKey,
            InputStream = stream,
            ContentType = contentType
        };

        request.Metadata.Add("Content-Type", contentType);
        
        // add try catch in case you have exceptions shield/handling here
        await _s3.PutObjectAsync(request, cancel);
        _logger.LogInformation("File sent successfully");
        
        // create url request
        var urlRequest = GetUrlRequest(options);

        // get presigned url
        var presignedUrl = options.Share is not null && options.Share.Value
            ? await _s3.GetPreSignedURLAsync(urlRequest)
            : string.Empty;

        // create object result
        return new AwsS3ObjectDto
        {
            Prefix = pathKey,
            PresignedUrl = presignedUrl,
            Size = options.File?.Length,
            LastModified = DateTime.UtcNow,
            FileName = options.File?.FileName
        };
    }

    /// <summary>
    /// Download file
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<AwsDownloadDto> DownloadFile(AwsS3BucketDto options, CancellationToken cancel)
    {
        var result = await GetFileByKey(options, cancel);
        return new AwsDownloadDto(result.ResponseStream, result.Headers.ContentType);
    }

    /// <summary>
    /// GetFile from Bucket
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    /// <exception cref="AwsExceptions"></exception>
    private async Task<GetObjectResponse> GetFileByKey(AwsS3BucketDto options, CancellationToken cancel)
    {
        await CheckExistBucket(options.BucketName);
        var s3Object = await _s3.GetObjectAsync(options.BucketName, options.Key, cancel);
        return s3Object;
    }

    /// <summary>
    /// Delete file from bucket
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <exception cref="AwsExceptions"></exception>
    public async Task<DeleteObjectResponse> DeleteFile(AwsS3BucketDto options, CancellationToken cancel)
    {
        await CheckExistBucket(options.BucketName);
        return await _s3.DeleteObjectAsync(options.BucketName, options.Key, cancel);
    }
}