using System;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Belant.Framework.Core
{
    /// <summary>
    /// IModel 
    /// Interface modeling berisi fungsi-fungsi untuk melakukan crud data ke database, rest service dan soap service
    /// </summary>
    public partial interface IModel2
    {
        ModelOptionType ModelOption { get; }
        Stopwatch ExecutionTime { get; set; }
        bool ExecutionStatus { get; set; }
        string ExecutionError { get; set; }
        void LogRequest(dynamic request, dynamic response, object input, object output, TimeSpan executionTime, bool status);
        Task LogRequestTask(dynamic request, dynamic response, object input, object output, TimeSpan executionTime, bool status);
        void LogRequestAsync(dynamic request, dynamic response, object input, object output, TimeSpan executionTime, bool status);
        void StartExecution();
        void StopExecution();
        void ErrorExecution(string message);

    }
}
