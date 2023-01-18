using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BO;
using DalApi;
using DO;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    /// <summary>
    /// creating an instance of DO
    /// </summary>
    DalApi.IDal? dal = DalApi.Factory.Get();

    /// <summary>
    /// returns all the orders items for a BO list
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.EntityNotFound"></exception>
    /// <exception cref="BO.IncorrectInput"></exception>
    public IEnumerable<OrderForList?> GetAllOrderForList() //calls get of DO order list, gets items for each order, and build BO orderItem list
    {
        IEnumerable<DO.Order?> ords = dal!.Order.GetAll();
        IEnumerable<DO.OrderItem?> ordItems = dal.OrderItem.GetAll();
            return from DO.Order? food in ords
                   select new BO.OrderForList
                   {
                       ID = food?.ID ?? throw new BO.EntityNotFound(),
                       CustomerName = food?.CustomerName?? throw new BO.IncorrectInput("The Name does not exist!"),
                       Status = GetStatus(food),
                       AmountOfItems = ordItems.Select(ordItems => ordItems?.ID == food?.ID).Count(), //go through the orderItems and see the count
                       TotalPrice = (double)ordItems.Sum(ordItems => ordItems?.Price)!
                   };

    }

    /// <summary>
    /// returns the status of the order
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public BO.Enums.OrderStatus GetStatus (DO.Order? order)
    {
        return order?.DeliveryDate != null? BO.Enums.OrderStatus.Recieved : order?.ShippingDate != null ?
            BO.Enums.OrderStatus.Shipped : BO.Enums.OrderStatus.JustPlaced;
    }

    /// <summary>
    /// returns a specific instance of an order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.EntityNotFound"></exception>
    public BO.Order GetBOOrder(int id) //get the Order ID, check it and return the order using the DO order, orderItem and product
    {
        if (id < 0)
        {
            throw new BO.EntityNotFound();
        }
       DO.Order? ord = dal!.Order.Get(id);
       double? tot = 0;//add up the total price
       foreach(DO.OrderItem? prod in dal!.OrderItem.GetAll())
        {
            if (prod?.ID == id) 
            {
                tot += prod?.Price;
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

    /// <summary>
    /// updates the shipping date
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.EntityNotFound"></exception>
    public BO.Order ShipUpdate(int id)
    {
        DO.Order? order1 = dal!.Order.Get(id); 
        BO.Order order2 = GetBOOrder(id); 

        if (order1?.ID == id)
        {
            DO.Order order3 = new()
            {
                ID = id,
                CustomerName = order1?.CustomerName,
                CustomerEmail = order1?.CustomerEmail,
                ShippingAddress = order1?.ShippingAddress,
                OrderDate = order1?.OrderDate,
                ShippingDate = DateTime.Now,
                DeliveryDate = null
            };
            dal.Order.Update(order3);
            order2.ShipDate = DateTime.Now;
            order2.Status = GetStatus(order1);
            return order2;
   
        }
        throw new BO.EntityNotFound();

    }


    /// <summary>
    /// updates the delivery date
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.EntityNotFound"></exception>
    public BO.Order DeliveryUpdate(int id) //get the order number, update the delivery status in DO order, and return BO order that was delivered
    {
        DO.Order? order1 = dal!.Order.Get(id); //get the order ID from the DO folder
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
                DeliveryDate = DateTime.Now
            };
            dal.Order.Update(temporder);
            order2.DeliveryDate = DateTime.Now;
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

    /// <summary>
    /// tracks the order
    /// </summary>
    /// <param name="ord"></param>
    /// <returns></returns>
    /// <exception cref="BO.EntityNotFound"></exception>
    public OrderTracking GetOrderTracking(int ord) //get order number, check it and print the string of dates and status in DO order
    {
        DO.Order order = new();
        try
        {
            order = (DO.Order)dal?.Order.Get(ord)!;//get the requested order from dal
        }
        catch
        {
            throw new BO.EntityNotFound("The order requested does not exist\n");//order does not exist
        }
        return new OrderTracking()
        {
            ID = ord,
            Status = GetStatus(order),
            Tracking = new List<Tuple<DateTime?, string>> { new Tuple<DateTime?, string>(order.OrderDate, "Approved"), new Tuple<DateTime?, string>(order.ShippingDate, "sent"),
            new Tuple<DateTime?, string>(order.DeliveryDate, "Approved")}
        };



    }

}
