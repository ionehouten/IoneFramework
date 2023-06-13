using System;

namespace Ione.Framework.Core.Logs
{
    [Serializable]
    public class Error
    {
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
    }
}
