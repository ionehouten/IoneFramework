using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;

namespace Belant.Framework.Rest
{
    /// <summary>
    /// OutputParameters
    /// Untuk output hasil dari select data, data di Convert menjadi T
    /// </summary>
    /// <typeparam name="T">Class</typeparam>
    public class OutputParameters<T> : OutputResponse  where T : new() 
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public new T Data { get; set; }

        /// <summary>
        /// JsonDeserialize
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public new OutputParameters<T> JsonDeserialize(IRestResponse response)
        {
            try
            {
                var json = new JsonDeserializer();
                return json.Deserialize<OutputParameters<T>>(response);
            }
            catch(Exception ex)
            {
                if(ex != null)
                {
                    throw new Exception(ex.Message + Environment.NewLine + response.Content, ex);
                }
                else
                {
                    throw new Exception("Error Deserializing " + response.Content);
                }
            }

        }

        /// <summary>
        /// XmlDeserialize
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public new OutputParameters<T> XmlDeserialize(IRestResponse response)
        {
            try
            {
                var xml = new XmlDeserializer();
                return xml.Deserialize<OutputParameters<T>>(response);
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    throw new Exception(ex.Message + Environment.NewLine + response.Content, ex);
                }
                else
                {
                    throw new Exception("Error Deserializing " + response.Content);
                }
            }
        }
    }
}
