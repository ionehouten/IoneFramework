using Ione.Framework.Core.Utils;
using System;
using System.Configuration;
using System.IO;

namespace Ione.Framework.Core
{

    /// <summary>
    /// Security
    /// Berisi fungsi-fungsi untuk keamanan aplikasi,encrypt app.config, log pengguna aplikasi, dll.
    /// </summary>
    public static partial class Security
    {
        private static string assemblyName;
        private static string assemblyVersion;
        //private static string macAddress ;
        //private static string cpuId;
        //private static string volumeSerial;
        //private static string hardDrive;
        //private static string ipAddress;
        //private static string totalCpuUsage;
        //private static string totalMemoryAvailable;
        //private static string totalPyshicalMemory;

        /// <summary>
        /// Untuk mengencrypt Configuration WCF/SOAP Service
        /// </summary>
        /// <param name="config"></param>
        /// <param name="bindings"></param>
        /// <param name="client"></param>
        /// <param name="appProvider"></param>
        /// <param name="appKey"></param>
        public static void ProtectConfiguration(Configuration config, ConfigurationSection bindings, ConfigurationSection client, string appProvider, string appKey)
        {
            try
            {
                if (!bindings.SectionInformation.IsProtected && !client.SectionInformation.IsProtected)
                {
                    /*Delete Key*/
                    Config.RunProcess(Path.Combine(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(), "aspnet_regiis.exe"), "-pz \"" + appKey + "\" ");
                    /*Create Key*/
                    Config.RunProcess(Path.Combine(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(), "aspnet_regiis.exe"), "-pc \"" + appKey + "\" ");
                    /*ACL Key*/
                    Config.RunProcess(Path.Combine(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(), "aspnet_regiis.exe"), "-pa \"" + appKey + "\" \"NT AUTHORITY\\NETWORK SERVICE\"");
                    /*Export Public Key*/
                    //Ione.Framework.Core.Config.RunProcess(Path.Combine(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(), "aspnet_regiis.exe"), "-px \"" + Config.AppKey + "\" \"" + Config.AppKey + "\" ");
                    bindings.SectionInformation.ProtectSection(appProvider);
                    client.SectionInformation.ProtectSection(appProvider);
                    config.Save();
                }

            }
            catch (Exception ex)
            {
                ex.SaveLog();
            }
        }

        /// <summary>
        /// Untuk mengencrypt Configuration yang lainnya
        /// </summary>
        /// <param name="config"></param>
        /// <param name="settings"></param>
        /// <param name="appProvider"></param>
        /// <param name="appKey"></param>
        public static void ProtectConfiguration(Configuration config, ConfigurationSection settings, string appProvider, string appKey)
        {
            try
            {
                if (!settings.SectionInformation.IsProtected)
                {
                    settings.SectionInformation.ProtectSection(appProvider);
                    config.Save();
                }

            }
            catch (Exception ex)
            {
                ex.SaveLog();
            }
        }

        /// <summary>
        /// Untuk mendapatkan informasi MacAddress pengguna aplikasi
        /// </summary>
        //public static string MacAddress
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(macAddress))
        //        {
        //            macAddress = HardwareInfo.GetMacAddress();
        //        }
        //        return macAddress;
        //    }
        //}

        /// <summary>
        /// Untuk mendapatkan informasi CpuId pengguna aplikasi
        /// </summary>
        //public static string CpuId
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(cpuId))
        //        {
        //            cpuId = HardwareInfo.GetCpuId();
        //        }
        //        return cpuId;
        //    }
        //}

        /// <summary>
        /// Untuk mendapatkan informasi VolumeSerial pengguna aplikasi
        /// </summary>
        //public static string VolumeSerial
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(volumeSerial))
        //        {
        //            volumeSerial = HardwareInfo.GetVolumeSerial();
        //        }
        //        return volumeSerial;
        //    }
        //}

        /// <summary>
        /// Untuk mendapatkan informasi HardDrive pengguna aplikasi
        /// </summary>
        //public static string HardDrive
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(hardDrive))
        //        {
        //            HardDrive drive = HardwareInfo.GetHardDrive();
        //            hardDrive = String.Format("Model : {0} Type : {1} SerialNo : {2} ", drive.Model, drive.Type, drive.SerialNo);
        //        }
        //        return hardDrive;
        //    }
        //}

        /// <summary>
        /// Untuk mendapatkan informasi IpAddress pengguna aplikasi
        /// </summary>

        //public static string IpAddress
        //{
        //    get
        //    {
        //        if (ipAddress == null)
        //        {
        //            ipAddress = HardwareInfo.GetIPAddress();
        //        }
        //        return ipAddress;
        //    }
        //}

        //public static string TotalCpuUsage
        //{
        //    get
        //    {
        //        if (totalCpuUsage == null)
        //        {
        //            totalCpuUsage = HardwareInfo.GetTotalCpuUsage();
        //        }
        //        return totalCpuUsage;
        //    }
        //}

        //public static string TotalMemoryAvailable
        //{
        //    get
        //    {
        //        if (totalMemoryAvailable == null)
        //        {
        //            totalMemoryAvailable = HardwareInfo.GetTotalMemoryAvailable();
        //        }
        //        return totalMemoryAvailable;
        //    }
        //}

        //public static string TotalPhysicalMemory
        //{
        //    get
        //    {
        //        if (totalPyshicalMemory == null)
        //        {
        //            totalPyshicalMemory = HardwareInfo.GetTotalPhysicalMemory();
        //        }
        //        return totalPyshicalMemory;
        //    }
        //}


        /// <summary>
        /// Untuk mendapatkan informasi Nama Aplikasi yang digunakan pengguna
        /// </summary>
        public static string AssemblyName
        {
            get
            {
                if (string.IsNullOrEmpty(assemblyName))
                {
                    assemblyName = AssemblyInfo.CallingName();
                }
                return assemblyName;
            }
        }

        /// <summary>
        /// Untuk mendapatkan informasi Versi Aplikasi yang digunakan pengguna
        /// </summary>
        public static string AssemblyVersion
        {
            get
            {
                if (string.IsNullOrEmpty(assemblyVersion))
                {
                    assemblyVersion = AssemblyInfo.CallingVersion();
                }
                return assemblyVersion;
            }
        }
        
    }


}
