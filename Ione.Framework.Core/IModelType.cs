using System.Threading.Tasks;


namespace Ione.Framework.Core
{
    public partial interface IModelType
    {
        Task<T> SelectAsync<T>(object input) where T : new();
        Task<T> SumAsync<T>(object input) where T : new();
        Task<T> ExecuteAsync<T>(object input, FormOperationType operation = FormOperationType.Execute) where T : new();
        Task<T> UploadAsync<T>(object input, FormOperationType operation = FormOperationType.Execute) where T : new();
    }
}
