using Ione.Framework.Core;
using Ione.Framework.Core.Authenticator;
using Ione.Framework.Core.Logs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Ione.Framework.Soap
{
    /// <summary>
    /// Model
    /// abstract Model SOAP
    /// </summary>
    public abstract class Model : IModel, IModelType
    {
        /// <summary>
        /// SetRequestHeader
        /// </summary>
        public bool SetRequestHeader = false;
        /// <summary>
        /// ModelOption
        /// Jenis Model
        /// </summary>
        public ModelOptionType ModelOption
        {
            get { return ModelOptionType.SOAP; }
        }
        /// <summary>
        /// Authenticator
        /// </summary>
        public IAuthenticator Authenticator;

        /// <summary>
        /// BeginMethod
        /// Daftar Nama BeginMethod untuk seluruh operasi
        /// 
        /// Default :
        /// this.BeginMethod = new Hashtable();
        /// this.BeginMethod.Add(FormOperationType.Read, "Beginexecute");
        /// this.BeginMethod.Add(FormOperationType.Create, "Begincreate");
        /// this.BeginMethod.Add(FormOperationType.Update, "Beginupdate");
        /// this.BeginMethod.Add(FormOperationType.Delete, "Begindelete");
        /// this.BeginMethod.Add(FormOperationType.Execute, "Beginexecute");
        /// this.BeginMethod.Add(FormOperationType.Sum, "Beginexecute");
        /// </summary>
        public Hashtable BeginMethod;

        /// <summary>
        /// EndMethod
        /// Daftar Nama EndMethod untuk seluruh operasi
        /// 
        /// Default :
        /// this.EndMethod = new Hashtable();
        /// this.EndMethod.Add(FormOperationType.Read, "Endexecute");
        /// this.EndMethod.Add(FormOperationType.Create, "Endcreate");
        /// this.EndMethod.Add(FormOperationType.Update, "Endupdate");
        /// this.EndMethod.Add(FormOperationType.Delete, "Enddelete");
        /// this.EndMethod.Add(FormOperationType.Execute, "Endexecute");
        /// this.EndMethod.Add(FormOperationType.Sum, "Endexecute");
        /// </summary>
        public Hashtable EndMethod;

        /// <summary>
        /// SelectInput
        /// object input untuk select data
        /// </summary>
        public static object SelectInput;

        /// <summary>
        /// SelectClient
        /// Type class client untuk select data
        /// </summary>
        public Type SelectClient;

        /// <summary>
        /// SelectOutput
        /// Type class untuk output select
        /// </summary>
        public Type SelectOutput;

        /// <summary>
        /// SelectRow
        /// Type class untuk row select data
        /// </summary>
        public Type SelectRow { get; set; }

        /// <summary>
        /// SelectMember
        /// Nama member pada output select array
        /// </summary>
        public String SelectMember;

        /// <summary>
        /// SumInput
        /// object input untuk summary data
        /// </summary>
        public static object SumInput;

        /// <summary>
        /// SumClient
        /// Type class client untuk summary data
        /// </summary>
        public Type SumClient;

        /// <summary>
        /// SumOutput
        /// Type class untuk output summary
        /// </summary>
        public Type SumOutput;

        /// <summary>
        /// SumRow
        /// Type class untuk row summary data
        /// </summary>
        public Type SumRow;

        /// <summary>
        /// SumMember
        /// Nama member pada output summary array
        /// </summary>
        public String SumMember;

        /// <summary>
        /// CrudInput
        /// object input untuk crud data
        /// </summary>
        public static object CrudInput;

        /// <summary>
        /// CrudClient
        /// Type class client untuk crud data
        /// </summary>
        public Type CrudClient;

        /// <summary>
        /// CrudOutput
        /// Type class untuk output crud
        /// </summary>
        public Type CrudOutput;

        /// <summary>
        /// UploadInput
        /// object input untuk upload data
        /// </summary>
        public static object UploadInput;

        /// <summary>
        /// UploadClient
        /// Type class client untuk upload data
        /// </summary>
        public Type UploadClient;

        /// <summary>
        /// UplaodOutput
        /// Type class untuk output upload
        /// </summary>
        public Type UploadOutput;

        /// <summary>
        /// DownloadInput
        /// object input untuk download data
        /// </summary>
        public static object DownloadInput;

        /// <summary>
        /// DownloadClient
        /// Type class client untuk download data
        /// </summary>
        public Type DownloadClient;

        /// <summary>
        /// DownloadOutput
        /// Type class untuk output download
        /// </summary>
        public Type DownloadOutput;

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
        /// Result
        /// Hasil async result pada seluruh operasi kecuali summary
        /// </summary>
        public IAsyncResult Result = null;


        /// <summary>
        /// ResultSum
        /// Hasil async result pada operasi summary
        /// </summary>
        public IAsyncResult ResultSum = null;

        /// <summary>
        /// EnabledLog
        /// </summary>
        public bool EnabledLog{ get; set; }

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
        /// this.BeginMethod = new Hashtable();
        /// this.BeginMethod.Add(FormOperationType.Read, "Beginexecute");
        /// this.BeginMethod.Add(FormOperationType.Create, "Begincreate");
        /// this.BeginMethod.Add(FormOperationType.Update, "Beginupdate");
        /// this.BeginMethod.Add(FormOperationType.Delete, "Begindelete");
        /// this.BeginMethod.Add(FormOperationType.Execute, "Beginexecute");
        /// this.BeginMethod.Add(FormOperationType.Sum, "Beginexecute");
        /// 
        /// this.EndMethod = new Hashtable();
        /// this.EndMethod.Add(FormOperationType.Read, "Endexecute");
        /// this.EndMethod.Add(FormOperationType.Create, "Endcreate");
        /// this.EndMethod.Add(FormOperationType.Update, "Endupdate");
        /// this.EndMethod.Add(FormOperationType.Delete, "Enddelete");
        /// this.EndMethod.Add(FormOperationType.Execute, "Endexecute");
        /// this.EndMethod.Add(FormOperationType.Sum, "Endexecute");
        /// </summary>
        public Model()
        {
            this.BeginMethod = new Hashtable
            {
                { FormOperationType.Read, "Beginexecute" },
                { FormOperationType.Create, "Begincreate" },
                { FormOperationType.Update, "Beginupdate" },
                { FormOperationType.Delete, "Begindelete" },
                { FormOperationType.Execute, "Beginexecute" },
                { FormOperationType.Sum, "Beginexecute" }
            };

            this.EndMethod = new Hashtable
            {
                { FormOperationType.Read, "Endexecute" },
                { FormOperationType.Create, "Endcreate" },
                { FormOperationType.Update, "Endupdate" },
                { FormOperationType.Delete, "Enddelete" },
                { FormOperationType.Execute, "Endexecute" },
                { FormOperationType.Sum, "Endexecute" }
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
        /// <param name="client"></param>
        public virtual void SetRequestHeaders(dynamic client)
        {
            try
            {
                
                string auth = null;
                if (this.Authenticator != null)
                {
                    if (this.Authenticator.GetType() == typeof(OAuth1Authenticator))
                    {
                        OAuth1Authenticator authenticator = this.Authenticator as OAuth1Authenticator;
                        authenticator.Url = new Uri(client.Endpoint.Address.Uri.OriginalString);
                        authenticator.Timestamp = OAuth.GenerateTimeStamp();
                        authenticator.Nonce = OAuth.GenerateNonce();
                        authenticator.Method = "POST";
                        authenticator.Version = "1.0";
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
                            out string normalizedUrl,
                            out string normalizedRequestParameters
                            );
                        auth = string.Format("OAuth oauth_consumer_key=\"{0}\",oauth_token=\"{1}\",oauth_signature_method=\"{2}\",oauth_timestamp=\"{3}\",oauth_nonce=\"{4}\",oauth_version=\"{5}\",oauth_signature=\"{6}\",oauth_url=\"{7}\"",
                            authenticator.ConsumerKey,
                            authenticator.AccessToken,
                            authenticator.SignatureMethod.GetSignaturType(),
                            authenticator.Timestamp,
                            authenticator.Nonce,
                            authenticator.Version,
                            authenticator.Signature,
                            authenticator.Url
                            );
                    }
                    if (this.Authenticator.GetType() == typeof(BasicAuthenticator))
                    {
                        BasicAuthenticator authenticator = this.Authenticator as BasicAuthenticator;
                        auth = string.Format("Basic {0}", BasicAuthenticator.Encode(authenticator));

                    }
                }

                //using (new OperationContextScope(client.InnerChannel))
                //{
                //    //// Add a SOAP Header to an outgoing request 
                //    //MessageHeader aMessageHeader = MessageHeader.CreateHeader("UserInfo", "http://tempuri.org", userInfo);
                //    //OperationContext.Current.OutgoingMessageHeaders.Add(aMessageHeader);

                //    // Add an HTTP Header to an outgoing request 
                //    HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
                //    if(!string.IsNullOrEmpty(auth))
                //        requestMessage.Headers["Authorization"] = auth;

                //    requestMessage.Headers["CpuId"] = Core.Security.CpuId;
                //    requestMessage.Headers["HardDrive"] = Core.Security.HardDrive;
                //    requestMessage.Headers["MacAddress"] = Core.Security.MacAddress;
                //    requestMessage.Headers["VolumeSerial"] = Core.Security.VolumeSerial;
                //    //requestMessage.Headers["IpAddress"] = Core.Security.Core.Security.IpAddress.XmlSerialize();
                //    requestMessage.Headers["AssemblyName"] = Core.Security.AssemblyName;
                //    requestMessage.Headers["AssemblyVersion"] = Core.Security.AssemblyVersion;

                //    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;


                //}
                
                var endpoint = new EndpointAddressBuilder(client.Endpoint.Address);

                if (!string.IsNullOrEmpty(auth))
                    endpoint.Headers.Add(AddressHeader.CreateAddressHeader("Authorization", string.Empty, auth));

                if (SetRequestHeader)
                {
                    //endpoint.Headers.Add(AddressHeader.CreateAddressHeader("CpuId", string.Empty, Security.CpuId));
                    //endpoint.Headers.Add(AddressHeader.CreateAddressHeader("HardDrive", string.Empty, Security.HardDrive));
                    //endpoint.Headers.Add(AddressHeader.CreateAddressHeader("MacAddress", string.Empty, Security.MacAddress));
                    //endpoint.Headers.Add(AddressHeader.CreateAddressHeader("VolumeSerial", string.Empty, Security.VolumeSerial));
                    //endpoint.Headers.Add(AddressHeader.CreateAddressHeader("IpAddress",string.Empty, Security.IpAddress.XmlSerialize()));
                    endpoint.Headers.Add(AddressHeader.CreateAddressHeader("AssemblyName", string.Empty, Security.AssemblyName));
                    endpoint.Headers.Add(AddressHeader.CreateAddressHeader("AssemblyVersion", string.Empty, Security.AssemblyVersion));

                }

                client.Endpoint.Address = endpoint.ToEndpointAddress();


            }
            catch
            {

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
            throw new NotImplementedException();
        }

        /// <summary>
        /// CountTask 
        /// Untuk mendapatkan seluruh total record menggunakan System.Threading.Task
        /// </summary>
        /// <param name="input">CountInput</param>
        /// <param name="operation">FormOperationType</param>
        /// <returns>int</returns>
        public async Task<int> CountTask(object input, FormOperationType operation = FormOperationType.Execute)
        {
            return await Task.Run(() => Count(input, operation));
        }

        /// <summary>
        /// Execute
        /// 
        /// </summary>
        /// <param name="input">CrudInput</param>
        /// <param name="operation">FormOperationType</param>
        /// <returns>object</returns>
        public virtual object Execute(object input, FormOperationType operation = FormOperationType.Execute)
        {
            StartExecution();
            object crudclient = Activator.CreateInstance(CrudClient);
            //dynamic crudoutput = Activator.CreateInstance(CrudOutput);
            string beginmethod = this.BeginMethod[operation] as string;
            string endmethod = this.EndMethod[operation] as string;
            try
            {
                this.SetRequestHeaders(crudclient);
                this.Result = crudclient.GetType().GetMethod(beginmethod).Invoke(crudclient, new object[] { input, null, null }) as IAsyncResult;
                this.Result.AsyncWaitHandle.WaitOne();
                this.Result.AsyncWaitHandle.Close();
                object crudoutput = crudclient.GetType().GetMethod(endmethod).Invoke(crudclient, new object[] { this.Result });
                crudclient.GetType().GetMethod("Close").Invoke(crudclient, new object[] { });
                return crudoutput;
            }
            catch (Exception ex)
            {
                ErrorExecution(ex.Message);
                ex.SaveLog();
                throw;
            }
            finally
            {
                StopExecution();
                this.LogRequestAsync(crudclient, null, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
            }
        }

        /// <summary>
        /// ExecuteTask
        /// 
        /// </summary>
        /// <param name="input">CrudInput</param>
        /// <param name="operation">FormOperationType</param>
        /// <returns>object</returns>
        public virtual async Task<object> ExecuteAsync(object input, FormOperationType operation = FormOperationType.Execute)
        {
            return await Task.Run(() => Execute(input, operation));
        }

        /// <summary>
        /// Execute T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<T> ExecuteAsync<T>(object input, FormOperationType operation = FormOperationType.Execute) where T : new()
        {
            object obj = await this.ExecuteAsync(input, operation);
            return (T)obj;
        }


        /// <summary>
        /// Select
        /// </summary>
        /// <param name="input">SelectInput</param>
        /// <returns>SelectOutput</returns>
        public virtual object Select(object input)
        {
            StartExecution();
            object selectclient = Activator.CreateInstance(SelectClient);
            //object selectoutput = Activator.CreateInstance(SelectOutput);
            string beginmethod = this.BeginMethod[FormOperationType.Read] as string;
            string endmethod = this.EndMethod[FormOperationType.Read] as string;
            try
            {
                this.SetRequestHeaders(selectclient);
                this.Result = selectclient.GetType().GetMethod(beginmethod).Invoke(selectclient, new object[] { input, null, null }) as IAsyncResult;
                this.Result.AsyncWaitHandle.WaitOne();
                this.Result.AsyncWaitHandle.Close();
                dynamic selectoutput = selectclient.GetType().GetMethod(endmethod).Invoke(selectclient, new object[] { this.Result });
                selectclient.GetType().GetMethod("Close").Invoke(selectclient, new object[] { });

 
                if (!string.IsNullOrEmpty(SelectMember))
                {
                    selectoutput = selectoutput.GetType().GetProperty(SelectMember).GetValue(selectoutput);
                    List<dynamic> listData = new List<dynamic>();
                    listData.AddRange(selectoutput);
                    selectoutput = null;
                    return listData;
                }
                else
                {
                    return selectoutput;
                }
            }
            catch (Exception ex)
            {
                ErrorExecution(ex.Message);
                ex.SaveLog();
                throw;
            }
            finally
            {
                StopExecution();
                this.LogRequestAsync(selectclient, null, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
            }
        }

        /// <summary>
        /// SelectTask
        /// </summary>
        /// <param name="input">SelectInput</param>
        /// <returns>object</returns>
        public virtual async Task<object> SelectAsync(object input)
        {
            return await Task.Run(() => Select(input));
        }

        /// <summary>
        /// Select T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<T> SelectAsync<T>(object input) where T : new()
        {
            object obj = await this.SelectAsync(input);
            return (T)obj;
        }


        /// <summary>
        /// Upload
        /// </summary>
        /// <param name="input">UploadInput</param>
        /// <param name="operation">FormOperationType</param>
        /// <returns>UploadOutput</returns>
        public virtual object Upload(object input, FormOperationType operation = FormOperationType.Execute)
        {
            StartExecution();

            object uploadclient = Activator.CreateInstance(UploadClient);
            //object uploadoutput = Activator.CreateInstance(UploadOutput);
            string beginmethod = this.BeginMethod[operation] as string;
            string endmethod = this.EndMethod[operation] as string;
            try
            {
                this.SetRequestHeaders(uploadclient);
                this.Result = uploadclient.GetType().GetMethod(beginmethod).Invoke(uploadclient, new object[] { input, null, null }) as IAsyncResult;
                this.Result.AsyncWaitHandle.WaitOne();
                this.Result.AsyncWaitHandle.Close();
                object uploadoutput = uploadclient.GetType().GetMethod(endmethod).Invoke(uploadclient, new object[] { this.Result });
                uploadclient.GetType().GetMethod("Close").Invoke(uploadclient, new object[] { });

                return uploadoutput;
            }
            catch (Exception ex)
            {
                ErrorExecution(ex.Message);
                ex.SaveLog();
                throw;
            }
            finally
            {
                StopExecution();
                this.LogRequestAsync(uploadclient, null, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
            }
        }

        /// <summary>
        /// UploadTask
        /// </summary>
        /// <param name="input">UploadInput</param>
        /// <param name="operation">FormOperationType</param>
        /// <returns>object</returns>
        public async Task<object> UploadAsync(object input, FormOperationType operation = FormOperationType.Execute)
        {
            return await Task.Run(() => Upload(input, operation));
        }

        /// <summary>
        /// Upload T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<T> UploadAsync<T>(object input, FormOperationType operation = FormOperationType.Execute) where T : new()
        {
            object obj = await this.UploadAsync(input, operation);
            return (T)obj;
        }

        /// <summary>
        /// Download
        /// </summary>
        /// <param name="input">DownloadInput</param>
        /// <param name="operation">FormOperationType</param>
        /// <returns>DownloadOutput</returns>
        public object Download(object input, FormOperationType operation = FormOperationType.Execute)
        {
            StartExecution();
            object downloadclient = Activator.CreateInstance(DownloadClient);
            //dynamic downloadoutput = Activator.CreateInstance(DownloadOutput);
            string beginmethod = this.BeginMethod[operation] as string;
            string endmethod = this.EndMethod[operation] as string;
            try
            {
                this.SetRequestHeaders(downloadclient);
                this.Result = downloadclient.GetType().GetMethod(beginmethod).Invoke(downloadclient, new object[] { input, null, null }) as IAsyncResult;
                this.Result.AsyncWaitHandle.WaitOne();
                this.Result.AsyncWaitHandle.Close();
                object downloadoutput = downloadclient.GetType().GetMethod(endmethod).Invoke(downloadclient, new object[] { this.Result });
                downloadclient.GetType().GetMethod("Close").Invoke(downloadclient, new object[] { });

                return downloadoutput;
            }
            catch (Exception ex)
            {
                ErrorExecution(ex.Message);
                ex.SaveLog();
                throw;
            }
            finally
            {
                StopExecution();
                this.LogRequestAsync(downloadclient, null, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
            }
        }

        /// <summary>
        /// Sum
        /// </summary>
        /// <param name="input">SumInput</param>
        /// <returns>object</returns>
        public object Sum(object input)
        {
            StartExecution();
            object sumclient = Activator.CreateInstance(SumClient);
            //object sumoutput = Activator.CreateInstance(SumOutput);
            string beginmethod = this.BeginMethod[FormOperationType.Sum] as string;
            string endmethod = this.EndMethod[FormOperationType.Sum] as string;
            try
            {
                this.SetRequestHeaders(sumclient);
                this.ResultSum = sumclient.GetType().GetMethod(beginmethod).Invoke(sumclient, new object[] { input, null, null }) as IAsyncResult;
                this.ResultSum.AsyncWaitHandle.WaitOne();
                this.ResultSum.AsyncWaitHandle.Close();
                dynamic sumoutput = sumclient.GetType().GetMethod(endmethod).Invoke(sumclient, new object[] { this.ResultSum });
                sumclient.GetType().GetMethod("Close").Invoke(sumclient, new object[] { });

                

                if (!string.IsNullOrEmpty(SumMember))
                {
                    sumoutput = sumoutput.GetType().GetProperty(SumMember).GetValue(sumoutput);
                    List<dynamic> listData = new List<dynamic>();
                    listData.AddRange(sumoutput);

                    if (listData.Count() > 0)
                    {
                        return listData[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return sumoutput;
                }

                

            }
            catch (Exception ex)
            {
                ErrorExecution(ex.Message);
                ex.SaveLog();
                return null;
            }
            finally
            {
                StopExecution();
                this.LogRequestAsync(sumclient, null, input, ExecutionError, ExecutionTime.Elapsed, ExecutionStatus);
            }
        }

        /// <summary>
        /// SumTask
        /// </summary>
        /// <param name="input">SumInput</param>
        /// <returns>object</returns>
        public async Task<object> SumAsync(object input)
        {
            return await Task.Run(() => Sum(input));
        }

        /// <summary>
        /// SumTask
        /// </summary>
        /// <param name="input">SumInput</param>
        /// <returns>object</returns>
        public async Task<T> SumAsync<T>(object input) where T : new()
        {
            object obj = await SumAsync(input);
            return (T)obj;
        }

       


        /// <summary>
        /// Download T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="operation"></param>
        /// <returns>T</returns>
        public T Download<T>(object input, FormOperationType operation = FormOperationType.Execute) where T : new()
        {
            return (T)this.Download(input, operation);
        }

        /// <summary>
        /// DownloadTask T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="operation"></param>
        /// <returns>T</returns>
        public async Task<T> DownloadTask<T>(object input, FormOperationType operation = FormOperationType.Execute) where T : new()
        {
            return await Task.Run(() => (T)Download(input, operation));
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

                if (request != null)
                {
                    if(request.Endpoint != null)
                    {
                        req.Address = request.Endpoint.Address.Uri.AbsoluteUri;
                    }
                }
                req.Method = "POST";
                req.Format = "XML";
                req.Input = input.SerializeObject();
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
                    LogRequest(request, response, input, output, executionTime,status);
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
