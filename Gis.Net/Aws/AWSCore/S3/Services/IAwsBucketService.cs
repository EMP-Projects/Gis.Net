using Amazon.S3.Model;
using Gis.Net.Aws.AWSCore.S3.Dto;

namespace Gis.Net.Aws.AWSCore.S3.Services;

/// <summary>
/// Provides methods for interacting with AWS S3 buckets.
/// </summary>
public interface IAwsBucketService
{
    /// <summary>
    /// Create Bucket
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<bool> CreateBucket(AwsS3BucketRootDto options, CancellationToken cancel);
    
    /// <summary>
    /// Create folder
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<bool> CreateFolder(AwsS3BucketDto options, CancellationToken cancel);
    
    /// <summary>
    /// Get All Bucket
    /// </summary>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<IEnumerable<string>> GetAllBuckets(CancellationToken cancel);
    
    /// <summary>
    /// Get all files
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<IEnumerable<AwsS3ObjectDto?>?> GetAllFiles(AwsS3BucketFileDto options, CancellationToken cancel);

    /// <summary>
    /// Check if existing file
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<bool> IsExistFile(AwsS3BucketFileDto options, CancellationToken cancel);
    
    /// <summary>
    /// Delete Bucket
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task DeleteBucket(AwsS3BucketRootDto options, CancellationToken cancel);
    
    /// <summary>
    /// Upload file
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<AwsS3ObjectDto> Upload(AwsS3BucketUploadDto options, CancellationToken cancel);
    
    /// <summary>
    /// Delete file
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<DeleteObjectResponse> DeleteFile(AwsS3BucketDto options, CancellationToken cancel);
    
    /// <summary>
    /// Delete folder
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task DeleteFolder(AwsS3BucketDto options, CancellationToken cancel);

    /// <summary>
    /// DownloadFile
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<AwsDownloadDto> DownloadFile(AwsS3BucketDto options, CancellationToken cancel);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task MoveFile(AwsS3MoveFileDto options, CancellationToken cancel);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<string> GetSharingUrl(AwsS3BucketUploadDto options, CancellationToken cancel);
    
    /// <summary>
    /// Check if bucket exists
    /// </summary>
    /// <param name="bucketName"></param>
    /// <returns></returns>
    Task<bool> IsExistBucket(string? bucketName);

}