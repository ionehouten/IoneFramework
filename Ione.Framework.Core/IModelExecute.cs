using System.Threading.Tasks;

namespace Ione.Framework.Core
{
    /// <summary>
    /// IModelExecute
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IModelExecute<TInput, TOutput> 
    {
        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="input"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        TOutput Execute(TInput input, FormOperationType operation = FormOperationType.Execute);

        /// <summary>
        /// ExecuteTask
        /// </summary>
        /// <param name="input"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        Task<TOutput> ExecuteTask(TInput input, FormOperationType operation = FormOperationType.Execute);
    }
    
}
