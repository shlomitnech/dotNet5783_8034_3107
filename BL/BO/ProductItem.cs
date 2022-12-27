using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductItem
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double? Price { get; set; }
    public Enums.Category? Category { get; set; }
    public int Amount { get; set; }
    public bool InStock { get; set; }

    public override string ToString() => $@"
	The product's ID is: {ID}
    The product's name is: {Name}
    The product's category is: {Category}
    The product's price is: {Price}
    The product's amount is: {Amount}
    The products stock is: {InStock}
";
}
