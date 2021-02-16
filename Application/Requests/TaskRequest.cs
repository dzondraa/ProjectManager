using Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests
{
    public class TaskRequest
    {
        private string Id { get; set; }
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<DataModel> AdditionalFields { get; set; } = null;

        public void SetId(string id)
        {
            this.Id = id;
        }
    }
}
