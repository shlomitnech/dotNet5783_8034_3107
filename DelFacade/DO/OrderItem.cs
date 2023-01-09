namespace DO;
/// <summary>
/// Information on the customer's cart
/// </summary>

public struct OrderItem
{
    public static int itemCounter = 0;

    public OrderItem()
    {
        ID = 0;
        productID = 0;
		orderID = 0;
        Price = 0;
        amount = 0;
    }
	public int ID { get; set; } = itemCounter++;
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
