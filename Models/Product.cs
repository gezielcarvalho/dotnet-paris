namespace DotNetParis.Models
{
    // Represents a product with an ID and a name.
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
