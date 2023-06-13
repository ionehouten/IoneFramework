using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Ione.Framework.Core
{
    /// <summary>
    /// Extension 
    /// Berisi fungsi-fungsi extension data object
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// GetInstance
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// GetInstanceList
        /// </summary>
        /// <param name="type"></param>
        /// <returns>IList</returns>
        public static IList GetInstanceList(Type type)
        {
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(type);

            return (IList)Activator.CreateInstance(constructedListType);
        }

     

        /// <summary>
        /// Untuk melakukan copy pada object yang berbeda
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parent"></param>
        public static void CopyPropertiesFrom(this object self, object parent)
        {
            var fromProperties = parent.GetType().GetProperties();
            var toProperties = self.GetType().GetProperties();
            foreach (var fromProperty in fromProperties)
            {
                foreach (var toProperty in toProperties)
                {
                    if (fromProperty.Name == toProperty.Name && fromProperty.PropertyType == toProperty.PropertyType)
                    {
                        toProperty.SetValue(self, fromProperty.GetValue(parent)); break;
                    }
                }
            }
        }

        /// <summary>
        /// Untuk melakukan copy pada object yang berbeda dengan menggunakan atrribut 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parent"></param>
        public static void MatchPropertiesFrom(this object self, object parent)
        {
            var childProperties = self.GetType().GetProperties();
            foreach (var childProperty in childProperties)
            {
                var attributesForProperty = childProperty.GetCustomAttributes(typeof(MatchParentAttribute), true);
                var isOfTypeMatchParentAttribute = false;
                MatchParentAttribute currentAttribute = null;
                foreach (var attribute in attributesForProperty)
                {
                    if (attribute.GetType() == typeof(MatchParentAttribute))
                    {
                        isOfTypeMatchParentAttribute = true;
                        currentAttribute = (MatchParentAttribute)attribute; break;
                    }
                }
                if (isOfTypeMatchParentAttribute)
                {
                    var parentProperties = parent.GetType().GetProperties();
                    object parentPropertyValue = null;
                    foreach (var parentProperty in parentProperties)
                    {
                        if (parentProperty.Name == currentAttribute.ParentPropertyName)
                        {
                            if (parentProperty.PropertyType == childProperty.PropertyType)
                            {
                                parentPropertyValue = parentProperty.GetValue(parent);
                            }
                        }
                    }
                    childProperty.SetValue(self, parentPropertyValue);
                }
            }
        }

        /// <summary>
        /// Untuk convert hashtable ke T 
        /// </summary>
        /// <typeparam name="T">Object/CLass</typeparam>
        /// <param name="data">Data dari Hashtable</param>
        /// <param name="defaultData">Default data dari T</param>
        /// <returns></returns>
        public static T ConvertTo<T>(this Hashtable data, object defaultData = null) where T : new()
        {
            var output = new T();
            //Definisi atribut yang akan dibaca
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            if(defaultData != null)
            {
                //Baca atribut Name dan Type dari defaultData
                var propertiesDefaultData = defaultData.GetType().GetProperties(flags).Cast<PropertyInfo>().
                    Select(item => new
                    {
                        Name = item.Name,
                        Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                    }).ToList();

                //isi data ke class baru dari default data
                foreach (var item in propertiesDefaultData)
                {
                    PropertyInfo propertyInfoOutput = output.GetType().GetProperty(item.Name);
                    PropertyInfo propertyInfoDefaultData = defaultData.GetType().GetProperty(item.Name);
                    if (propertyInfoOutput != null)
                    {
                        propertyInfoOutput.SetValue(output, propertyInfoDefaultData.GetValue(defaultData), null);
                    }
                }
            }
            if(data != null)
            {
                //isi data ke class baru dari hashtable data
                foreach (DictionaryEntry item in data)
                {
                    PropertyInfo propertyInfo = output.GetType().GetProperty(item.Key.ToString());
                    //jika propertyInfo tidak null dan jika value dari class baru null, maka isi data dari hashtable
                    if (propertyInfo != null && propertyInfo.GetValue(output) == null)
                    {
                        propertyInfo.SetValue(output, item.Value, null);
                    }

                }
            }
            return output;
        }

        /// <summary>
        /// Untuk convert hashtable ke object 
        /// </summary>
        /// <param name="data">Data dari Hashtable</param>
        /// <param name="type">Type dari return object</param>
        /// <param name="defaultData">Default data dari type tersebut</param>
        /// <returns></returns>
        public static object ConvertTo(this Hashtable data, Type type, object defaultData = null)
        {
            var output = Activator.CreateInstance(type);
            //Definisi atribut yang akan dibaca
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            if (defaultData != null)
            {
                //Baca atribut Name dan Type dari defaultData
                var propertiesDefaultData = defaultData.GetType().GetProperties(flags).Cast<PropertyInfo>().
                    Select(item => new
                    {
                        Name = item.Name,
                        Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                    }).ToList();

                //isi data ke class baru dari default data
                foreach (var item in propertiesDefaultData)
                {
                    PropertyInfo propertyInfoOutput = output.GetType().GetProperty(item.Name);
                    PropertyInfo propertyInfoDefaultData = defaultData.GetType().GetProperty(item.Name);
                    if (propertyInfoOutput != null)
                    {
                        //propertyInfoOutput.SetValue(output, propertyInfoDefaultData.GetValue(defaultData), null);
                        propertyInfoOutput.SetPropertyValue(output, propertyInfoDefaultData.GetValue(defaultData));
                    }
                }
            }
            if (data != null)
            {
                //isi data ke class baru dari hashtable data
                foreach (DictionaryEntry item in data)
                {
                    PropertyInfo propertyInfo = output.GetType().GetProperty(item.Key.ToString());
                    //jika propertyInfo tidak null dan jika value dari class baru null, maka isi data dari hashtable
                    if (propertyInfo != null && propertyInfo.GetValue(output) == null)
                    {
                        //propertyInfo.SetValue(output, item.Value, null);
                        propertyInfo.SetPropertyValue(output, item.Value);
                    }

                }
            }
            return output;
        }

        /// <summary>
        /// SetPropertyValue 
        /// Untuk mengisi nilai dari properti
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="instance"></param>
        /// <param name="value"></param>
        /// <param name="index"></param>
        public static void SetPropertyValue(this PropertyInfo propertyInfo, object instance,object value, object[] index = null)
        {
            try
            {
                if (instance == null) return;
                if (value == null) return;
                if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
                {
                    propertyInfo.SetValue(instance, Converter.ToDateTime(value), index);
                }
                else if (propertyInfo.PropertyType == typeof(Int16) || propertyInfo.PropertyType == typeof(UInt16))
                {
                    propertyInfo.SetValue(instance, Converter.ToInt16(value), index);
                }
                else if (propertyInfo.PropertyType == typeof(Int16?) || propertyInfo.PropertyType == typeof(UInt16?))
                {
                    propertyInfo.SetValue(instance, Converter.ToInt16Null(value), index);
                }
                else if (propertyInfo.PropertyType == typeof(Int32) || propertyInfo.PropertyType == typeof(UInt32))
                {
                    propertyInfo.SetValue(instance, Converter.ToInt32(value), index);
                }
                else if (propertyInfo.PropertyType == typeof(Int32?) || propertyInfo.PropertyType == typeof(UInt32?))
                {
                    propertyInfo.SetValue(instance, Converter.ToInt32Null(value), index);
                }
                else if (propertyInfo.PropertyType == typeof(Int64) || propertyInfo.PropertyType == typeof(UInt64))
                {
                    propertyInfo.SetValue(instance, Converter.ToInt64(value), index);
                }
                else if (propertyInfo.PropertyType == typeof(Int64?) || propertyInfo.PropertyType == typeof(UInt64?))
                {
                    propertyInfo.SetValue(instance, Converter.ToInt64Null(value), index);
                }
                else if (propertyInfo.PropertyType == typeof(Decimal))
                {
                    propertyInfo.SetValue(instance, Converter.ToDecimal(value), index);
                }
                else if (propertyInfo.PropertyType == typeof(Decimal?))
                {
                    propertyInfo.SetValue(instance, Converter.ToDecimalNull(value), index);
                }
                else if (propertyInfo.PropertyType == typeof(Double))
                {
                    propertyInfo.SetValue(instance, Converter.ToDouble(value), index);
                }
                else if (propertyInfo.PropertyType == typeof(Double?))
                {
                    propertyInfo.SetValue(instance, Converter.ToDoubleNull(value), index);
                }
                else if (propertyInfo.PropertyType == typeof(float))
                {
                    propertyInfo.SetValue(instance, Converter.ToDecimal(value), index);
                }
                else if (propertyInfo.PropertyType == typeof(float?))
                {
                    propertyInfo.SetValue(instance, Converter.ToDecimalNull(value), index);
                }
                else if (propertyInfo.PropertyType == typeof(Boolean))
                {
                    propertyInfo.SetValue(instance, Converter.ToBoolean(value), index);
                }
                else if (propertyInfo.PropertyType == typeof(String))
                {
                    if (value.GetType() == typeof(DateTime))
                    {
                        propertyInfo.SetValue(instance, Converter.FormatDate(value), index);
                    }
                    else
                    {
                        propertyInfo.SetValue(instance, Converter.ToString(value), index);
                    }
                }
            }
            catch
            {
            }
        }

        public static bool IsHex(this string input)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"\A\b[0-9a-fA-F]+\b\Z");
        }

        /// <summary>
        /// PropertyCopier
        /// </summary>
        /// <typeparam name="settings"></typeparam>
        /// <typeparam name="name"></typeparam>
        public static bool IsPropertyExist(dynamic settings, string name)
        {
            if (settings is ExpandoObject)
                return ((IDictionary<string, object>)settings).ContainsKey(name);

            return settings.GetType().GetProperty(name) != null;
        }
    }

    /// <summary>
    /// PropertyMatcher
    /// </summary>
    /// <typeparam name="TParent"></typeparam>
    /// <typeparam name="TChild"></typeparam>
    public class PropertyMatcher<TParent, TChild> where TParent : class where TChild : class
    {
        /// <summary>
        /// GenerateMatchedObject
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public static void GenerateMatchedObject(TParent parent, TChild child)
        {
            var childProperties = child.GetType().GetProperties();
            foreach (var childProperty in childProperties)
            {
                var attributesForProperty = childProperty.GetCustomAttributes(typeof(MatchParentAttribute), true); var isOfTypeMatchParentAttribute = false; MatchParentAttribute currentAttribute = null; foreach (var attribute in attributesForProperty) { if (attribute.GetType() == typeof(MatchParentAttribute)) { isOfTypeMatchParentAttribute = true; currentAttribute = (MatchParentAttribute)attribute; break; } }
                if (isOfTypeMatchParentAttribute)
                {
                    var parentProperties = parent.GetType().GetProperties();
                    object parentPropertyValue = null;
                    foreach (var parentProperty in parentProperties)
                    {
                        if (parentProperty.Name == currentAttribute.ParentPropertyName)
                        {
                            if (parentProperty.PropertyType == childProperty.PropertyType)
                            {
                                parentPropertyValue = parentProperty.GetValue(parent);
                            }
                        }
                    }
                    childProperty.SetValue(child, parentPropertyValue);
                }
            }
        }
    }

    /// <summary>
    /// PropertyCopier
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    public class PropertyCopier<TSource, TTarget> where TSource : class where TTarget : class
    {
        /// <summary>
        /// Copy
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public static void Copy(TSource parent, TTarget child)
        {
            var parentProperties = parent.GetType().GetProperties();
            var childProperties = child.GetType().GetProperties();

            foreach (var parentProperty in parentProperties)
            {
                foreach (var childProperty in childProperties)
                {
                    if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                    {
                        childProperty.SetValue(child, parentProperty.GetValue(parent));
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Copy
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public static void Merge(TSource parent, TTarget child)
        {
            var parentProperties = parent.GetType().GetProperties();
            var childProperties = child.GetType().GetProperties();

            foreach (var parentProperty in parentProperties)
            {
                foreach (var childProperty in childProperties)
                {
                    if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                    {
                        if(parentProperty.GetValue(parent) != null)
                        {
                            childProperty.SetValue(child, parentProperty.GetValue(parent));
                        }
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Copy
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public static void Merge(TSource parent, TTarget child, string[] nullCopy)
        {
            var parentProperties = parent.GetType().GetProperties();
            var childProperties = child.GetType().GetProperties();

            foreach (var parentProperty in parentProperties)
            {
                foreach (var childProperty in childProperties)
                {
                    if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                    {
                        if (parentProperty.GetValue(parent) != null)
                        {
                            childProperty.SetValue(child, parentProperty.GetValue(parent));
                        }
                        break;
                    }
                }
            }
        }
    }

    



}
