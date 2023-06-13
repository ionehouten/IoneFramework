using Ione.Framework.Core;
using RestSharp;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Ione.Framework.Rest
{
    /// <summary>
    /// RestConverter
    /// </summary>
    public static partial class RestConverter
    {
        /// <summary>
        /// ToHashtable
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="checkObject"></param>
        /// <returns></returns>
        public static Hashtable ToHashtable(this object instance, bool checkObject = true)
        {
            Hashtable parameters = new Hashtable();
            if (instance == null) return parameters;
            Type type = instance.GetType();

            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var properties = type.GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();
            

            foreach (var item in properties)
            {
                PropertyInfo property = type.GetProperty(item.Name);
                if(property.PropertyType.Name != "Object" || checkObject == false)
                {
                    var jsonPropertyAttribute = property.GetJsonPropertyAttribute();
                    string name = (jsonPropertyAttribute != null) ? jsonPropertyAttribute.Name : item.Name;

                    Object value = property.GetValue(instance);
                    if (value != null || checkObject == false)
                    {
                        parameters.Add(name, value);
                    }

                }
                else
                {
                    Hashtable value = property.GetValue(instance).ToHashtable(false);
                    foreach(DictionaryEntry pair in value)
                    {
                        parameters.Add(pair.Key, pair.Value);
                    }
                }
                

            }

            return parameters;
        }



        /// <summary>
        /// GetSerializeAsAttribute
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static JsonPropertyNameAttribute GetJsonPropertyAttribute(this PropertyInfo property)
        {
            return (JsonPropertyNameAttribute)property.GetCustomAttribute(typeof(JsonPropertyNameAttribute));
        }


        

        /// <summary>
        /// JsonDeserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(RestResponse response) where T : new()
        {
            try
            {
                return JsonSerializer.Deserialize<T>(response.Content);
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
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public static T XmlDeserialize<T>(RestResponse response) where T : new()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                using TextReader reader = new StringReader(response.Content);
                return (T)serializer.Deserialize(reader);


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
        /// JsonSerialize
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string JsonSerialize(this object input)
        {
            try
            {
                return JsonSerializer.Serialize(input);
            }
            catch (Exception ex)
            {
                ex.SaveLog();
                return "";
            }
        }

        /// <summary>
        /// XmlSerialize
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string XmlSerialize(this object input)
        {
            try
            {
                var xml = new XmlSerializer(input.GetType());
                string data = "";
                using (var sww = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(sww))
                    {
                        xml.Serialize(writer, input);
                        data = sww.ToString(); // Your XML
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                ex.SaveLog();
                return "";
            }
        }

    }
}
