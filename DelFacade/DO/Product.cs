namespace DO;
/// <summary>
/// The information about the specific products for the adminstrator
/// </summary>

public struct Product
{
    public int ID { get; set; }
    public string name { get; set; }
    public double price { get; set; }
    public Enums.Category Category { get; set; }
    public int inStock { get; set; }    
    public bool isDeleted { get; set; }

    public override string ToString() => $@"
    ID: {ID},
    Product Name: {name},
    Category: {Category},
    Amount in Stock: {inStock},
  
";
}
