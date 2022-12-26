using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductForList
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double? Price { get; set; }
    public Enums.Category? Category { get; set; }

    public override string ToString() => $@"
	The product's ID is: {ID}
    The product's name is: {Name}
    The product's price is: {Price}
    The product's category is: {Category}
";
}
