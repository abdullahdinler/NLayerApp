namespace NLayer.Core.Models
{
    public class Category : BaseEntity
    {
        public ICollection<Product>? Products { get; set; }
    }
}
