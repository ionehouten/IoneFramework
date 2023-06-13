using System.Threading.Tasks;


namespace Ione.Framework.Core
{
    public partial interface IController : IMore
    {
        FormOptionType FormOption { get; set; }
        FormOperationType FormOperation { get; set; }
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
    }
}
