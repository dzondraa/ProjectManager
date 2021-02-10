using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Application.DataTransfer
{
    public class DataModel
    {
        public string Name { get; set; }
        public JsonElement Value { get; set; }
    }
}
