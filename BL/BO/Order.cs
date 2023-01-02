using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Order
{
    public int ID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public DateTime? OrderDate { get; set; }
    public Enums.OrderStatus Status { get; set; }
    public DateTime? PaymentDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public List<BO.OrderItem?>? Items { get; set; }
    public double? TotalPrice { get; set; }

    public override string ToString() => $@"
	Orders's ID is: {ID},
	Customer's Name is: {CustomerName}
	Customer's email is: {CustomerEmail}
	Customer's address is: {CustomerAddress}
	The order was placed on: {OrderDate}
    The order's status is: {Status}
    The order's payment date was: {ShipDate}
	The order's shipping date is: {ShipDate}
	The order's delivery date is: {DeliveryDate}
    The order's items' are: {Items}
    The order's total price is: {TotalPrice}
";



}
