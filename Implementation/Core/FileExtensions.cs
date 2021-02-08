using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Core
{
    public static class FileExtensions
    {
        private static readonly FileExtensionContentTypeProvider _provider = new FileExtensionContentTypeProvider();

        public static string GetContentType(this string fileName)
        {
            if(!_provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
