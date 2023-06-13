using Belant.Framework.Core;
using Belant.Framework.Core.Authenticator;
using Belant.Framework.Core.Logs;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


namespace Belant.Framework.Rest
{
    /// <summary>
    /// Model
    /// REST Model
    /// </summary>
    public abstract partial class Model : IModel, IModelType
    {
        private Hashtable DefaultRequestHeader;

        /// <summary>
        /// SetRequestHeader
        /// </summary>
        public bool SetRequestHeader = false;
        /// <summary>
        /// IncludeHeaderIpAddress
        /// </summary>
        public bool SendIpAddress = false;
        /// <summary>
        /// IncludeHeaderDrive
        /// </summary>
        public bool SendHardware = false;
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// ModelOption
        /// Jenis Model
        /// </summary>
        public ModelOptionType ModelOption
        {
            get { return ModelOptionType.REST; }
        }
        /// <summary>
        /// Authenticator
        /// </summary>
        public IAuthenticator Authenticator;

        /// <summary>
        /// Rest Client
        /// </summary>
        public IRestClient Client;

        /// <summary>
        /// Rest Request
        /// </summary>
        public RestRequest Request;

        /// <summary>
        /// Rest Response
        /// </summary>
        public IRestResponse Response;

        /// <summary>
        /// UseRpc
        /// Pengaturan request dengan XML RPC
        /// Default = false
        /// </summary>
        public bool UseRpc = false;

        /// <summary>
        /// RestClient
        /// Rest Server URL
        /// </summary>
        public abstract string RestClient { get; }

        /// <summary>
        /// RequestFormat
        /// Format Request = DataFormat.Json/DataFormat.Xml
        /// Default = DataFormat.Json
        /// </summary>
        public DataFormat RequestFormat = DataFormat.Json;

        /// <summary>
        /// JsonSerializerSettings
        /// </summary>
        public JsonSerializerSettings JsonSerializerSettings = null;

        /// <summary>
        /// RequestHeader
        /// </summary>
        public Hashtable RequestHeader;

        /// <summary>
        /// RequestParameter
        /// </summary>
        public Hashtable RequestParameter;
        /// <summary>
        /// RequestNamespace
        /// </summary>
        public string XmlNamespace = "http://www.belant.net/framework";

        /// <summary>
        /// UrlSegment
        /// </summary>
        public Hashtable UrlSegment;

        /// <summary>
        /// SelectInput
        /// Object input untuk select data
        /// </summary>
        public static object SelectInput;

        /// <summary>
        /// CrudInput
        /// Object input untuk CRUD data
        /// </summary>
        public static object CrudInput;

        /// <summary>
        /// SelectClient
        /// URI Select
        /// </summary>
        public string SelectClient;

        /// <summary>
        /// SelectOutput
        /// Type output untuk select data
        /// </summary>
        public Type SelectOutput;

        /// <summary>
        /// SelectRow
        /// Type untuk row select data
        /// </summary>
        public Type SelectRow { get; set; }

        /// <summary>
        /// SelectRootElement
        /// RootElement untuk request format XML
        /// </summary>
        public string SelectRootElement;

        /// <summary>
        /// SelectMethod
        /// Method untuk select data
        /// Default = Method.GET
        /// </summary>
        public Method SelectMethod = Method.GET;

        /// <summary>
        /// SumInput
        /// Object input untuk summary data
        /// </summary>
        public static object SumInput;

        /// <summary>
        /// SumClient
        /// URI Summary
        /// </summary>
        public string SumClient;

        /// <summary>
        /// SumOutput
        /// Type output untuk summary data
        /// </summary>
        public Type SumOutput;

        /// <summary>
        /// SumRow
        /// Type untuk row summary data
        /// </summary>
        public Type SumRow;

        /// <summary>
        /// SumRootElement
        /// RootElement untuk request format XML
        /// </summary>
        public string SumRootElement;

        /// <summary>
        /// SumMethod
        /// Method untuk summary data
        /// Default = Method.GET
        /// </summary>
        public Method SumMethod = Method.GET;

        /// <summary>
        /// CreateInput
        /// Object input untuk create data
        /// </summary>
        public static object CreateInput;

        /// <summary>
        /// CreateClient
        /// URI Create
        /// </summary>
        public string CreateClient;

        /// <summary>
        /// CreateOutput
        /// Type output untuk create data
        /// Default = typeof(OutputResponse)
        /// </summary>
        public Type CreateOutput =  typeof(OutputResponse);

        /// <summary>
        /// CreateRootElement
        /// RootElement untuk request format XML
        /// </summary>
        public string CreateRootElement;

        /// <summary>
        /// CreateMethod
        /// Method untuk summary data
        /// Default = Method.POST
        /// </summary>
        public Method CreateMethod = Method.POST;

        /// <summary>
        /// UpdateInput
        /// Object input untuk update data
        /// </summary>
        public static object UpdateInput;

        /// <summary>
        /// UpdateClient
        /// URI Update
        /// </summary>
        public string UpdateClient;

        /// <summary>
        /// UpdateOutput
        /// Type output untuk update data
        /// Default = typeof(OutputResponse)
        /// </summary>
        public Type UpdateOutput = typeof(OutputResponse);

        /// <summary>
        /// UpdateRootElement
        /// RootElement untuk request format XML
        /// </summary>
        public string UpdateRootElement;

        /// <summary>
        /// UpdateMethod
        /// Method untuk summary data
        /// Default = Method.PUT
        /// </summary>
        public Method UpdateMethod = Method.PUT;

        /// <summary>
        /// DeleteInput
        /// Object input untuk delete data
        /// </summary>
        public static object DeleteInput;

        /// <summary>
        /// DeleteClient
        /// URI Delete
        /// </summary>
        public string DeleteClient;

        /// <summary>
        /// DeleteOutput
        /// Type output untuk delete data
        /// Default = typeof(OutputResponse)
        /// </summary>
        public Type DeleteOutput = typeof(OutputResponse);

        /// <summary>
        /// DeleteRootElement
        /// RootElement untuk request format XML
        /// </summary>
        public string DeleteRootElement;

        /// <summary>
        /// DeleteMethod
        /// Method untuk summary data
        /// Default = Method.DELETE
        /// </summary>
        public Method DeleteMethod = Method.DELETE;

        /// <summary>
        /// UploadInput
        /// Object input untuk upload data
        /// </summary>
        public static object UploadInput;

        /// <summary>
        /// UploadClient
        /// URI Upload
        /// </summary>
        public string UploadClient;

        /// <summary>
        /// UploadOutput
        /// Type output untuk upload data
        /// Default = typeof(OutputResponse)
        /// </summary>
        public Type UploadOutput = typeof(OutputResponse);

        /// <summary>
        /// UploadRootElement
        /// RootElement untuk request format XML
        /// </summary>
        public string UploadRootElement;

        /// <summary>
        /// UploadMethod
        /// Method untuk summary data
        /// Default = Method.PUT
        /// </summary>
        public Method UploadMethod = Method.POST;

        /// <summary>
        /// DownloadInput
        /// Object input untuk download data
        /// </summary>
        public static object DownloadInput;

        /// <summary>
        /// DownloadClient
        /// URI Download
        /// </summary>
        public string DownloadClient;

        /// <summary>
        /// DownloadOutput
        /// Type output untuk download data
        /// Default = typeof(byte[])
        /// </summary>
        public Type DownloadOutput = typeof(byte[]);

        /// <summary>
        /// DownloadRootElement
        /// RootElement untuk request format XML
        /// </summary>
        public string DownloadRootElement;

        /// <summary>
        /// DownloadMethod
        /// Method untuk summary data
        /// Default = Method.GET
        /// </summary>
        public Method DownloadMethod = Method.GET;

        /// <summary>
        /// ExecuteClient
        /// URI Execute
        /// </summary>
        public string ExecuteClient;

        /// <summary>
        /// ExecuteOutput
        /// Type output untuk execute data
        /// Default = typeof(OutputResponse)
        /// </summary>
        public Type ExecuteOutput = typeof(OutputResponse);

        /// <summary>
        /// ExecuteRootElement
        /// RootElement untuk request format XML
        /// </summary>
        public string ExecuteRootElement;

        /// <summary>
        /// ExecuteMethod
        /// Method untuk summary data
        /// Default = Method.POST
        /// </summary>
        public Method ExecuteMethod = Method.POST;

        /// <summary>
        /// SelectCount
        /// Total data dari output response
        /// </summary>
        public int SelectCount { get; set; }

        /// <summary>
        /// SelectList
        /// List data dari output response
        /// </summary>
        public IList SelectList { get; set; }

        /// <summary>
        /// TotalRecords
        /// Seluruh total record
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// EnabledLog
        /// </summary>
        public bool EnabledLog { get; set; }

        /// <summary>
        /// ExecutionTime
        /// </summary>
        public Stopwatch ExecutionTime { get; set; }

        /// <summary>
        /// ExecutionStatus
        /// </summary>
        public bool ExecutionStatus { get; set; }

        /// <summary>
        /// ExecutionError
        /// </summary>
        public string ExecutionError { get; set; }
        
        /// <summary>
        /// Model
        /// </summary>
        public Model()
        {
            JsonSerializerSettings = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };

        }
        /// <summary>
        /// StartExecution
        /// </summary>
        public void StartExecution()
        {
            ExecutionTime = new Stopwatch();
            ExecutionTime.Start();
            ExecutionStatus = true;
            ExecutionError = string.Empty;
        }
        /// <summary>
        /// StopExecution
        /// </summary>
        public void StopExecution()
        {
            ExecutionTime.Stop();
        }
        /// <summary>
        /// ErrorExecution
        /// </summary>
        /// <param name="message"></param>
        public void ErrorExecution(string message)
        {
            ExecutionStatus = false;
            ExecutionError = message;
        }


        /// <summary>
        /// SetRequestHeaders
        /// </summary>
        private void SetRequestHeaders()
        {
            this.DefaultRequestHeader = new Hashtable();

            if (this.Authenticator != null)
            {
                if(this.Authenticator.GetType() == typeof(OAuth1Authenticator))
                {
                    OAuth1Authenticator authenticator = this.Authenticator as OAuth1Authenticator;
                    authenticator.Url = this.Client.BuildUri(this.Request);
                    authenticator.Timestamp = OAuth.GenerateTimeStamp();
                    authenticator.Nonce = OAuth.GenerateNonce();
                    authenticator.Method = this.Request.Method.ToString();
                    authenticator.Version = "1.0";
                    //string normalizedUrl;
                    //string normalizedRequestParameters;
                    authenticator.Signature = new OAuth().GenerateSignature(
                        authenticator.Url,
                        authenticator.ConsumerKey,
                        authenticator.ConsumerSecret,
                        authenticator.AccessToken,
                        authenticator.TokenSecret,
                        authenticator.Method,
                        authenticator.Timestamp,
                        authenticator.Nonce,
                        authenticator.SignatureMethod,
                        null,
                        out _, //normalizedUrl
                        out _ //normalizedRequestParameters
                        );
                    string auth = string.Format("OAuth oauth_consumer_key=\"{0}\",oauth_token=\"{1}\",oauth_signature_method=\"{2}\",oauth_timestamp=\"{3}\",oauth_nonce=\"{4}\",oauth_version=\"{5}\",oauth_signature=\"{6}\",oauth_url=\"{7}\"",
                             authenticator.ConsumerKey,
                             authenticator.AccessToken,
                             authenticator.SignatureMethod.GetSignaturType(),
                             authenticator.Timestamp,
                             authenticator.Nonce,
                             authenticator.Version,
                             authenticator.Signature,
                             authenticator.Url
                             );

                    this.DefaultRequestHeader.Add("Authorization", auth);
                }
                else if (this.Authenticator.GetType() == typeof(BasicAuthenticator))
                {
                    BasicAuthenticator authenticator = this.Authenticator as BasicAuthenticator;
                    string auth = string.Format("Basic {0}", BasicAuthenticator.Encode(authenticator));

                    this.DefaultRequestHeader.Add("Authorization", auth);
                }

                else if (this.Authenticator.GetType() == typeof(JwtAuthenticator))
                {
                    JwtAuthenticator authenticator = this.Authenticator as JwtAuthenticator;
                    string auth = string.Format("Bearer {0}", authenticator.AccessToken);

                    this.DefaultRequestHeader.Add("Authorization", auth);
                }
            }
            
            //for windows
            //this.DefaultRequestHeader.Add("CpuId", Core.Security.CpuId);
            //this.DefaultRequestHeader.Add("HardDrive", Core.Security.HardDrive);
            //this.DefaultRequestHeader.Add("VolumeSerial", Core.Security.VolumeSerial);

            this.DefaultRequestHeader.Add("MacAddress", Core.Security.MacAddress);
            this.DefaultRequestHeader.Add("AssemblyName", Core.Security.AssemblyName);
            this.DefaultRequestHeader.Add("AssemblyVersion", Core.Security.AssemblyVersion);
           

            if(SendIpAddress)
                this.DefaultRequestHeader.Add("IpAddress", Core.Security.IpAddress);

            if(SendHardware)
            {
                try
                {
                    this.DefaultRequestHeader.Add("TotalCpuUsage", Core.Security.TotalCpuUsage);
                    this.DefaultRequestHeader.Add("TotalMemoryAvailable", Core.Security.TotalMemoryAvailable);
                    this.DefaultRequestHeader.Add("TotalPhysicalMemory", Core.Security.TotalPhysicalMemory);
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        if (drive.IsReady)
                        {
                            this.DefaultRequestHeader.Add("Drive" + drive.Name.Replace(":\\", "") + "TotalSize", drive.TotalSize.FormatBytes());
                            this.DefaultRequestHeader.Add("Drive" + drive.Name.Replace(":\\", "") + "TotalFreeSpace", drive.TotalFreeSpace.FormatBytes());
                            this.DefaultRequestHeader.Add("Drive" + drive.Name.Replace(":\\", "") + "TotalUsedSpace", (drive.TotalSize - drive.TotalFreeSpace).FormatBytes());
                        }
                    }
                }
                catch { }
            }
           

            switch (this.RequestFormat)
            {
                case DataFormat.Json:
                    this.DefaultRequestHeader.Add("Accept", "application/json");
                    break;
                case DataFormat.Xml:
                    this.DefaultRequestHeader.Add("Accept", "application/xml");
                    break;
            }
            foreach (DictionaryEntry item in this.DefaultRequestHeader)
            {
                this.Request.AddHeader(Core.Converter.ToString(item.Key), Core.Converter.ToString(item.Value));
            }
            if(this.RequestHeader != null)
            {
                foreach (DictionaryEntry item in this.RequestHeader)
                {
                    this.Request.AddHeader(Core.Converter.ToString(item.Key), Core.Converter.ToString(item.Value));
                }

            }

        }

        /// <summary>
        /// SetRequestParameters
        /// </summary>
        /// <param name="input"></param>
        private void SetRequestParameters(dynamic input)
        {
            if (input != null)
            {
                if (this.UseRpc)
                {
                    switch (this.RequestFormat)
                    {
                        case DataFormat.Json:
                            string jsonBody = JsonConvert.SerializeObject(input, JsonSerializerSettings);
                            //jsonBody = jsonBody.Replace(" 00:00:00", "");
                            //jsonBody = jsonBody.Replace(":\"", ":");
                            //jsonBody = jsonBody.Replace("\",", ",");

                            //jsonBody = jsonBody.Replace("\":", "\": \"");
                            //jsonBody = jsonBody.Replace(",\"", "\",\"");
                            //jsonBody = jsonBody.Replace("\"[", "[");
                            //jsonBody = jsonBody.Replace("]\"", "]");
                            //jsonBody = jsonBody.Replace("\"{", "{");
                            //jsonBody = jsonBody.Replace("}\"", "}");
                            //jsonBody = jsonBody.Replace("\"null\"", "null");
                            //jsonBody = jsonBody.Replace("\"null", "null");
                            this.Request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
                            break;
                        case DataFormat.Xml:
                            this.Request.AddXmlBody(input, this.XmlNamespace);
                            break;
                    }
                    this.AddRequestParameters(((object)input).ToHashtable(),true);
                }
                else
                {
                    this.AddRequestParameters(((object)input).ToHashtable(), false);

                    try
                    {
                        if(Extension.IsPropertyExist(input, "Data"))
                        {
                            if(input.Data != null)
                            {
                                this.AddRequestParameters(((object)input.Data).ToHashtable(), false);
                            }
                        }
                    }
                    catch { }
                }
            }

            if (this.RequestParameter != null)
            {
                foreach (DictionaryEntry item in this.RequestParameter)
                {
                    this.Request.AddParameter(Core.Converter.ToString(item.Key), Core.Converter.ToString(item.Value));
                }

            }
        }

        /// <summary>
        /// AddRequestParameters
        /// </summary>
        /// <param name="data"></param>
        /// <param name="isRpc"></param>
        private void AddRequestParameters(Hashtable data, bool isRpc)
        {
            foreach (DictionaryEntry item in data)
            {
                if ((item.Key as string) != "Data" && item.Value != null)
                {
                    if (item.Value.GetType() == typeof(List<InputFile>))
                    {
                        foreach (InputFile file in (List<InputFile>)item.Value)
                        {
                            this.Request.AddFile(file.Name, file.Path, file.Type);
                        }

                    }
                    else if (item.Value.GetType() == typeof(InputFile))
                    {
                        InputFile file = item.Value as InputFile;
                        this.Request.AddFile(file.Name, file.Path, file.Type);
                    }
                    else if (item.Value.GetType() == typeof(DateTime?) || item.Value.GetType() == typeof(DateTime))
                    {
                        this.Request.AddParameter(Core.Converter.ToString(item.Key), item.Value.FormatDate(JsonSerializerSettings.DateFormatString));
                    }
                    else
                    {
                        if(!isRpc)
                        {
                            this.Request.AddParameter(Core.Converter.ToString(item.Key), item.Value);
                        }
                        if (this.Request.Method == Method.PUT || this.Request.Method == Method.DELETE || this.Request.Method == Method.GET)
                        {
                            this.Request.AddUrlSegment(Core.Converter.ToString(item.Key), item.Value.ToString());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Count
        /// Untuk mendapatkan seluruh total record
        /// </summary>
        /// <param name="input">CountInput</param>
        /// <param name="operation">FormOperationType</param>
        /// <returns>int</returns>
        public int Count(object input, FormOperationType operation = FormOperationType.Execute)
        {
            return this.TotalRecords;
        }


        /// <summary>
        /// Execute
        /// 
        /// </summary>
        /// <param name="input">CrudInput</param>
        /// <param name="operation">FormOperationType</param>
        /// <returns>CrudOutput</returns>
        [Obsolete]
        public async Task<object> ExecuteAsync(object input, FormOperationType operation = FormOperationType.Execute)
        {
            try
            {
                StartExecution();
                dynamic executeOutput;
                this.Client = new RestClient(this.RestClient);
                switch (operation)
                {
                    case FormOperationType.Create:
                        executeOutput = Activator.CreateInstance(CreateOutput);
                        if (this.CreateMethod == Method.GET) this.UseRpc = false;
                        this.Request = new RestRequest(this.CreateClient, this.CreateMethod);
                        break;
                    case FormOperationType.Update:
                        if (this.UpdateMethod == Method.GET) this.UseRpc = false;
                        executeOutput = Activator.CreateInstance(UpdateOutput);
                        this.Request = new RestRequest(this.UpdateClient, this.UpdateMethod);
                        break;
                    case FormOperationType.Delete:
                        if (this.DeleteMethod == Method.GET) this.UseRpc = false;
                        executeOutput = Activator.CreateInstance(DeleteOutput);
                        this.Request = new RestRequest(this.DeleteClient, this.DeleteMethod);
                        break;
                    default:
                        executeOutput = Activator.CreateInstance(ExecuteOutput);
                        this.Request = new RestRequest(this.ExecuteClient, this.ExecuteMethod);
                        break;
                }

                this.Request.RequestFormat = this.RequestFormat;
                this.SetRequestParameters(input);
                this.SetRequestHeaders();
                
                this.Response = await this.Client.ExecuteTaskAsync(this.Request);
                try
                {
                    var content = this.Response.Content.Replace("\"data\":[]", "\"data\":null");
                    this.Response.Content = content;
                    switch (this.RequestFormat)
                    {
                        case DataFormat.Json:
                            executeOutput = executeOutput.JsonDeserialize(this.Response);
                            break;
                        case DataFormat.Xml:
                            executeOutput = executeOutput.XmlDeserialize(this.Response);
                            break;
                    }
                }
                catch(Exception)
                {
                    if (this.Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        throw;
                    }
                    else
                    {
                        if (this.Response.ErrorException != null)
                        {
                            throw this.Response.ErrorException;
                        }
                        else
                        {
                            throw new Exception(string.Format("({0}) - {1}", this.Response.StatusCode.ToInt32(), this.Response.StatusDescription));
                        }
                    }
                }
                
                
                return executeOutput;
            }
            catch (Exception ex)
            {
                ErrorExecution(ex.Message);
                throw;
            }
            finally
            {
                StopExecution();
                try
                {
                    LogRequestAsync(this.Request, this.Response, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
                }
                catch { }
            }
        }


        /// <summary>
        /// Execute
        /// 
        /// </summary>
        /// <param name="operation">FormOperationType</param>
        /// <returns>CrudOutput</returns>
        public async Task<string> ExecuteAsync(FormOperationType operation = FormOperationType.Execute)
        {
            try
            {
                StartExecution();
                string executeOutput = string.Empty;
                this.Client = new RestClient(this.RestClient);
                switch (operation)
                {
                    case FormOperationType.Create:
                        this.Request = new RestRequest(this.CreateClient, this.CreateMethod);
                        break;
                    case FormOperationType.Update:
                        this.Request = new RestRequest(this.UpdateClient, this.UpdateMethod);
                        break;
                    case FormOperationType.Delete:
                        this.Request = new RestRequest(this.DeleteClient, this.DeleteMethod);
                        break;
                    default:
                        this.Request = new RestRequest(this.ExecuteClient, this.ExecuteMethod);
                        break;
                }
                this.Request.RequestFormat = this.RequestFormat;
                this.SetRequestParameters(null);
                this.SetRequestHeaders();

                CancellationToken cancellationToken = new CancellationToken();
                this.Response = await this.Client.ExecuteAsync<IRestResponse>(this.Request, cancellationToken);
                try
                {

                    var content = this.Response.Content.Replace("\"data\":[]", "\"data\":null");
                    executeOutput = content;
                }
                catch (Exception)
                {
                    if (this.Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        throw;
                    }
                    else
                    {
                        if (this.Response.ErrorException != null)
                        {
                            throw this.Response.ErrorException;
                        }
                        else
                        {
                            throw new Exception(string.Format("({0}) - {1}", this.Response.StatusCode.ToInt32(), this.Response.StatusDescription));
                        }
                    }
                }


                return executeOutput;
            }
            catch (Exception ex)
            {
                ErrorExecution(ex.Message);
                throw;
            }
            finally
            {
                StopExecution();
                try
                {
                    LogRequestAsync(this.Request, this.Response, null, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
                }
                catch { }
            }
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="input">SelectInput</param>
        /// <returns>object</returns>
        [Obsolete]
        public async Task<object> SelectAsync(object input) 
        {
            try
            {
                this.StartExecution();

                dynamic selectOutput = Activator.CreateInstance(SelectOutput);
                if (this.SelectMethod == Method.GET) this.UseRpc = false;
                this.Client  = new RestClient(this.RestClient);
                this.Request = new RestRequest(this.SelectClient, this.SelectMethod)
                {
                    RequestFormat = this.RequestFormat
                };
                this.SetRequestParameters(input);
                this.SetRequestHeaders();
                this.Response = await this.Client.ExecuteTaskAsync(this.Request);

                try
                {
                    var content = this.Response.Content.Replace("\"data\":[]", "\"data\":null");
                    this.Response.Content = content;
                    switch (this.RequestFormat)
                    {
                        case DataFormat.Json:
                            selectOutput = selectOutput.JsonDeserialize(this.Response);
                            break;
                        case DataFormat.Xml:
                            selectOutput = selectOutput.XmlDeserialize(this.Response);
                            break;
                    }
                }
                catch (Exception)
                {
                    if (this.Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        throw;
                    }
                    else
                    {
                        if (this.Response.ErrorException != null)
                        {
                            throw this.Response.ErrorException;
                        }
                        else
                        {
                            throw new Exception(string.Format("({0}) - {1}", this.Response.StatusCode.ToInt32(), this.Response.StatusDescription));
                        }
                    }
                }
                return selectOutput;

            }
            catch (Exception ex)
            {
                ErrorExecution(ex.Message);
                throw;
            }
            finally
            {
                StopExecution();
                try
                {
                    LogRequestAsync(this.Request, this.Response, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
                }
                catch { }
            }
        }


        /// <summary>
        /// Upload
        /// </summary>
        /// <param name="input">UploadInput</param>
        /// <param name="operation">FormOperationType</param>
        /// <returns>OutputResponse</returns>
        [Obsolete]
        public async Task<object> UploadAsync(object input, FormOperationType operation = FormOperationType.Execute)
        {
            try
            {
                StartExecution();
                dynamic executeOutput;
                this.Client = new RestClient(this.RestClient);
                executeOutput = Activator.CreateInstance(UploadOutput);
                this.Request = new RestRequest(this.UploadClient, this.UploadMethod)
                {
                    RequestFormat = this.RequestFormat
                };
                this.SetRequestParameters(input);
                this.SetRequestHeaders();

                this.Response = await this.Client.ExecuteTaskAsync(this.Request);
                try
                {
                    var content = this.Response.Content.Replace("\"data\":[]", "\"data\":null");
                    this.Response.Content = content;
                    switch (this.RequestFormat)
                    {
                        case DataFormat.Json:
                            executeOutput = executeOutput.JsonDeserialize(this.Response);
                            break;
                        case DataFormat.Xml:
                            executeOutput = executeOutput.XmlDeserialize(this.Response);
                            break;
                    }
                }
                catch (Exception)
                {
                    if (this.Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        throw;
                    }
                    else
                    {
                        if (this.Response.ErrorException != null)
                        {
                            throw this.Response.ErrorException;
                        }
                        else
                        {
                            throw new Exception(string.Format("({0}) - {1}", this.Response.StatusCode.ToInt32(), this.Response.StatusDescription));
                        }
                    }
                }
                
                return executeOutput;
            }
            catch (Exception ex)
            {
                ErrorExecution(ex.Message);
                throw;
            }
            finally
            {
                StopExecution();
                try
                {
                    LogRequestAsync(this.Request, this.Response, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
                }
                catch { }
            }
        }

        /// <summary>
        /// Download
        /// </summary>
        /// <param name="input">DownloadInput</param>
        /// <param name="operation">FormOperationType</param>
        /// <returns>byte[]</returns>
        public object Download(object input, FormOperationType operation = FormOperationType.Execute)
        {
            try
            {
                StartExecution();
                if (this.DownloadMethod == Method.GET) this.UseRpc = false;
                this.Client = new RestClient(this.RestClient);
                this.Request = new RestRequest(this.DownloadClient, this.DownloadMethod)
                {
                    RequestFormat = this.RequestFormat
                };
                this.SetRequestParameters(input);
                this.SetRequestHeaders();
                
                return this.Client.DownloadData(this.Request);
            }
            catch (Exception ex)
            {
                ErrorExecution(ex.Message);
                throw;
            }
            finally
            {
                StopExecution();
                try
                {
                    LogRequestAsync(this.Request, this.Response, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
                }
                catch { }
            }
        }

        /// <summary>
        /// Sum
        /// </summary>
        /// <param name="input">SumInput</param>
        /// <returns>object</returns>
        [Obsolete]
        public async Task<object> SumAsync(object input)
        {
            try
            {
                StartExecution();
                dynamic executeOutput;
                if(this.SumMethod == Method.GET) this.UseRpc = false; 
                this.Client = new RestClient(this.RestClient);
                executeOutput = Activator.CreateInstance(SumOutput);
                this.Request = new RestRequest(this.SumClient, this.SumMethod)
                {
                    RequestFormat = this.RequestFormat
                };
                this.SetRequestParameters(input);
                this.SetRequestHeaders();
                
                this.Response = await this.Client.ExecuteTaskAsync(this.Request);
                try
                {
                    var content = this.Response.Content.Replace("\"data\":[]", "\"data\":null");
                    this.Response.Content = content;
                    switch (this.RequestFormat)
                    {
                        case DataFormat.Json:
                            executeOutput = executeOutput.JsonDeserialize(this.Response);
                            break;
                        case DataFormat.Xml:
                            executeOutput = executeOutput.XmlDeserialize(this.Response);
                            break;
                    }
                }
                catch (Exception)
                {
                    if (this.Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        throw;
                    }
                    else
                    {
                        if (this.Response.ErrorException != null)
                        {
                            throw this.Response.ErrorException;
                        }
                        else
                        {
                            throw new Exception(string.Format("({0}) - {1}", this.Response.StatusCode.ToInt32(), this.Response.StatusDescription));
                        }
                    }
                }
                return executeOutput;
            }
            catch (Exception ex)
            {
                ErrorExecution(ex.Message);
                throw;
            }
            finally
            {
                StopExecution();
                try
                {
                    LogRequestAsync(this.Request, this.Response, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
                }
                catch { }
            }
        }

        /// <summary>
        /// Select T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        [Obsolete]
        public async Task<T> SelectAsync<T>(object input) where T : new()
        {
            object obj = await this.SelectAsync(input);
            return (T)obj;
        }

        /// <summary>
        /// Sum T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        [Obsolete]
        public async Task<T> SumAsync<T>(object input) where T : new()
        {
            object obj = await this.SumAsync(input);
            return (T)obj;
        }

        /// <summary>
        /// Execute T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        [Obsolete]
        public async Task<T> ExecuteAsync<T>(object input, FormOperationType operation = FormOperationType.Execute) where T : new()
        {
            object obj = await this.ExecuteAsync(input, operation);
            return (T)obj;
        }

        /// <summary>
        /// Upload T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        [Obsolete]
        public async Task<T> UploadAsync<T>(object input, FormOperationType operation = FormOperationType.Execute) where T : new()
        {
            object obj = await this.UploadAsync(input, operation);
            return (T)obj;
        }

        /// <summary>
        /// LogRequest
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="executionTime"></param>
        /// <param name="status"></param>
        public void LogRequest(dynamic request, dynamic response, object input, object output, TimeSpan executionTime, bool status)
        {
            try
            {
                Request req = new Request
                {
                    DeviceInfo = new DeviceInfo()
                };
                req.DeviceInfo.AssemblyName = Security.AssemblyName;
                req.DeviceInfo.AssemblyVersion = Security.AssemblyVersion;
                req.DeviceInfo.CpuId = Security.CpuId;
                req.DeviceInfo.HardDrive = Security.HardDrive;
                req.DeviceInfo.IpAddress = Security.IpAddress;
                req.DeviceInfo.MacAddress = Security.MacAddress;
                req.DeviceInfo.VolumeSerial = Security.VolumeSerial;
                req.DeviceInfo.MachineName = Environment.MachineName;
                req.DeviceInfo.Is64BitOperatingSystem = Environment.Is64BitOperatingSystem;
                req.DeviceInfo.OSVersion = Environment.OSVersion.VersionString;
                req.DeviceInfo.ProcessorCount = Environment.ProcessorCount;
                req.DeviceInfo.UserName = Environment.UserName;
                
                Uri uri = this.Client.BuildUri(request);
                req.Address = uri.AbsoluteUri;
                
                switch ((request as RestRequest).RequestFormat)
                {
                    case DataFormat.Json:
                        req.Format = "JSON";
                        req.Input = input.JsonSerialize();
                        break;
                    case DataFormat.Xml:
                        req.Format = "XML";
                        req.Input = input.XmlSerialize();
                        break;
                }

                switch ((request as RestRequest).Method)
                {
                    case Method.DELETE: req.Method = "DELETE"; break;
                    case Method.GET: req.Method = "GET"; break;
                    case Method.HEAD: req.Method = "HEAD"; break;
                    case Method.MERGE: req.Method = "MERGE"; break;
                    case Method.OPTIONS: req.Method = "OPTIONS"; break;
                    case Method.PATCH: req.Method = "PATCH"; break;
                    case Method.POST: req.Method = "POST"; break;
                    case Method.PUT: req.Method = "PUT"; break;
                }
                
                req.Output = Converter.ToString(output);
                req.Status = status;
                req.Date = DateTime.Now;
                req.ExecutionTime = executionTime;
                

                req.SaveLog();
            }
            catch (Exception ex)
            {
                ex.SaveLog();
            }
        }

        /// <summary>
        /// LogRequestTask
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="executionTime"></param>
        /// <param name="status"></param>
        public async Task LogRequestTask(dynamic request, dynamic response, object input, object output, TimeSpan executionTime, bool status)
        {
            try
            {
                await Task.Run(() =>
                {
                    LogRequest(request, response, input, output, executionTime, status);
                }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ex.SaveLog();
            }
        }

        /// <summary>
        /// LogRequestAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="executionTime"></param>
        /// <param name="status"></param>
        public async void LogRequestAsync(dynamic request, dynamic response, object input, object output, TimeSpan executionTime, bool status)
        {
            try
            {
                if (EnabledLog == false) return;
                await this.LogRequestTask(request, response, input, output, executionTime, status);
            }
            catch (Exception ex)
            {
                ex.SaveLog();
            }
        }
    }
}
