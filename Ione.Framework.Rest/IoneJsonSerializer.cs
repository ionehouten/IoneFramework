using Newtonsoft.Json;
using System.IO;

namespace Ione.Framework.Rest
{
    /// <summary>
    /// IoneJsonSerializer
    /// </summary>
    public class IoneJsonSerializer : IJsonSerializer
    {
        private Newtonsoft.Json.JsonSerializer serializer;

        /// <summary>
        /// IoneJsonSerializer
        /// </summary>
        /// <param name="serializer"></param>
        public IoneJsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            this.serializer = serializer;
        }

        /// <summary>
        /// ContentType
        /// </summary>
        public string ContentType
        {
            get { return "application/json"; } // Probably used for Serialization?
            set { }
        }

        /// <summary>
        /// DateFormat
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        /// Namespace
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// RootElement
        /// </summary>
        public string RootElement { get; set; }

        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    serializer.Serialize(jsonTextWriter, obj);

                    return stringWriter.ToString();
                }
            }
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public T Deserialize<T>(RestSharp.IRestResponse response)
        {
            var content = response.Content;

            using (var stringReader = new StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        /// <summary>
        /// Default
        /// </summary>
        public static IoneJsonSerializer Default
        {
            get
            {
                return new IoneJsonSerializer(new Newtonsoft.Json.JsonSerializer()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            }
        }
    }
}
