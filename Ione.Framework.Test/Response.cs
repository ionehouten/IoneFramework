using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Ione.Framework.Test
{
    public class Response<TData, TOutput> where TData : new() where TOutput : new()
    {
        private bool _status = true;

        [JsonPropertyName("record_count")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Count { get; set; }

        [JsonPropertyName("elapsed_time")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ElapsedTime { get; set; }

        [JsonPropertyName("id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Id { get; set; }

        [JsonPropertyName("status")]
        [DefaultValue(true)]
        public bool Status { get { return _status; } set { _status = value; } }

        [JsonPropertyName("message")]
        public string Message { get; set; }
        
        [JsonPropertyName("error")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Error Error { get; set; }

        [JsonPropertyName("data")]
        public TData Data { get; set; }

        [JsonPropertyName("meta")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Metadata Meta{ get; set; }
    }

    public class Error
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("stack_trace")]
        public string StackTrace { get; set; }
    }

    public class Metadata
    {
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; }
    }

    public class Pagination
    {
        [JsonPropertyName("next_page")]
        public string NextPage { get; set; }

        [JsonPropertyName("page_size")]
        public int? PageSize { get; set; }

        [JsonPropertyName("prev_page")]
        public string PrevPage { get; set; }
    }
}
