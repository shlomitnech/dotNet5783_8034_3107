namespace DO;
/// <summary>
/// The information about the specific products for the adminstrator
/// </summary>

public struct Product
{
    public static int productCounter = 100000;

    public Product()
    {
        Name = "";
        Price = 0;
        inStock = 0;
        Category = Enums.Category.other;
    }
    public Product(int id)
    {
        ID = id;
        Name = "";
        Price = 0;
        inStock = 0;
        Category = Enums.Category.other;
    }

    public int ID { get; init; } = productCounter++;
    public string? Name { get; set; }
    public double? Price { get; set; }
    public Enums.Category? Category { get; set; }
    public int inStock { get; set; }    

    public override string ToString() => $@"
    ID: {ID}
    Product Name: {Name}
    Product Price: {Price}
    Category: {Category}
    Amount in Stock: {inStock}
  
";
}
