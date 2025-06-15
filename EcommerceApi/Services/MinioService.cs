using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace EcommerceApi.Services;

public class MinioService
{
    private readonly IMinioClient _minioClient;
    private const string bucketName = "pictures";

    public MinioService()
    {
        _minioClient = new MinioClient()
            .WithEndpoint("localhost", 9000)
            .WithCredentials("root", "password")
            .Build();
    }


    /// <summary>
    /// Uploads an image file to MinIO storage.
    /// </summary>
    /// <param name="image">The image file to be uploaded.</param>
    /// <returns>The file path of the uploaded image.</returns>
    public async Task<string> UploadImage(IFormFile image) 
    {
        var fileName = $"{Guid.NewGuid()}_{image.FileName}";

        using (var stream = image.OpenReadStream()) 
        {
            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName)
                .WithStreamData(stream)
                .WithObjectSize(image.Length)
                .WithContentType(image.ContentType)
            );
        }

        var filePath = $"{bucketName}/{fileName}";

        return filePath;
    }

    public async Task DeleteImage(string imageName)
    {
        var parts = imageName.Split('/', 2); 
        
        if (parts.Length != 2)
        {
            Console.WriteLine("Invalid image name format. The format should be 'bucketname/objectkey'.");
            return;
        }

        var bucketName = parts[0]; 
        var objectKey = parts[1];  

        await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectKey)    
        );
    }
}
