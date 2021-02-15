using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class ProjectDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Timestamp { get; } = DateTime.Now.ToString();

    }
}
