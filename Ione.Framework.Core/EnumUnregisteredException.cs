using System;

namespace Ione.Framework.Core
{
    public class EnumUnregisteredException : Exception
    {
        public EnumUnregisteredException(string message)
            : base(message)
        {
        }
    }
}
