using Newtonsoft.Json;
using RestSharp.Serializers;

namespace Belant.Framework.Core
{ 
    /// <summary>
    /// InputParameters
    /// Untuk output input
    /// </summary>
    /// <typeparam name="T">Class</typeparam>
    public class InputParameters<T> where T : new()
    {
        [JsonProperty(PropertyName = "limit"), SerializeAs(Name = "limit")]
        public int? Limit { get; set; }
        [JsonProperty(PropertyName = "offset"), SerializeAs(Name = "offset")]
        public int? Offset { get; set; }
        [JsonProperty(PropertyName = "filter"), SerializeAs(Name = "filter")]
        public string Filter { get; set; }
        public T Data { get; set; }
        [JsonProperty(PropertyName = "order"), SerializeAs(Name = "order")]
        public string Order { get; set; }
    }
}
