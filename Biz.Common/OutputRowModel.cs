﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Biz.Common
{
    public enum KindOfType : ushort
    {
        ok = 1,
        error = 2
    }

    public class OutputRowModel
    {
        [JsonProperty("lineNumber")]
        public int LineNumber { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public KindOfType Type { get; set; }

        [JsonProperty("concatAB")]
        public string ConcatAB { get; set; }

        [JsonProperty("sumCD")]
        public int? SumCD { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        public override string ToString()
            => ConcatAB;

        public bool ShouldSerializeSumCD() 
            => SumCD.HasValue; 
    }
}
