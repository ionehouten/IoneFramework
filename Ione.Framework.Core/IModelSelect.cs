using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Belant.Framework.Core
{
    /// <summary>
    /// IModelSelect
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IModelSelect<TInput, TOutput> 
    {
        /// <summary>
        /// SelectClient
        /// </summary>
        Type SelectClient { get; set; }

        /// <summary>
        /// SelectOutput
        /// </summary>
        Type SelectOutput { get; set; }

        /// <summary>
        /// SelectRow
        /// </summary>
        Type SelectRow { get; set; }

        /// <summary>
        /// SelectMember
        /// </summary>
        string SelectMember { get; set; }

        /// <summary>
        /// SelectList
        /// </summary>
        IList SelectList { get; set; }

        /// <summary>
        /// TotalRecords
        /// </summary>
        int TotalRecords { get; set; }

        /// <summary>
        /// Row
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OutputParameters<TOutput> Row(TInput input);

        /// <summary>
        /// RowTask
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OutputParameters<TOutput>> RowTask(TInput input);

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OutputParameters<List<TOutput>> Select(TInput input);


        /// <summary>
        /// SelectTask
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OutputParameters<List<TOutput>>> SelectTask(TInput input);


    }
    
}
