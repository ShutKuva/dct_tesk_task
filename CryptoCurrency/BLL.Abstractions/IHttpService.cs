namespace BLL.Abstractions
{
    public interface IHttpService
    {
        Task<T?> GetObject<T>(string relativePath, IDictionary<string, string> query = null!);
    }
}
