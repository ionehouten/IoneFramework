namespace Ione.Framework.Soap
{
    /// <summary>
    /// IViewModel
    /// Interface ViewModel khusus SOAP
    /// </summary>
    public partial interface IViewModel  : Core.IViewModel
    {
        /// <summary>
        /// Model
        /// SOAP Model
        /// </summary>
        new Model Model { get; set; }
    }
}
