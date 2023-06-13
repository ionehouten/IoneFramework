using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;

namespace Belant.Framework.Core
{
    public class JsonNetSerializer : IRestSerializer
    {

        public JsonSerializerSettings JsonSerializerSettings { get; set; }


        public string Serialize(object obj) => JsonConvert.SerializeObject(obj);

        public string Serialize(Parameter bodyParameter) => JsonConvert.SerializeObject(bodyParameter.Value, JsonSerializerSettings);

        public T Deserialize<T>(RestResponse response) => JsonConvert.DeserializeObject<T>(response.Content);

        public string[] SupportedContentTypes { get; } = {
            "application/json", "text/json", "text/x-json", "text/javascript", "*+json"
        };

        public string ContentType { get; set; } = "application/json";

        public DataFormat DataFormat { get; } = DataFormat.Json;

        public ISerializer Serializer => throw new System.NotImplementedException();

        public IDeserializer Deserializer => throw new System.NotImplementedException();

        public string[] AcceptedContentTypes => throw new System.NotImplementedException();

        public SupportsContentType SupportsContentType => throw new System.NotImplementedException();
    }


}
