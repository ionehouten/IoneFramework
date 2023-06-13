using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Ione.Framework.Core
{
    /// <summary>
    /// Delegate void dengan parameter objek
    /// </summary>
    /// <param name="invoke"></param>
    public delegate void InvokeHandler(object invoke = null);
    /// <summary>
    /// Delegate void dengan parameter objek dan bool
    /// </summary>
    /// <param name="invoke"></param>
    /// <param name="status"></param>
    public delegate void InvokeHandlerStatus(object invoke , bool status);
    /// <summary>
    /// Delegate void tanpa parameter
    /// </summary>
    public delegate void InvokeHandlerNoParam();
    /// <summary>
    /// Delegate Task dengan parameter objek
    /// </summary>
    /// <param name="invoke"></param>
    /// <returns>Returns Task</returns>
    public delegate Task InvokeHandlerTask(dynamic invoke = null);
    /// <summary>
    /// Delegate Task tanpa parameter
    /// </summary>
    /// <returns>Returns Task</returns>
    public delegate Task InvokeHandlerTaskNoParam();

    /// <summary>
    /// MatchParentAttribute
    /// Attribute untuk mencocokan property dengan object yang berbeda
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MatchParentAttribute : Attribute
    {
        /// <summary>
        /// ParentPropertyName
        /// </summary>
        public readonly string ParentPropertyName;

        /// <summary>
        /// MatchParentAttribute
        /// </summary>
        /// <param name="parentPropertyName"></param>
        public MatchParentAttribute(string parentPropertyName)
        {
            ParentPropertyName = parentPropertyName;
        }
    }

    /// <summary>
    /// AccessMenu untuk pengaturan visibility pada RibbonMenu atau BarMenu
    /// </summary>
    [Serializable]
    public class AccessMenu
    {
        private bool enabled = true;
        private bool visible = true;
        /// <summary>
        /// AccessMenu
        /// </summary>
        public AccessMenu() { }
        /// <summary>
        /// PropertyName nama properti pada BarButtonItem, BarCheckItem etc.
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// Readonly True untuk readonly, false not readonly
        /// </summary>
        public bool Enabled
        {
            get { return this.enabled; }
            set { this.enabled = value; }
        }
        /// <summary>
        /// Visibility True untuk visible, False untuk invisible
        /// </summary>
        public bool Visibility
        {
            get { return this.visible; }
            set { this.visible = value; }
        }
    }

    /// <summary>
    /// AccessForm untuk pengaturan readonly pada property
    /// </summary>
    [Serializable]
    public class AccessForm
    {
        private bool readOnly = false;
        private bool visible = true;

        /// <summary>
        /// AccessForm
        /// </summary>
        public AccessForm() { }
        /// <summary>
        /// ParentPropertyName nama properti induk dari PropertyName, PanelControl, LayoutControl, etc. 
        /// </summary>
        public string ParentPropertyName { get; set; }
        /// <summary>
        /// PropertyName nama properti pada TextEdit,DateEdit, etc. 
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// Readonly True untuk readonly, false not readonly
        /// </summary>
        public bool Readonly
        {
            get { return this.readOnly; }
            set { this.readOnly = value; }
        }
        public bool Visibility
        {
            get { return this.visible; }
            set { this.visible = value; }
        }
    }

    /// <summary>
    /// AccessOperation untuk pengaturan Visible/Enable pada RibbonMenu atau BarMenu
    /// </summary>
    [Serializable]
    public class AccessOperation
    {
        private bool enable = true;
        private bool visible = true;

        /// <summary>
        /// AccessOperation
        /// </summary>
        public AccessOperation() { }
        /// <summary>
        /// FormOperation type form operation
        /// </summary>
        public FormOperationType FormOperation { get; set; }
        /// <summary>
        /// ParentPropertyName nama properti induk dari PropertyName, PanelControl, LayoutControl, etc. 
        /// </summary>
        public string ParentPropertyName { get; set; }
        /// <summary>
        /// PropertyName nama properti pada BarButtonItem, BarCheckItem etc.
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// Enable True untuk enable, False untuk disable
        /// </summary>
        public bool Enable
        {
            get { return this.enable; }
            set { this.enable = value; }
        }
        /// <summary>
        /// Visibility True untuk visible, False untuk invisible
        /// </summary>
        public bool Visible
        {
            get { return this.visible; }
            set { this.visible = value; }
        }
    }

    /// <summary>
    /// FormOperationType Mendefinisikan himpunan nilai yang digunakan untuk menentukan mode form operation.
    /// </summary>
    public enum FormOperationType : int
    {
        /// <summary>
        /// ReadOnly
        /// </summary>
        ReadOnly,
        /// <summary>
        /// Read
        /// </summary>
        Read,
        /// <summary>
        /// Create
        /// </summary>
        Create,
        /// <summary>
        /// Update
        /// </summary>
        Update,
        /// <summary>
        /// Delete
        /// </summary>
        Delete,
        /// <summary>
        /// Detail
        /// </summary>
        Detail,
        /// <summary>
        /// Disable
        /// </summary>
        Disable,
        /// <summary>
        /// Cancel
        /// </summary>
        Cancel,
        /// <summary>
        /// Execute
        /// </summary>
        Execute,
        /// <summary>
        /// Count
        /// </summary>
        Count,
        /// <summary>
        /// Sum
        /// </summary>
        Sum
    }

    /// <summary>
    /// EnvironmentType Mendefinisikan himpunan nilai yang digunakan untuk menentukan mode environment.
    /// </summary>
    public enum EnvironmentType : int
    {
        /// <summary>
        /// Development
        /// </summary>
        Development,
        /// <summary>
        /// Testing
        /// </summary>
        Testing,
        /// <summary>
        /// Production
        /// </summary>
        Production,
    }

    /// <summary>
    /// FormOptionType Mendefinisikan himpunan nilai yang digunakan untuk menentukan mode form option.
    /// </summary>
    public enum FormOptionType : int
    {
        /// <summary>
        /// Default menggunakan form dari Grid atau turunan dari Grid
        /// </summary>
        Default,
        /// <summary>
        /// Advanced menggunakan form custom
        /// </summary>
        Advanced,
    }

    /// <summary>
    /// PopUpType Mendefinisikan jenis pilihan
    /// </summary>
    public enum PopUpType : int
    {
        /// <summary>
        /// Default menggunakan form dari Grid atau turunan dari Grid
        /// </summary>
        SingleSelection,
        /// <summary>
        /// Advanced menggunakan form custom
        /// </summary>
        MultipleSelection,
    }

    /// <summary>
    /// AdvancedCreateType Mendefinisikan himpunan nilai yang digunakan untuk menentukan mode form setelah simpan data.
    /// </summary>
    public enum FormAdvancedCreateType : int
    {
        /// <summary>
        /// Default
        /// </summary>
        Default,
        /// <summary>
        /// ReloadRow
        /// </summary>
        ReloadRow,
        /// <summary>
        /// HistoryRow
        /// </summary>
        HistoryRow
    }

    /// <summary>
    /// PagingOptionType Mendefinisikan himpunan nilai yang digunakan untuk menentukan mode paging option.
    /// </summary>
    public enum PagingOptionType : int
    {
        /// <summary>
        /// Default paging untuk database MySql,PostgreSql, SqlServer, etc
        /// </summary>
        Default,
        /// <summary>
        /// OldOracle paging untuk Oracle11g kebawah
        /// </summary>
        OldOracle,
    }

    /// <summary>
    /// PagingViewType Mendefinisikan himpunan nilai yang digunakan untuk menentukan mode paging view.
    /// </summary>
    public enum PagingViewType : int
    {
        /// <summary>
        /// ScrollView tampilan hanya ada tombol more dan scroll untuk menampilkan data berikutnya
        /// </summary>
        ScrollView,
        /// <summary>
        /// PageView tampilan terdapat tombol First, Prev, Next, Last, etc.
        /// </summary>
        PageView,
    }

    /// <summary>
    /// PagingViewType Mendefinisikan himpunan nilai yang digunakan untuk menentukan mode load data.
    /// </summary>
    public enum LoadOptionType : int
    {
        /// <summary>
        /// Default
        /// </summary>
        Default,
        /// <summary>
        /// Reload
        /// </summary>
        Reload,
        /// <summary>
        /// More
        /// </summary>
        More,
        /// <summary>
        /// Page
        /// </summary>
        Page
    }

    /// <summary>
    /// SearchQueryType Mendefinisikan himpunan nilai yang digunakan untuk pengaturan query search.
    /// </summary>
    public enum SearchQueryType : int
    {
        /// <summary>
        /// QueryString
        /// </summary>
        QueryString,
        /// <summary>
        /// QueryObject
        /// </summary>
        QueryObject
    }

    /// <summary>
    /// LoadingOptionType Mendefinisikan himpunan nilai yang digunakan untuk pengaturan loading.
    /// </summary>
    public enum LoadingOptionType : int
    {
        /// <summary>
        /// SplashScreen
        /// </summary>
        SplashScreen,
        /// <summary>
        /// ProgressBar
        /// </summary>
        ProgressBar
    }

    /// <summary>
    /// LoadingOptionType Mendefinisikan himpunan nilai yang digunakan untuk jenis-jenis model.
    /// </summary>
    public enum ModelOptionType : int
    {
        /// <summary>
        /// SOAP
        /// </summary>
        SOAP,
        /// <summary>
        /// REST
        /// </summary>
        REST
    }

    /// <summary>
    /// Config 
    /// berisi fungsi-fungsi konfigurasi
    /// </summary>
    public static partial class Config
    {
        /// <summary>
        /// PathApp
        /// Lokasi direktori aplikasi
        /// </summary>
        public static string PathApp = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        /// <summary>
        /// PathLogs
        /// Lokasi direktori log aplikasi
        /// </summary>
        public static string PathLogs = Path.Combine(PathApp, "Logs");
        /// <summary>
        /// Environment
        /// </summary>
        public static EnvironmentType Environment = EnvironmentType.Development;

        #region SHOW APPLICATION
        /// <summary>
        /// ShowCommands
        /// Command Open File/Explorer
        /// </summary>
        public enum ShowCommands : int
        {
            /// <summary>
            /// SW_HIDE
            /// </summary>
            SW_HIDE = 0,
            /// <summary>
            /// SW_SHOWNORMAL
            /// </summary>
            SW_SHOWNORMAL = 1,
            /// <summary>
            /// SW_NORMAL
            /// </summary>
            SW_NORMAL = 1,
            /// <summary>
            /// SW_SHOWMINIMIZED
            /// </summary>
            SW_SHOWMINIMIZED = 2,
            /// <summary>
            /// SW_SHOWMAXIMIZED
            /// </summary>
            SW_SHOWMAXIMIZED = 3,
            /// <summary>
            /// SW_MAXIMIZE
            /// </summary>
            SW_MAXIMIZE = 3,
            /// <summary>
            /// SW_SHOWNOACTIVATE
            /// </summary>
            SW_SHOWNOACTIVATE = 4,
            /// <summary>
            /// SW_SHOW
            /// </summary>
            SW_SHOW = 5,
            /// <summary>
            /// SW_MINIMIZE
            /// </summary>
            SW_MINIMIZE = 6,
            /// <summary>
            /// SW_SHOWMINNOACTIVE
            /// </summary>
            SW_SHOWMINNOACTIVE = 7,
            /// <summary>
            /// SW_SHOWNA
            /// </summary>
            SW_SHOWNA = 8,
            /// <summary>
            /// SW_RESTORE
            /// </summary>
            SW_RESTORE = 9,
            /// <summary>
            /// SW_SHOWDEFAULT
            /// </summary>
            SW_SHOWDEFAULT = 10,
            /// <summary>
            /// SW_FORCEMINIMIZE
            /// </summary>
            SW_FORCEMINIMIZE = 11,
            /// <summary>
            /// SW_MAX
            /// </summary>
            SW_MAX = 11
        }

        /// <summary>
        /// ShellExecute
        /// Execute Open File/Explorer
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lpOperation"></param>
        /// <param name="lpFile"></param>
        /// <param name="lpParameters"></param>
        /// <param name="lpDirectory"></param>
        /// <param name="nShowCmd"></param>
        /// <returns></returns>
        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(
            IntPtr hwnd,
            string lpOperation,
            string lpFile,
            string lpParameters,
            string lpDirectory,
            ShowCommands nShowCmd);

        #endregion

        /// <summary>
        /// RunProcess 
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="arguments"></param>
        public static void RunProcess(string processName, string arguments)
        {
            var newProcess = new ProcessStartInfo(processName);

            if (!string.IsNullOrEmpty(arguments))
                newProcess.Arguments = arguments;
            newProcess.CreateNoWindow = true;
            newProcess.ErrorDialog = true;
            newProcess.RedirectStandardError = true;
            newProcess.RedirectStandardInput = true;
            newProcess.RedirectStandardOutput = true;
            newProcess.UseShellExecute = false;
            using (var proc = new Process())
            {
                proc.StartInfo = newProcess;
                proc.Start();
                //Dialog.info(proc.StandardOutput.ReadToEnd());
                Console.WriteLine(proc.StandardOutput.ReadToEnd());
            }
        }

        /// <summary>
        /// CatIoneFramework
        /// </summary>
        public const string CatIoneFramework = "Ione Framework";
    }

    //public class EnvironmentType
    //{
    //    public int Value { get; set; }

    //    public const int Development = 0;
    //    public const int Testing = 1;
    //    public const int Production = 2;
    //    public EnvironmentType(int value)
    //    {
    //        Value = value;
    //    }

    //    public static implicit operator int(EnvironmentType option)
    //    {
    //        return option.Value;
    //    }

    //    public static implicit operator EnvironmentType(int value)
    //    {
    //        return new EnvironmentType(value);
    //    }
    //}

    //public class FormOptionType
    //{
    //    public int Value { get; set; }

    //    public const int Default = 0;
    //    public const int Advanced = 1;
    //    public FormOptionType(int value)
    //    {
    //        Value = value;
    //    }

    //    public static implicit operator int(FormOptionType option)
    //    {
    //        return option.Value;
    //    }

    //    public static implicit operator FormOptionType(int value)
    //    {
    //        return new FormOptionType(value);
    //    }
    //}

    //[Serializable]
    //public class FormOperationType
    //{
    //    public int Value { get; set; }

    //    public const int ReadOnly = 0;
    //    public const int Read = 1;
    //    public const int Create = 2;
    //    public const int Update = 3;
    //    public const int Delete = 4;
    //    public const int Detail = 5;
    //    public const int Disable = 6;
    //    public const int Cancel = 7;
    //    public const int Execute = 8;
    //    public const int Count = 9;
    //    public const int Sum = 10;
    //    public static int ReadOnly1 { get; set; }
    //    public FormOperationType(int value)
    //    {
    //        Value = value;
    //    }

    //    public static implicit operator int(FormOperationType operation)
    //    {
    //        return operation.Value;
    //    }

    //    public static implicit operator FormOperationType(int value)
    //    {
    //        return new FormOperationType(value);
    //    }
    //}


    //public class TestFormOperationEx : FormOperationType
    //{
    //    public TestFormOperationEx(int value) : base(value)
    //    { }
    //}
    //public class TestOperationExtension
    //{
    //    public TestOperationExtension()
    //    {
    //        FormOperationType cType = FormOperationType.Read;
    //        switch (cType)
    //        {
    //            case FormOperationType.ReadOnly:
    //                Console.WriteLine("ReadOnly");
    //                break;
    //            case FormOperationType.Read:
    //                Console.WriteLine("Read");
    //                break;
    //        }
    //    }
    //}


}
