namespace BLL.Models
{
    public class MultipleData<T>
    {
        public IEnumerable<T> Data { get; set; } = default!;
    }
}
