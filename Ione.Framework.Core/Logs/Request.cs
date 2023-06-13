using Ione.Framework.Core.Utils;
using System;
using System.Collections.Generic;

namespace Ione.Framework.Core.Logs
{
    [Serializable]
    public class Request
    {
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public string Response { get; set; }
        public DeviceInfo DeviceInfo { get; set; }
        public List<DeviceInfo> DeviceInfos{ get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public bool Status { get; set; }
        public string Method { get; set; }
        public string Format { get; set; }
    }
    [Serializable]
    public class DeviceInfo
    {
        public string CpuId { get; set; }
        public string HardDrive { get; set; }
        public string MacAddress { get; set; }
        public string VolumeSerial { get; set; }
        public string IpAddress { get; set; }
        public List<string> IpAddresses { get; set; }
        public string AssemblyName { get; set; }
        public string AssemblyVersion { get; set; }
        public string MachineName { get; set; }
        public bool Is64BitOperatingSystem { get; set; }
        public string OSVersion { get; set; }
        public int ProcessorCount { get; set; }
        public string UserName { get; set; }

    }
}
