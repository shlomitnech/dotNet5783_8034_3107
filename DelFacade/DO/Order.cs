using System.Net;

namespace DO;
/// <summary>
/// The customer's information and order information
/// </summary>

public struct Order
{
    public static int orderCounter = 1000;

    public Order()
    {

        CustomerName = "";
		CustomerEmail = "";
        ShippingAddress = "";
        OrderDate = DateTime.MinValue;
        ShippingDate = DateTime.MinValue;
        DeliveryDate = DateTime.MinValue;
    }


    public int ID { get; set; } = orderCounter++;
    public string? CustomerName { get; set; } 
	public string? CustomerEmail { get; set; }
	public string? ShippingAddress { get; set; }
	public DateTime? OrderDate { get; set; }
    public DateTime? ShippingDate { get; set; }
	public DateTime? DeliveryDate { get; set; }

	public override string ToString() => $@"
	Item's ID : {ID},
	Customer's Name is: {CustomerName}
	Customer's email is: {CustomerEmail}
	Customer's address is: {ShippingAddress}
	The order was placed on: {OrderDate}
	The order's shipping date is: {ShippingDate}
	The order's delivery date is: {DeliveryDate}
";



}
