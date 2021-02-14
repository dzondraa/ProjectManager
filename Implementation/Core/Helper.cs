using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Implementation.Core
{
    public static class Helper
    {
        public static dynamic toEntityValue(JsonElement value)
        {
            var type = value.ValueKind;
            switch (type)
            {
                case JsonValueKind.Number:
                    return value.GetDouble();
                case JsonValueKind.String:
                    return value.GetString();
                case JsonValueKind.True:
                    return true;
                case JsonValueKind.False:
                    return false;
            }
            return null;
        }
    }
}
