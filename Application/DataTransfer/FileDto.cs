using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class FileDto
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string ProjectName { get; set; }
        public string Timestamp { get; } = DateTime.Now.ToString();
    }
}
