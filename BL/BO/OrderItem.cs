using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderItem
{
    public int ID { get; set; }
    public int ProductID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }

    public override string ToString() => $@"
	Order Item's ID is: {ID}
    The product's ID is: {ProductID}
    The price of the order is: {Price}
    The amount of the item is: {Amount}
	
";
}
