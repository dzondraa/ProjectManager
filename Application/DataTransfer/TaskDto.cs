using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class TaskDto
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<DataModel> AdditionalFields { get; set; } = null;
        public string Timestamp { get; } = DateTime.Now.ToString();


    }
}
