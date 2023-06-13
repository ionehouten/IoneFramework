using System.Threading.Tasks;

namespace Ione.Framework.Core
{
    /// <summary>
    /// IMore 
    /// Interface untuk reload data
    /// </summary>
    public interface IReload
    {
        Task Reload();
        void ReloadAsync();
    }
}
