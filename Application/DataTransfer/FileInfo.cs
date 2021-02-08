using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Application.DataTransfer
{
    public class FileInfo
    {
        public Stream Content { get; set; }
        public string ContentType { get; set; }
    }
}
