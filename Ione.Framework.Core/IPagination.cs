using System.Threading.Tasks;

namespace Ione.Framework.Core
{
    /// <summary>
    /// IPagination 
    /// Interface untuk paging data
    /// </summary>
    public interface IPagination
    {
        /// <summary>
        /// First
        /// Halaman pertama
        /// </summary>
        /// <returns>Task</returns>
        Task First();
        /// <summary>
        /// Prev
        /// Halaman sebelumnya
        /// </summary>
        /// <returns>Task</returns>
        Task Prev();
        /// <summary>
        /// Next
        /// Halaman selanjutnya
        /// </summary>
        /// <returns>Task</returns>
        Task Next();
        /// <summary>
        /// Last
        /// Halaman terakhir
        /// </summary>
        /// <returns>Task</returns>
        Task Last();
        /// <summary>
        /// SelectedPage
        /// Pilih Halaman
        /// </summary>
        /// <returns>Task</returns>
        Task SelectedPage();
        /// <summary>
        /// SelectedPerPage
        /// Jumlah baris per halaman
        /// </summary>
        /// <returns></returns>
        Task SelectedPerPage();
        /// <summary>
        /// FirstAsync
        /// </summary>
        void FirstAsync();
        /// <summary>
        /// PrevAsync
        /// </summary>
        void PrevAsync();
        /// <summary>
        /// NextAsync
        /// </summary>
        void NextAsync();
        /// <summary>
        /// LastAsync
        /// </summary>
        void LastAsync();
        /// <summary>
        /// SelectedPageAsync
        /// </summary>
        void SelectedPageAsync();
        /// <summary>
        /// SelectedPerPageAsync
        /// </summary>
        void SelectedPerPageAsync();
    }
}
