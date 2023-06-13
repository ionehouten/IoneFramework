using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Ione.Framework.Test
{
   
    public class Request<T> where T : new()
    {
        [JsonPropertyName("pageSize")]
        [FromQuery(Name = "pageSize")]
        public int? PageSize { get; set; }
        [JsonPropertyName("page")]
        public int? Page { get; set; }
        [JsonIgnore]
        public string Filter { get; set; }
        [JsonPropertyName("order")]
        public string Order { get; set; }
        [JsonIgnore]
        public T Data { get; set; }
    }
}
