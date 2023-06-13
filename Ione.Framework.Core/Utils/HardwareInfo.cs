using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace Belant.Framework.Core.Utils
{
    public static class HardwareInfo
    {
        public static string GetMacAddress()
        {
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                String sMacAddress = string.Empty;
                foreach (NetworkInterface adapter in nics)
                {
                    if (sMacAddress == String.Empty)// only return MAC Address from first card  
                    {
                        //IPInterfaceProperties properties = adapter.GetIPProperties(); Line is not required
                        sMacAddress = adapter.GetPhysicalAddress().ToString();
                    }
                }
                return sMacAddress;

            }
            catch (Exception ex)
            {
                ex.SaveLog();
                return "";
            }

        }
        public static string GetCpuId()
        {
            try
            {
                string cpuInfo = String.Empty;
                string temp = String.Empty;
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if (cpuInfo == String.Empty)
                    {// only return cpuInfo from first CPU
                        cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                    }
                }
                return cpuInfo;
            }
            catch (Exception ex)
            {
                ex.SaveLog();
                return "";
            }
        }
        public static string GetVolumeSerial(string strDriveLetter = "C")
        {
            try
            {
                if (strDriveLetter == "" || strDriveLetter == null) strDriveLetter = "C";
                ManagementObject disk =
                    new ManagementObject("win32_logicaldisk.deviceid=\"" + strDriveLetter + ":\"");
                disk.Get();
                return disk["VolumeSerialNumber"].ToString();
            }
            catch (Exception ex)
            {
                ex.SaveLog();
                return "";
            }

        }   
        public static HardDrive GetHardDrive()
        {
            try
            {
                List<HardDrive> hdCollection = new List<HardDrive>();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

                foreach (ManagementObject wmi_HD in searcher.Get())
                {
                    HardDrive hd = new HardDrive
                    {
                        Model = wmi_HD["Model"].ToString(),
                        Type = wmi_HD["InterfaceType"].ToString()
                    };
                    hdCollection.Add(hd);
                }

                ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

                int i = 0;
                foreach (ManagementObject wmi_HD in searcher2.Get())
                {
                    // get the hard drive from collection
                    // using index
                    if (i < hdCollection.Count())
                    {

                        if (wmi_HD["SerialNumber"] == null)
                            hdCollection[i].SerialNo = "None";
                        else
                            hdCollection[i].SerialNo = wmi_HD["SerialNumber"].ToString();
                    }


                    ++i;
                }

                if (hdCollection.Count > 0)
                {
                    return hdCollection[0];
                }
                else
                {
                    HardDrive hd = new HardDrive
                    {
                        Model = "Undefined",
                        SerialNo = "Undefined",
                        Type = "Undefined"
                    };
                    return hd;
                }


            }
            catch (Exception ex)
            {
                ex.SaveLog();
                HardDrive hd = new HardDrive
                {
                    Model = "Undefined",
                    SerialNo = "Undefined",
                    Type = "Undefined"
                };
                return hd;
            }

        }
        public static IpAddress GetMyIp()
        {
            IpAddress output = new IpAddress();
            try
            {
                string url = "http://freegeoip.net/xml/";
                WebClient wc = new()
                {
                    Proxy = null
                };
                MemoryStream ms = new MemoryStream(wc.DownloadData(url));

                StreamReader sr = new StreamReader(ms);
                string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
                xml += sr.ReadToEnd();
                ms.Dispose();
                output = xml.DeserializeObject<IpAddress>();
                /*
                <Response>
                    <Ip>93.139.127.187</Ip>
                    <CountryCode>HR</CountryCode>
                    <CountryName>Croatia</CountryName>
                    <RegionCode>16</RegionCode>
                    <RegionName>Varazdinska</RegionName>
                    <City>Varazdinske Toplice</City>
                    <ZipCode/>
                    <Latitude>46.2092</Latitude>
                    <Longitude>16.4192</Longitude>
                    <MetroCode/>
                </Response>
                 */
            }
            catch (Exception ex)
            {
                ex.SaveLog();
            }

            return output;
        }

        public static string GetIPAddress()
        {
            string output = "";
            try
            {

                output = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                output = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                             .Matches(output)[0].ToString();
            }
            catch { }

            return output;
        }

        public static string GetTotalCpuUsage()
        {
            string output = "0%";
            try
            {
                ManagementObject processor = new ManagementObject("Win32_PerfFormattedData_PerfOS_Processor.Name='_Total'");
                processor.Get();

                output = processor.Properties["PercentProcessorTime"].Value.ToString() + "%";
                try
                {
                    if (processor != null)
                        processor.Dispose();
                }
                catch { }
            }
            catch { }
            return output;
        }
        public static string GetTotalMemoryAvailable()
        {
            string output = "";
            try
            {
                PerformanceCounter pCntr = new PerformanceCounter("Memory", "Available MBytes");
                pCntr.NextValue();
                Thread.Sleep(1);
                output = pCntr.NextValue() + " MB";
                try
                {
                    if (pCntr != null)
                        pCntr.Dispose();
                }
                catch { }
            }
            catch { }
            return output;
        }
        public static string GetTotalPhysicalMemory()
        {
            string output = "";
            try
            {

                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject item in moc)
                {
                    return Convert.ToString(Math.Round(Convert.ToDouble(item.Properties["TotalPhysicalMemory"].Value) / 1073741824, 2)) + " GB";
                }

            }
            catch { }
            return output;
        }
    }
    [Serializable]
    public class HardDrive
    {
        public string Model { get; set; }
        public string Type { get; set; }
        public string SerialNo { get; set; }
    }
    [Serializable]
    [XmlRoot("Response")]
    public class IpAddress
    {
        public string IP { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string TimeZone { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MetroCode { get; set; }
    }
}
