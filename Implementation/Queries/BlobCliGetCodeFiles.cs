using Application.DataTransfer;
using Application.Queries;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class BlobCliGetCodeFiles : IGetCode
    {
        private readonly BlobServiceClient _blobServiceClient;
        public int Id => 1233;

        public string Name => "Downloading files using BlobStorage CLI";

        public BlobCliGetCodeFiles(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }


        public async Task<FileInfo> Execute(FileDto search)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("code");
            var blobClient = containerClient.GetBlobClient(search.Name);
            var blobDownloadInfo = await blobClient.DownloadAsync();
            return new FileInfo {
                Content = blobDownloadInfo.Value.Content,
                ContentType = blobDownloadInfo.Value.ContentType
            };

        }
    }
}
