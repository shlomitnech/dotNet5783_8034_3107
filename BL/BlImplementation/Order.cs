using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using Dal;
using DO;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    static IDal? Dos = new DalList();
    public IEnumerable<OrderForList?> GetAllOrderForList() //calls get of DO order list, gets items for each order, and build BO orderItem list
    {
        IEnumerable<DO.Order?> ords = Dos!.Order.GetAll();
            IEnumerable<DO.OrderItem?> ordItems = Dos.OrderItem.GetAll();
            return from DO.Order? food in ords
                   select new BO.OrderForList
                   {
                       ID = food?.ID ?? throw new BO.EntityNotFound(),
                       CustomerName = food?.CustomerName,
                       Status = GetStatus(food.Value),
                       AmountOfItems = ordItems.Select(ordItems => ordItems?.ID == food?.ID).Count(), //go through the orderItems and see the count
                       TotalPrice = (double)ordItems.Sum(ordItems => ordItems?.Price)!
                   };

    }
    public BO.Enums.OrderStatus GetStatus (DO.Order? order)
    {
        return order?.DeliveryDate != null? BO.Enums.OrderStatus.Recieved : order?.ShippingDate != null ?
            BO.Enums.OrderStatus.Shipped : BO.Enums.OrderStatus.JustPlaced;
    }
    public BO.Order GetBOOrder(int id) //get the Order ID, check it and return the order using the DO order, orderItem and product
    {
        if (id < 0)
        {
            throw new BO.EntityNotFound();
        }
       DO.Order? ord = Dos!.Order.Get(id);
       double? tot = 0;//add up the total price
       foreach(DO.OrderItem? apple in Dos!.OrderItem.GetAll())
        {
            if (apple?.ID == id) 
            {
                tot += apple?.Price;
            }
        }
        
       if(ord?.ID == id) //if the id exists
       {
            return new BO.Order
            {
                ID = id,
                CustomerAddress = ord?.ShippingAddress,
                CustomerEmail = ord?.CustomerEmail,
                CustomerName = ord?.CustomerName,
                OrderDate = ord?.OrderDate,
                ShipDate = ord?.ShippingDate,
                DeliveryDate = ord?.DeliveryDate,
                Status = GetStatus(ord),
                TotalPrice = tot,
            };
       }
       throw new BO.EntityNotFound();

    }
    public BO.Order ShipUpdate(int id, DateTime date) //gets an order number, check if it exists and update the date in Do order, return the BO order that was shipped
    {
        DO.Order? order1 = Dos!.Order.Get(id); //get the order ID from the DO folder
        BO.Order order2 = GetBOOrder(id); //get the order from BO

        if (order1?.ID == id)
        {
            DO.Order order3 = new()
            {
                ID = id,
                CustomerName = order1?.CustomerName,
                CustomerEmail = order1?.CustomerEmail,
                ShippingAddress = order1?.ShippingAddress,
                OrderDate = order1?.OrderDate,
                ShippingDate = date,
                DeliveryDate = null
            };
            Dos.Order.Update(order3);
            order2.ShipDate = date;
            order2.Status = GetStatus(order1);
            return order2;
   
        }
        throw new BO.EntityNotFound();

    }

    public BO.Order DeliveryUpdate(int id, DateTime date) //get the order number, update the delivery status in DO order, and return BO order that was delivered
    {
        DO.Order? order1 = Dos!.Order.Get(id); //get the order ID from the DO folder
        BO.Order order2 = GetBOOrder(id); //get the order from BO

        if (order1?.ID == id)
        {
            DO.Order temporder = new()
            {
                ID = id,
                CustomerName = order1?.CustomerName,
                CustomerEmail = order1?.CustomerEmail,
                ShippingAddress = order1?.ShippingAddress,
                OrderDate = order1?.OrderDate,
                ShippingDate = order1?.ShippingDate,
                DeliveryDate = date
            };
            Dos.Order.Update(temporder);
            order2.DeliveryDate = date;
            order2.Status = GetStatus(order1);
            return order2;
            /*
            double tot = 0;//add up the total price
            foreach (DO.OrderItem apple in Dos.OrderItem.GetAll())
            {
                if (apple.ID == id)
                {
                    tot += apple.Price;
                }
            }
            return new BO.Order
            {
                ID = id,
                CustomerName = order1.CustomerName,
                CustomerEmail = order1.CustomerEmail,
                CustomerAddress = order1.ShippingAddress,
                ShipDate = order1.OrderDate,
                OrderDate = order1.OrderDate,
                DeliveryDate = order1.DeliveryDate,
                Status = GetStatus(order1),
                TotalPrice = tot,
            };
            */

        }
        throw new BO.EntityNotFound();
    }
    public OrderTracking GetOrderTracking(int ord) //get order number, check it and print the string of dates and status in DO order
    {
        OrderTracking ordtrack = new();
        ordtrack.Tracking = new();
        foreach (DO.Order? list in Dos!.Order.GetAll().Select(v => (DO.Order?)v)) //iterate through all the orders in DO
        {
            if (list?.ID == ord) // if the item has the same id 
            {
                ordtrack.ID = ord; //save the ID 
                if (list?.DeliveryDate != null)
                {
                    ordtrack.Status = BO.Enums.OrderStatus.Shipped;
                  //  ordtrack.Tracking.Add(Tuple.Create)
                }
                if (list?.OrderDate!= null)
                {

                }
                if (list?.ShippingDate!= null)
                {

                }
                return ordtrack;
            }
        }
        throw new BO.EntityNotFound("Order doesn't exist\n");

    }

}
