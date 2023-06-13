using Ione.Framework.Core;
using Ione.Framework.Core.Authenticator;
using Ione.Framework.Core.Logs;
using RestSharp;
using RestSharp.Serializers.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ione.Framework.Rest
{
    public abstract partial class Model<TEntity, TInput, TRows, TRow> : IServiceModel
    {
        public Model()
        {
            EnabledLog = false;
            ModelOption = ModelOptionType.REST;
            IsRpc = false;
            DateFormat = "yyyy-MM-dd HH:mm:ss";
            RequestFormat = DataFormat.Json;
            JsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

            JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter(DateFormat));
        }

        public abstract string RestClient { get; }
        public abstract string EndpointGet { get; }
        public abstract string EndpointGetRow { get; }
        public abstract string EndpointPost { get; }
        public abstract string EndpointPut { get;}
        public abstract string EndpointDelete { get; }
        public abstract string EndpointDownload { get; }
        public bool EnabledLog { get; set; }
        public Stopwatch ExecutionTime { get; set; }
        public bool ExecutionStatus { get; set; }
        public string ExecutionError { get; set; }
        public ModelOptionType ModelOption { get; set; }
        public IAuthenticator Authenticator { get; set; }
        public bool IsRpc { get; set; }

        /// <summary>
        /// RequestFormat
        /// Format Request = DataFormat.Json/DataFormat.Xml
        /// Default = DataFormat.Json
        /// </summary>
        public DataFormat RequestFormat { get; set; }

        /// <summary>
        /// RequestHeader
        /// </summary>
        public Hashtable RequestHeader;

        /// <summary>
        /// RequestParameter
        /// </summary>
        public Hashtable RequestParameter;

        /// <summary>
        /// IncludeHeaderIpAddress
        /// </summary>
        public bool SendIpAddress { get; set; }

        /// <summary>
        /// IncludeHeaderDrive
        /// </summary>
        public bool SendHardware { get; set; }

        public string RootElement { get; set; }
        public string XmlNamespace { get; set; }
        public string DateFormat { get; set; }
        public JsonSerializerOptions JsonSerializerOptions { get; set; }


        private Hashtable DefaultRequestHeader { get; set; }
        
       
        /// <summary>
        /// Rest Client
        /// </summary>
        public RestClient Client;

        /// <summary>
        /// Rest Request
        /// </summary>
        public RestRequest Request;

        /// <summary>
        /// Rest Response
        /// </summary>
        public RestResponse Response;

        /// <summary>
        /// StartExecution
        /// </summary>
        public virtual void StartExecution()
        {
            ExecutionTime = new Stopwatch();
            ExecutionTime.Start();
            ExecutionStatus = true;
            ExecutionError = string.Empty;
        }
        /// <summary>
        /// StopExecution
        /// </summary>
        public virtual void StopExecution()
        {
            ExecutionTime.Stop();
        }
        /// <summary>
        /// ErrorExecution
        /// </summary>
        /// <param name="message"></param>
        public virtual void ErrorExecution(string message)
        {
            ExecutionStatus = false;
            ExecutionError = message;
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
                req.Response =  ((RestResponse)response).GetType().ToString();
                req.DeviceInfo.AssemblyName = Security.AssemblyName;
                req.DeviceInfo.AssemblyVersion = Security.AssemblyVersion;
                //req.DeviceInfo.CpuId = Security.CpuId;
                //req.DeviceInfo.HardDrive = Security.HardDrive;
                //req.DeviceInfo.IpAddress = Security.IpAddress;
                //req.DeviceInfo.MacAddress = Security.MacAddress;
                //req.DeviceInfo.VolumeSerial = Security.VolumeSerial;
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
                    case Method.Delete: req.Method = "DELETE"; break;
                    case Method.Get: req.Method = "GET"; break;
                    case Method.Head: req.Method = "HEAD"; break;
                    case Method.Merge: req.Method = "MERGE"; break;
                    case Method.Options: req.Method = "OPTIONS"; break;
                    case Method.Patch: req.Method = "PATCH"; break;
                    case Method.Post: req.Method = "POST"; break;
                    case Method.Put: req.Method = "PUT"; break;
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

        /// <summary>
        /// SetRequestHeaders
        /// </summary>
        private void SetRequestHeaders()
        {
            this.DefaultRequestHeader = new Hashtable();

            if (this.Authenticator != null)
            {
                if (this.Authenticator.GetType() == typeof(OAuth1Authenticator))
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
            //this.DefaultRequestHeader.Add("MacAddress", Core.Security.MacAddress);

            this.DefaultRequestHeader.Add("AssemblyName", Core.Security.AssemblyName);
            this.DefaultRequestHeader.Add("AssemblyVersion", Core.Security.AssemblyVersion);


            //if (SendIpAddress)
            //    this.DefaultRequestHeader.Add("IpAddress", Core.Security.IpAddress);

            if (SendHardware)
            {
                try
                {
                    //this.DefaultRequestHeader.Add("TotalCpuUsage", Core.Security.TotalCpuUsage);
                    //this.DefaultRequestHeader.Add("TotalMemoryAvailable", Core.Security.TotalMemoryAvailable);
                    //this.DefaultRequestHeader.Add("TotalPhysicalMemory", Core.Security.TotalPhysicalMemory);
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
            if (this.RequestHeader != null)
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
                if (this.IsRpc)
                {
                    switch (this.RequestFormat)
                    {
                        case DataFormat.Json:
                            this.Request.AddBody((object)input, "application/json");
                            break;
                        case DataFormat.Xml:
                            //this.Request.AddXmlBody(input, this.XmlNamespace);
                            break;
                    }
                    //this.AddRequestParameters(((object)input).ToHashtable(), true);
                }
                else
                {
                    this.AddRequestParameters(((object)input).ToHashtable(), false);

                    try
                    {
                        if (Extension.IsPropertyExist(input, "Data"))
                        {
                            if (input.Data != null)
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
                    //else if(item.Value.GetType() == typeof(List<FormFile>))
                    //{
                    //    foreach (FormFile file in (List<FormFile>)item.Value)
                    //    {
                    //        if (file.Length > 0)
                    //        {
                    //            using var memoryStream = new MemoryStream();
                    //            file.CopyTo(memoryStream);
                    //            this.Request.AddFile(file.Name, memoryStream.ToArray(), file.FileName, file.ContentType);
                    //        }
                    //    }
                    //}
                    //else if (item.Value.GetType() == typeof(FormFile))
                    //{
                    //    FormFile file = item.Value as FormFile;

                    //    if(file.Length > 0)
                    //    {
                    //        using var memoryStream = new MemoryStream();
                    //        file.CopyTo(memoryStream);
                    //        this.Request.AddFile(file.Name, memoryStream.ToArray(), file.FileName, file.ContentType);
                    //    }
                        
                    //}
                    else if (item.Value.GetType() == typeof(DateTime?) || item.Value.GetType() == typeof(DateTime))
                    {
                        string valdate = item.Value.FormatDate(DateFormat);
                        if (!string.IsNullOrEmpty(valdate))
                        {
                            this.Request.AddParameter(Core.Converter.ToString(item.Key), valdate);
                        }
                            
                    }
                    else
                    {
                        if (!isRpc)
                        {
                            this.Request.AddParameter(Core.Converter.ToString(item.Key), Converter.ToString(item.Value));
                        }
                        if (this.Request.Method == Method.Put || this.Request.Method == Method.Delete || this.Request.Method == Method.Get)
                        {
                            if(item.Value != null)
                            {
                                string valstr = Core.Converter.ToString(item.Value);
                                if(!string.IsNullOrEmpty(valstr)) {
                                    try
                                    {
                                        this.Request.AddUrlSegment(Core.Converter.ToString(item.Key), valstr);
                                    }
                                    catch(Exception ex)
                                    {
                                        Console.WriteLine(string.Format("AddRequestParameters-AddUrlSegment {0}:{1}", Core.Converter.ToString(item.Key), ex.Message));
                                    }
                                   
                                }
                               
                            }
                            
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="input">GetAsync</param>
        /// <returns>TRows</returns>
        public async Task<TRows> GetAsync(TInput input)
        {
            try
            {
                StartExecution();
                this.Client = new RestClient(RestClient);
                this.Client.UseSystemTextJson(JsonSerializerOptions);
                this.Request = new RestRequest(EndpointGet, Method.Get)
                {
                    RequestFormat = this.RequestFormat,
                    
                };
                if(this.RequestFormat == DataFormat.Json)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                }
                else if (this.RequestFormat == DataFormat.Xml)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/xml"; };
                }
                
                SetRequestParameters(input);
                SetRequestHeaders();
                
                RestResponse<TRows> response = await Client.ExecuteAsync<TRows>(Request);
                Response = response;
                if (response != null && response.ErrorException != null) throw response.ErrorException;
                return response.Data;
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
                    LogRequestAsync(Request, Response, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
                }
                catch { }
            }
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="endpoint">Endpoint</param>
        /// <param name="input">Input</param>
        /// <returns>TRows</returns>
        public async Task<TRows> GetAsync(string endpoint, TInput input) 
        {
            try
            {
                StartExecution();
                this.Client = new RestClient(RestClient);
                this.Client.UseSystemTextJson(JsonSerializerOptions);
                this.Request = new RestRequest(endpoint, Method.Get)
                {
                    RequestFormat = this.RequestFormat,
                };

                if (this.RequestFormat == DataFormat.Json)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                }
                else if (this.RequestFormat == DataFormat.Xml)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/xml"; };
                }
                SetRequestParameters(input);
                SetRequestHeaders();

                RestResponse<TRows> response = await Client.ExecuteAsync<TRows>(Request);
                Response = response;
                if (response != null && response.ErrorException != null) throw response.ErrorException;
                return response.Data;
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
                    LogRequestAsync(Request, Response, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
                }
                catch { }
            }
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="input">GetRowAsync</param>
        /// <returns>TRow</returns>
        public async Task<TRow> GetRowAsync(TInput input)
        {
            try
            {
                StartExecution();
                this.Client = new RestClient(RestClient);
                this.Client.UseSystemTextJson(JsonSerializerOptions);
                this.Request = new RestRequest(EndpointGetRow, Method.Get)
                {
                    RequestFormat = this.RequestFormat,
                };
                if (this.RequestFormat == DataFormat.Json)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                }
                else if (this.RequestFormat == DataFormat.Xml)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/xml"; };
                }
                SetRequestParameters(input);
                SetRequestHeaders();

                RestResponse<TRow> response = await Client.ExecuteAsync<TRow>(Request);
                Response = response;
                if (response != null && response.ErrorException != null) throw response.ErrorException;
                return response.Data;
            }
            catch (Exception ex)
            {
                ErrorExecution(ex.Message);
                throw ;
            }
            finally
            {
                StopExecution();
                try
                {
                    LogRequestAsync(Request, Response, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
                }
                catch { }
            }
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="endpoint">Endpoint</param>
        /// <param name="input">GetRowAsync</param>
        /// <returns>TRow</returns>
        public async Task<TRow> GetRowAsync(string endpoint, TInput input)
        {
            try
            {
                StartExecution();
                this.Client = new RestClient(RestClient);
                this.Client.UseSystemTextJson(JsonSerializerOptions);
                this.Request = new RestRequest(endpoint, Method.Get)
                {
                    RequestFormat = this.RequestFormat,
                };
                if (this.RequestFormat == DataFormat.Json)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                }
                else if (this.RequestFormat == DataFormat.Xml)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/xml"; };
                }
                SetRequestParameters(input);
                SetRequestHeaders();

                RestResponse<TRow> response = await Client.ExecuteAsync<TRow>(Request);
                Response = response;
                if (response != null && response.ErrorException != null) throw response.ErrorException;
                return response.Data;
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
                    LogRequestAsync(Request, Response, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
                }
                catch { }
            }
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="input">PostAsync</param>
        /// <returns>TRows</returns>
        public async Task<TRow> PostAsync(TEntity input)
        {
            try
            {
                StartExecution();
                this.Client = new RestClient(RestClient);
                this.Client.UseSystemTextJson(JsonSerializerOptions);
                this.Request = new RestRequest(EndpointPost, Method.Post)
                {
                    RequestFormat = this.RequestFormat,
                };
                if (this.RequestFormat == DataFormat.Json)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                }
                else if (this.RequestFormat == DataFormat.Xml)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/xml"; };
                }
                SetRequestParameters(input);
                SetRequestHeaders();

                RestResponse<TRow> response = await Client.ExecuteAsync<TRow>(Request);
                Response = response;
                if (response != null && response.ErrorException != null) throw response.ErrorException;
                return response.Data;
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
                    LogRequestAsync(Request, Response, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
                }
                catch { }
            }
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="endpoint">Endpoint</param>
        /// <param name="input">Input</param>
        /// <returns>TRows</returns>
        public async Task<TRow> PostAsync(string endpoint, object input)
        {
            try
            {
                StartExecution();
                this.Client = new RestClient(RestClient);
                this.Client.UseSystemTextJson(JsonSerializerOptions);
                this.Request = new RestRequest(endpoint, Method.Post)
                {
                    RequestFormat = this.RequestFormat,
                };
                if (this.RequestFormat == DataFormat.Json)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                }
                else if (this.RequestFormat == DataFormat.Xml)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/xml"; };
                }
                SetRequestParameters(input);
                SetRequestHeaders();

                RestResponse<TRow> response = await Client.ExecuteAsync<TRow>(Request);
                Response = response;
                if (response != null && response.ErrorException != null) throw response.ErrorException;
                return response.Data;
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
                    LogRequestAsync(Request, Response, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
                }
                catch { }
            }
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="input">PutAsync</param>
        /// <returns>TRows</returns>
        public async Task<TRow> PutAsync(TEntity input)
        {
            try
            {
                StartExecution();
                this.Client = new RestClient(RestClient);
                this.Client.UseSystemTextJson(JsonSerializerOptions);
                this.Request = new RestRequest(EndpointPut, Method.Put)
                {
                    RequestFormat = this.RequestFormat,
                };
                if (this.RequestFormat == DataFormat.Json)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                }
                else if (this.RequestFormat == DataFormat.Xml)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/xml"; };
                }
                SetRequestParameters(input);
                SetRequestHeaders();

                RestResponse<TRow> response = await Client.ExecuteAsync<TRow>(Request);
                Response = response;
                if (response != null && response.ErrorException != null) throw response.ErrorException;
                return response.Data;
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
                    LogRequestAsync(Request, Response, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
                }
                catch { }
            }
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="endpoint">Endpoint</param>
        /// <param name="input">Input</param>
        /// <returns>TRows</returns>
        public async Task<TRow> PutAsync(string endpoint, object input)
        {
            try
            {
                StartExecution();
                this.Client = new RestClient(RestClient);
                this.Client.UseSystemTextJson(JsonSerializerOptions);
                this.Request = new RestRequest(endpoint, Method.Put)
                {
                    RequestFormat = this.RequestFormat,
                };
                if (this.RequestFormat == DataFormat.Json)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                }
                else if (this.RequestFormat == DataFormat.Xml)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/xml"; };
                }
                SetRequestParameters(input);
                SetRequestHeaders();

                RestResponse<TRow> response = await Client.ExecuteAsync<TRow>(Request);
                Response = response;
                if (response != null && response.ErrorException != null) throw response.ErrorException;
                return response.Data;
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
                    LogRequestAsync(Request, Response, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
                }
                catch { }
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="input">DeleteAsync</param>
        /// <returns>TRows</returns>
        public async Task<TRow> DeleteAsync(TEntity input)
        {
            try
            {
                StartExecution();
                this.Client = new RestClient(RestClient);
                this.Client.UseSystemTextJson(JsonSerializerOptions);
                this.Request = new RestRequest(EndpointDelete, Method.Delete)
                {
                    RequestFormat = this.RequestFormat,
                };
                if (this.RequestFormat == DataFormat.Json)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                }
                else if (this.RequestFormat == DataFormat.Xml)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/xml"; };
                }
                SetRequestParameters(input);
                SetRequestHeaders();

                RestResponse<TRow> response = await Client.ExecuteAsync<TRow>(Request);
                Response = response;
                if (response != null && response.ErrorException != null) throw response.ErrorException;
                return response.Data;
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
                    LogRequestAsync(Request, Response, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
                }
                catch { }
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="endpoint">Endpoint</param>
        /// <param name="input">Input</param>
        /// <returns>TRows</returns>
        public async Task<TRow> DeleteAsync(string endpoint, object input)
        {
            try
            {
                StartExecution();
                this.Client = new RestClient(RestClient);
                this.Client.UseSystemTextJson(JsonSerializerOptions);
                this.Request = new RestRequest(endpoint, Method.Delete)
                {
                    RequestFormat = this.RequestFormat,
                };
                if (this.RequestFormat == DataFormat.Json)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                }
                else if (this.RequestFormat == DataFormat.Xml)
                {
                    this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/xml"; };
                }
                SetRequestParameters(input);
                SetRequestHeaders();

                RestResponse<TRow> response = await Client.ExecuteAsync<TRow>(Request);
                Response = response;
                if (response != null && response.ErrorException != null) throw response.ErrorException;
                return response.Data;
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
                    LogRequestAsync(Request, Response, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
                }
                catch { }
            }
        }

        public byte[] Download(object input)
        {
            try
            {
                StartExecution();
                this.Client = new RestClient(this.RestClient);
                this.Request = new RestRequest(this.EndpointDownload, Method.Get)
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
            }
        }

        public byte[] Download(string restClient, string endpoint)
        {
            try
            {
                StartExecution();
                this.Client = new RestClient(restClient);
                this.Request = new RestRequest(endpoint, Method.Get)
                {
                    RequestFormat = this.RequestFormat
                };
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
            }
        }

        public async Task<object> GetAsync(object input)
        {
            return await GetAsync((TInput)input);
        }

        public async Task<object> GetRowAsync(object input)
        {
            return await GetRowAsync((TInput)input);
        }

        public async Task<object> PostAsync(object input)
        {
            return await PostAsync((TEntity)input);
        }

        public async Task<object> PutAsync(object input)
        {
            return await PutAsync((TEntity)input);
        }

        public async Task<object> DeleteAsync(object input)
        {
            return await DeleteAsync((TEntity)input);
        }
    }
}
