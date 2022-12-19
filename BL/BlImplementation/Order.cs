using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using BlApi;
using BO;
using DO;
using DalApi;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{  
    //The manager functions
    public IEnumerable<OrderForList?> GetAllOrderForLists() //returns the order list (for the manager to see)
    {

    }
    public BO.Order GetBOOrder(int id) //get the Order ID, check it and return the order using the DO order, orderItem and product
    {

    }
    public BO.Order ShipUpdate(int id) //gets an order number, check if it exists and update the date in Do order, return the BO order that was shipped
    {

    }
    public BO.Order DeliveryUpdate(int id) //get the order number, update the delivery status in DO order, and return BO order that was delivered
    {

    }
    public BO.OrderTracking GetOrderTracking(int ord) //get order number, check it and print the string of dates and status in DO order
    {

    }

}
