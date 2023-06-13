using System;
using System.Reflection;

namespace Ione.Framework.Core.Utils
{
    public static class AssemblyInfo
    {
        public static string CallingName()
        {
            try
            {
                Assembly assembly = Assembly.GetEntryAssembly();
                if (assembly == null) return "";
                return assembly.GetName().Name.ToString();
            }
            catch (Exception ex)
            {
                ex.SaveLog();
                return "";
            }
        }
        public static string CallingVersion()
        {
            try
            {
                Assembly assembly = Assembly.GetEntryAssembly();
                if (assembly == null) return "";
                Version version  = assembly.GetName().Version;
                return String.Format("{0}.{1}", version.Major, version.Minor);
            }
            catch (Exception ex)
            {
                ex.SaveLog();
                return "";
            }
        }
    }
}
