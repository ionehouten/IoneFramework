using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Ione.Framework.Core
{
    /// <summary>
    /// IoneException
    /// </summary>
    [Serializable]
    public class IoneException : Exception
    {
        /// <summary>
        /// ErrorCode
        /// </summary>
        public virtual int ErrorCode { get; }

        /// <summary>
        /// IoneException()
        /// </summary>
        public IoneException()
        : base() { }

        /// <summary>
        /// IoneException(string message)
        /// </summary>
        /// <param name="message"></param>
        public IoneException(string message)
        : base(message) { }

        /// <summary>
        /// IoneException(int errorCode)
        /// </summary>
        /// <param name="errorCode"></param>
        public IoneException(int errorCode)
        {
            this.ErrorCode = errorCode;
            CustomException item = CustomListException.Find(errorCode.ToString());
            if(item != null)
            {
                throw new IoneException(item.ErrorMessage);
            }
        }

        /// <summary>
        /// IoneException(int errorCode, string message)
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        public IoneException(int errorCode, string message) : base(message)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// IoneException(string format, params object[] args)
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public IoneException(string format, params object[] args)
        : base(string.Format(format, args)) { }

        /// <summary>
        /// IoneException(string message, Exception innerException)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public IoneException(string message, Exception innerException)
        : base(message, innerException) { }

        /// <summary>
        /// IoneException(string format, Exception innerException, params object[] args)
        /// </summary>
        /// <param name="format"></param>
        /// <param name="innerException"></param>
        /// <param name="args"></param>
        public IoneException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException) { }

        /// <summary>
        /// IoneException(SerializationInfo info, StreamingContext context)
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected IoneException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
    }

    /// <summary>
    /// CustomListException
    /// </summary>
    public static class CustomListException
    {

        /// <summary>
        /// ListException
        /// SOAP ERROR CODE 03
        /// REST ERROR CODE 04
        /// </summary>
        public static List<CustomException> ListException = new List<CustomException>()
        {
            new CustomException() { ErrorCode = "0301", ErrorMessage = "SelectClient belum di definisikan"},
            new CustomException() { ErrorCode = "0301", ErrorMessage = "SelectClient belum di definisikan"},
            new CustomException() { ErrorCode = "0301", ErrorMessage = "SelectClient belum di definisikan"},
            new CustomException() { ErrorCode = "0301", ErrorMessage = "SelectClient belum di definisikan"},
            new CustomException() { ErrorCode = "0301", ErrorMessage = "SelectClient belum di definisikan"},
            new CustomException() { ErrorCode = "0301", ErrorMessage = "SelectClient belum di definisikan"},
        };

        /// <summary>
        /// Find
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static CustomException Find(string errorCode)
        {
            CustomException item = CustomListException.ListException.Where(x => x.ErrorCode == errorCode).FirstOrDefault();
            if(item == null)
            {
                item = new CustomException() { ErrorCode = "999999", ErrorMessage = "Undefined Error" };
            }
            return item;
        }
    }

    /// <summary>
    /// CustomException
    /// </summary>
    public class CustomException
    {
        /// <summary>
        /// ErrorCode
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// ErrorMessage
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// InnerException
        /// </summary>
        public Exception InnerException { get; set; }
    }
}
