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

    // Derived classes to demonstrate LSP

    public class PublicProduct : Product
    {
        public PublicProduct(int id, string name) : base(id, name)
        {
            // Public-specific behavior can be added here if needed
        }
    }

    public class PrivateProduct : Product
    {
        public PrivateProduct(int id, string name) : base(id, name)
        {
            // Private-specific behavior can be added here if needed
        }
    }
}
