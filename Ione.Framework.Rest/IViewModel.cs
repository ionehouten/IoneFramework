namespace Belant.Framework.Rest
{
    /// <summary>
    /// IViewModel
    /// Interface ViewModel khusus REST
    /// </summary>
    public partial interface IViewModel : Core.IViewModel
    {
        /// <summary>
        /// Model
        /// REST Model
        /// </summary>
        new Model Model { get; set; }
    }
}
