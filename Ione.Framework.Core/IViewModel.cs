using System.Threading.Tasks;


namespace Ione.Framework.Core
{
    /// <summary>
    /// IViewModel 
    /// Interface untuk ViewModel, ViewModel adalah yang menghubungkan Model dan View
    /// </summary>
    public partial interface IViewModel :IReload, IMore, IPagination
    {
        int TotalData { get; set; }
        decimal? Min { get; set; }
        decimal? Max { get; set; }
        decimal? NextMin { get; }
        decimal? NextMax { get; }
        IModel Model { get; set; }
        FormOptionType FormOption { get; set; }
        FormOperationType FormOperation { get; set; }
        PagingOptionType PagingOption { get; set; }
        PagingViewType PagingView{ get; set; }
        LoadOptionType LoadOption { get; set; }
        LoadingOptionType LoadingOption { get; set; }
        FormAdvancedCreateType FormAdvancedAfterCreate { get; set; }
        bool EnabledScrollMore { get; set; }
        object Input { get; set; }
        object Selected { get; }
        void Init();
        Task Load();
        void Add();
        void Edit();
        void Delete();
        void Search();
        void Export();
        Task Save();
        bool Validation();
        void RefreshBinding();
        void ShowForm();
        void CloseForm();
    }
}
