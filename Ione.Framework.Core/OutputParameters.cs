using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;

namespace Belant.Framework.Core
{
    /// <summary>
    /// OutputParameters
    /// Untuk output hasil dari select data, data di Convert menjadi T
    /// </summary>
    /// <typeparam name="T">Class</typeparam>
    public class OutputParameters<T> : OutputResponse 
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public new T Data { get; set; }

        ///// <summary>
        ///// JsonDeserialize
        ///// </summary>
        ///// <param name="response"></param>
        ///// <returns></returns>
        //public new OutputParameters<T> JsonDeserialize(IRestResponse response)
        //{
        //    try
        //    {
        //        var json = new JsonNetSerializer();
        //        return json.Deserialize<OutputParameters<T>>(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex != null)
        //        {
        //            throw new Exception(ex.Message + Environment.NewLine + response.Content, ex);
        //        }
        //        else
        //        {
        //            throw new Exception("Error Deserializing " + response.Content);
        //        }
        //    }

        //}

        ///// <summary>
        ///// XmlDeserialize
        ///// </summary>
        ///// <param name="response"></param>
        ///// <returns></returns>
        //public new OutputParameters<T> XmlDeserialize(IRestResponse response)
        //{
        //    try
        //    {
        //        var xml = new DotNetXmlDeserializer();
        //        return xml.Deserialize<OutputParameters<T>>(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex != null)
        //        {
        //            throw new Exception(ex.Message + Environment.NewLine + response.Content, ex);
        //        }
        //        else
        //        {
        //            throw new Exception("Error Deserializing " + response.Content);
        //        }
        //    }
        //}
    }

    public class OutputParameters<TData, TOutput> where TData : new() where TOutput : new()
    {
        public object Id { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        [JsonProperty("record_count")]
        public int Count { get; set; }
        public string ElapsedTime { get; set; }
        public TData Data { get; set; }
        public OutputParameterError Error { get; set; }

        /// <summary>
        /// JsonDeserialize
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public TOutput JsonDeserialize(IRestResponse response)
        {
            try
            {
                var json = new JsonNetSerializer();
                return json.Deserialize<TOutput>(response);
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

        /// <summary>
        /// XmlDeserialize
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public OutputParameters<TOutput> XmlDeserialize(IRestResponse response)
        {
            try
            {
                var xml = new DotNetXmlDeserializer();
                return xml.Deserialize<OutputParameters<TOutput>>(response);
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

    public class OutputParameterError
    {
        public string File { get; set; }
        public string Line { get; set; }
        public string Msg { get; set; }
    }
}
