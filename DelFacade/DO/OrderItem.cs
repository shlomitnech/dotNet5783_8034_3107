namespace DO;
/// <summary>
/// Information on the customer's cart
/// </summary>

public struct OrderItem
{
	public int ID { get; set; }
	public int productID { get; set; }
	public int orderID { get; set; }
	public double? Price { get; set; }
	public int amount { get; set; }
	//public bool isDeleted { get; set; }

	public override string ToString() => $@"
	ID: {ID}
	Product ID: {productID}
	Order ID: {orderID}
	Amount in Cart: {amount}
	Product Price: {Price}
	";
}
