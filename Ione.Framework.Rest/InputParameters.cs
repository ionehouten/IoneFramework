using Newtonsoft.Json;
using RestSharp.Serializers;
using System.Collections.Generic;

namespace Belant.Framework.Rest
{
    /// <summary>
    /// InputParameters
    /// Untuk input parameter Select 
    /// </summary>
    public class InputParameters
    {
        /// <summary>
        /// Limit
        /// </summary>
        [JsonProperty(PropertyName = "limit")]
        [SerializeAs(Name = "limit")]
        public int? Limit { get; set; }

        /// <summary>
        /// Offset
        /// </summary>
        [JsonProperty(PropertyName = "offset")]
        [SerializeAs(Name = "offset")]
        public int? Offset { get; set; }

        /// <summary>
        /// Order
        /// </summary>
        [JsonProperty(PropertyName = "order")]
        [SerializeAs(Name = "order")]
        public string Order { get; set; }

        /// <summary>
        /// Condition
        /// </summary>
        public object Condition { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Files
        /// </summary>
        public List<InputFile> Files { get; set; }
    }

    /// <summary>
    /// InputFile
    /// Untuk upload file
    /// </summary>
    public class InputFile
    {
        /// <summary>
        /// Name : Filename
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Path : FilePath
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Type : ContentType
        /// </summary>
        public string Type { get; set; }
    }
}
