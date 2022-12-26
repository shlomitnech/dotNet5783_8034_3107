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
using DO;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    readonly private static IDal DOs = DalApi.Kitchen.Get();
    public IEnumerable<OrderForList?> GetAllOrderForList() //calls get of DO order list, gets items for each order, and build BO orderItem list
    {
            IEnumerable<DO.Order> ords = DOs.Order.GetAll();
            IEnumerable<DO.OrderItem> ordItems = DOs.OrderItem.GetAll();
            return from DO.Order? food in ords
                   select new BO.OrderForList
                   {
                       ID = food?.ID ?? throw new BO.EntityNotFound(),
                       CustomerName = food?.CustomerName,
                       Status = GetStatus(food.Value),
                       AmountOfItems = ordItems.Select(ordItems => ordItems.ID == food?.ID).Count(), //go through the orderItems and see the count
                       TotalPrice = (double)ordItems.Sum(ordItems => ordItems.price)

                   };

    }
    private BO.Enums.OrderStatus GetStatus (DO.Order order)
    {
        return order.DeliveryDate != null? BO.Enums.OrderStatus.Recieved : order.ShippingDate != null ?
            BO.Enums.OrderStatus.Shipped : BO.Enums.OrderStatus.JustPlaced;
    }
    public BO.Order GetBOOrder(int id) //get the Order ID, check it and return the order using the DO order, orderItem and product
    {
        if (id < 0)
        {
            throw new BO.EntityNotFound();
        }
       DO.Order ord = DOs.Order.Get(id);
       double tot = 0;//add up the total price
       foreach(DO.OrderItem apple in DOs.OrderItem.GetAll())
        {
            if (apple.ID == id) 
            {
                tot += apple.price;
            }
        }
        
       if(ord.ID == id) //if the id exists
       {
            return new BO.Order
            {
                ID = id,
                CustomerAddress = ord.ShippingAddress,
                CustomerEmail = ord.CustomerEmail,
                CustomerName = ord.CustomerName,
                OrderDate = ord.OrderDate,
                ShipDate = ord.ShippingDate,
                DeliveryDate = ord.DeliveryDate,
                Status = GetStatus(ord),
                TotalPrice = tot,
            };
       }
       throw new BO.EntityNotFound();

    }
    public BO.Order ShipUpdate(int id) //gets an order number, check if it exists and update the date in Do order, return the BO order that was shipped
    {
        DO.Order order1 = DOs.Order.Get(id); //get the order ID from the DO folder
        if (order1.ID == id)
        {
            DO.Order order2 = new()
            {
                ID = id,
                CustomerName = order1.CustomerName,
                CustomerEmail = order1.CustomerEmail,
                ShippingAddress = order1.ShippingAddress,
                OrderDate = order1.OrderDate,
                ShippingDate = DateTime.Now,
                DeliveryDate = null
            };
            DOs.Order.Update(order2);
            double tot = 0;//add up the total price
            foreach (DO.OrderItem apple in DOs.OrderItem.GetAll())
            {
                if (apple.ID == id)
                {
                    tot += apple.price;
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
                Status = GetStatus(order2),
                TotalPrice = tot,
            };

        }
        throw new BO.EntityNotFound();
    }

    public BO.Order DeliveryUpdate(int id) //get the order number, update the delivery status in DO order, and return BO order that was delivered
    {
        DO.Order order1 = DOs.Order.Get(id); //get the order ID from the DO folder
        if (order1.ID == id)
        {
            DO.Order order2 = new()
            {
                ID = id,
                CustomerName = order1.CustomerName,
                CustomerEmail = order1.CustomerEmail,
                ShippingAddress = order1.ShippingAddress,
                OrderDate = order1.OrderDate,
                ShippingDate = order1.ShippingDate,
                DeliveryDate = DateTime.Now
            };
            DOs.Order.Update(order2);
            double tot = 0;//add up the total price
            foreach (DO.OrderItem apple in DOs.OrderItem.GetAll())
            {
                if (apple.ID == id)
                {
                    tot += apple.price;
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

        }
        throw new BO.EntityNotFound();
    }
    public OrderTracking GetOrderTracking(int ord) //get order number, check it and print the string of dates and status in DO order
    {
        OrderTracking ordtrack = new();
        ordtrack.Tracking = new();
        foreach (DO.Order? list in DOs.Order.GetAll()) //iterate through all the orders in DO
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
