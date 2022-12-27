using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderForList
{
    public int ID { get; set; } //order ID
    public string? CustomerName { get; set; } 
    public Enums.OrderStatus Status { get; set; }
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
	Order's ID : {ID},
	Customer's Name is: {CustomerName}
    The order's status is: {Status}
    The amount of items is: {AmountOfItems}
    The order's total price is: {TotalPrice}

";

}
