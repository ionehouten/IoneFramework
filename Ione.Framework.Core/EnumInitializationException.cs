using System;

namespace Ione.Framework.Core
{
    public class EnumInitializationException : Exception
    {
        public EnumInitializationException(string message)
            : base(message)
        {
        }
    }
}
