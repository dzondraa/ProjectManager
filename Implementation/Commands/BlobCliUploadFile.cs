using Application.Commands;
using Application.DataTransfer;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Implementation.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class BlobCliUploadFile : IUploadFileCommandAsync
    {
        private readonly BlobServiceClient _blobServiceClient;

        public int Id => 1211;

        public string Name => "Uploading code files from drive, using BlobCli";

        public BlobCliUploadFile(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task Execute(FileDto request)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("code");
            // Clearing the metadata field
            containerClient.SetMetadata(new Dictionary<string, string>());
            if (!File.Exists(request.Path)) throw new FileNotFoundException("asd");
            var blobClient = containerClient.GetBlobClient(request.Name);
            var metaData = new Dictionary<string, string>(1);
            metaData.Add("Project", request.ProjectName);
            //blobClient.SetMetadata(metaData);
            await blobClient.UploadAsync(request.Path, new BlobHttpHeaders { ContentType = request.Path.GetContentType() });
            blobClient.SetMetadata(metaData);
        }
    }
}
