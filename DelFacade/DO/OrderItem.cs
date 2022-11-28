﻿namespace DO;
/// <summary>
/// Information on the customer's cart
/// </summary>

public class OrderItem
{
	public int ID { get; set; }
	public int productID { get; set; }
	public int orderID { get; set; }
	public double price { get; set; }
	public int amount { get; set; }
	public bool isDelete { get; set; }

	public override string ToString() => $@"
	ID: {ID},
	Product ID: {productID},
	Order ID: {orderID},
	Amount in Stock {amount},
	Product Price: {price},
	";
}