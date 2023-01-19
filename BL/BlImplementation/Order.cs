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
        List<BO.OrderForList?>? list = new List<BO.OrderForList?>(); // create a list of BO orders

        foreach (DO.Order order in ords)
        {
            int quantity = 0;
            double? total = 0;

            foreach (DO.OrderItem item in ordItems) // for each order item in DO
            {
                if (item.orderID == order.ID) // if the OrderID of that order item matches with an order ID (meaning it's part of that order)
                {
                    quantity++; // increase the amount of items inside of an order
                    total += item.Price; // increase the total by the price of that item
                }
            }
            list.Add(new BO.OrderForList // add a new BO order to the list created above
            {
                ID = order.ID,
                CustomerName = order.CustomerName ?? throw new BO.IncorrectInput("The Name does not exist!"),
                Status = GetStatus(order),
                AmountOfItems = quantity,
                TotalPrice = (double)total // set the total price equal to the variable total
            });
        }
        return list;
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
    public int AddOrder(BO.Order ord)
    {
        DO.Order o = new DO.Order
        {
            CustomerName = ord.CustomerName,
            CustomerEmail = ord.CustomerEmail,
            ShippingAddress = ord.CustomerAddress,
        };
        try
        {
            return dal!.Order.Add(o); //the the product to the list and return the ID
        }
        catch (DalApi.Duplicates)
        {
            throw new BO.IdExistException("Product ID already exists");
        }

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
