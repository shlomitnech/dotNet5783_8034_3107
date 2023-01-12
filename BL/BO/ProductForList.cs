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

    public override string ToString() =>
        $@"    Product's ID: {ID}
    Product's name: {Name}
    Product's price: {Price}
    Product's category: {Category}
";
}
