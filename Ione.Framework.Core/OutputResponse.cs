using RestSharp;

namespace Belant.Framework.Core
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
        /// Error
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Count
        /// </summary>
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
        public OutputResponse JsonDeserialize(RestResponse response)
        {
            try
            {
                var json = new JsonNetSerializer();
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
                var xml = new RestSharp.Deserializers.DotNetXmlDeserializer();
                return xml.Deserialize<OutputResponse>(response);
            }
            catch
            {
                return null;
            }
        }


    }
}
