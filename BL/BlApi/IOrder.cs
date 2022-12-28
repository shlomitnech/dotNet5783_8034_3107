using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;


public interface IOrder
{
    //The manager functions
    public Order GetBOOrder(int id); //get the Order ID, check it and return the order using the DO order, orderItem and product
    public Order ShipUpdate(int id, DateTime date); //gets an order number, check if it exists and update the date in Do order, return the BO order that was shipped
    public Order DeliveryUpdate(int id, DateTime date); //get the order number, update the delivery status in DO order, and return BO order that was delivered
    public OrderTracking GetOrderTracking(int ord); //get order number, check it and print the string of dates and status in DO orders
    public IEnumerable<OrderForList?> GetAllOrderForList();//calls get of DO order list, gets items for each order, and build BO orderItem list
    public Enums.OrderStatus GetStatus(DO.Order order);


}
