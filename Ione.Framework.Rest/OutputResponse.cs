using RestSharp;
using RestSharp.Deserializers;

namespace Belant.Framework.Rest
{
    /// <summary>
    /// OutputResponse
    /// Untuk output response dari rest
    /// </summary>
    public class OutputResponse
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }


        /// <summary>
        /// Count
        /// </summary>
        //[DeserializeAs(Name = "all_record")]
        public int Count { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// JsonDeserialize
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public OutputResponse JsonDeserialize(IRestResponse response)
        {
            try
            {
                var json = new RestSharp.Deserializers.JsonDeserializer();
                return json.Deserialize<OutputResponse>(response);

            }
            catch
            {
                return null;

            }

        }
        /// <summary>
        /// XmlDeserialize
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public OutputResponse XmlDeserialize(IRestResponse response)
        {
            try
            {
                var xml = new RestSharp.Deserializers.XmlDeserializer();
                return xml.Deserialize<OutputResponse>(response);
            }
            catch
            {
                return null;
            }
        }


    }
}
