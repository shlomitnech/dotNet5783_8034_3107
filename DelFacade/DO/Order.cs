namespace DO;
/// <summary>
/// The customer's information and order information
/// </summary>

public struct Order
{

	public int ID { get; set; }
	public string customerName { get; set; }
	public string customerEmail { get; set; }
	public string shippingAddress { get; set; }
	public DateTime orderDate { get; set; }
	public DateTime shippingDate { get; set; }
	public DateTime DeliveryDate { get; set; }
	public bool IsDeleted { get; set; }

	public override string ToString() => $@"
	Item's ID : {ID},
	Customer's Name is: {customerName},
	Customer's email is: {customerEmail},
	Customer's address is: {shippingAddress},
	The order was placed on: {orderDate},
	The order's shipping date is: {shippingDate},
	The order's delivery date is: {DeliveryDate}
";



}
