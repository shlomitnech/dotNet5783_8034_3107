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
    readonly private static IDal DOs = DalApi.Kitchen.Get();
    public IEnumerable<OrderForList?> GetAllOrderForLists() //returns the order list (for the manager to see)
    {
        IEnumerable<DO.Order?> ords = DOs.Order.GetAll(); 
        IEnumerable<DO.OrderItem?> ordItems = DOs.OrderItem.GetAll();
        return from DO.Order ord in ords
               select new BO.OrderForList
               {
                   ID = ord?.ID ?? throw new BO.EntityNotFound()


               };
        
        


    }
    public BO.Order GetBOOrder(int id) //get the Order ID, check it and return the order using the DO order, orderItem and product
    {
        if (id < 0)
        {
            throw new BO.EntityNotFound();
        }
       // DO.Order ord = DOs.Order.Get(id);

    }
    public BO.Order ShipUpdate(int id) //gets an order number, check if it exists and update the date in Do order, return the BO order that was shipped
    {
        DO.Order order1 = DOs.Order.Get(id);
        if (order1.ID == id)
        {
            DO.Order order2 = new();
            {

            }
        }
    }
    public BO.Order DeliveryUpdate(int id) //get the order number, update the delivery status in DO order, and return BO order that was delivered
    {
        DO.Order order1 = DOs.Order.Get(id); //get the order ID from the DO folder
                  
    }
    public BO.OrderTracking GetOrderTracking(int ord) //get order number, check it and print the string of dates and status in DO order
    {
        OrderTracking ordtrack = new();
        
        foreach (DO.Order? list in DOs.Order.GetAll()) //iterate through all the orders in DO
        {
            if (list?.ID == ord) // if the item has the same id 
            {


                return ordtrack;
            }
        }
        throw new BO.EntityNotFound("Order doesn't exist\n");

    }

}
