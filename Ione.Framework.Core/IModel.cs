using RestSharp;
using System;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Ione.Framework.Core
{
    /// <summary>
    /// IModel 
    /// Interface modeling berisi fungsi-fungsi untuk melakukan crud data ke database, rest service dan soap service
    /// </summary>
    public partial interface IModel
    {
        bool EnabledLog { get; set; }
        Stopwatch ExecutionTime { get; set; }
        bool ExecutionStatus { get; set; }
        string ExecutionError { get; set; }
        ModelOptionType ModelOption { get; }
        int SelectCount { get; set; }
        IList SelectList { get; set; }
        Type SelectRow { get; set; }
        int TotalRecords { get; set; }
        Task<object> SelectAsync(object input);
        Task<object> SumAsync(object input);
        Task<object> ExecuteAsync(object input, FormOperationType operation = FormOperationType.Execute);
        Task<object> UploadAsync(object input, FormOperationType operation = FormOperationType.Execute);
        object Download(object input, FormOperationType operation = FormOperationType.Execute);
        int Count(object input, FormOperationType operation = FormOperationType.Execute);

        void LogRequest(dynamic request, dynamic response, object input, object output, TimeSpan executionTime, bool status);
        Task LogRequestTask(dynamic request, dynamic response, object input, object output, TimeSpan executionTime, bool status);
        void LogRequestAsync(dynamic request, dynamic response, object input, object output, TimeSpan executionTime, bool status);
        void StartExecution();
        void StopExecution();
        void ErrorExecution(string message);

    }

    public partial interface IServiceModel
    {
        bool EnabledLog { get; set; }
        Stopwatch ExecutionTime { get; set; }
        bool ExecutionStatus { get; set; }
        string ExecutionError { get; set; }
        ModelOptionType ModelOption { get; }
        Task<object> GetAsync(object input);
        Task<object> GetRowAsync(object input);
        Task<object> PostAsync(object input);
        Task<object> PutAsync(object input);
        Task<object> DeleteAsync(object input);
        byte[] Download(object input);
    }
}
