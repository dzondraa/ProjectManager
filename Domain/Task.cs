using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Dictionary<string, object>> Properties { get; set; } = null;

    }
}
