using Application.DataTransfer;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Application.Queries
{
    public interface IGetCode : IQueryAsync<FileDto, FileInfo>
    {
    }
}
