namespace OnlineStore.Api.Models;

public partial class Category
{
    public int Categoryid { get; set; }

    public string? Nameofcategory { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
