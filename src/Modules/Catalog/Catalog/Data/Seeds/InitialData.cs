namespace Catalog.Data.Seeds
{
    public static class InitialData
    {
        public static IEnumerable<Product> Products =>
            new List<Product>
            {
                Product.Create(new Guid(), "IPhone X", ["category1"], "Description1", "image1", 1000),
                Product.Create(new Guid(), "Motorolla", ["category1, category2"], "Description2", "image2", 750),
                Product.Create(new Guid(), "Samsung 12", ["category3"], "Description3", "image3", 900)
            };
    }
}
