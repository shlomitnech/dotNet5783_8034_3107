using DO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading;
using DalApi;
using System;

namespace Dal;

public class DalOrder : IOrder //change to be internal?
{
    readonly static Random random = new Random(); // readonly static field for generating random numbers

    /// <summary>
    /// Adds an instance to the main array
    /// </summary>
    /// <param name="current"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(Order current)
    {
        current.OrderDate = DateTime.Now - new TimeSpan(random.NextInt64(10L * 1000L * 3600L * 24L * 100L));
        current.ShippingDate = current.OrderDate + new TimeSpan(random.NextInt64(10L * 1000L * 3600L * 24L * 100L)); // add a random time interval to the order date to get the shipping date
        current.DeliveryDate = current.ShippingDate + new TimeSpan(random.NextInt64(10L * 1000L * 3600L * 24L * 100L)); // add a random time interval to the shipping date to get the delivery date
        if (current.ID == 0)
        {
            Order order = new Order();
            order.CustomerName = current.CustomerName;
            order.CustomerEmail = current.CustomerEmail;
            order.ShippingAddress = current.ShippingAddress;
            order.OrderDate = current.OrderDate;
            order.ShippingDate = current.ShippingDate;
            order.DeliveryDate = current.DeliveryDate;
            DataSource.orders.Add(order);
            return order.ID;         
        }
        //If we already have 100 orders then it will send an error
        if (DataSource.orders.Count >= 100)
        {
            throw new EntityNotFound("Can't take more orders");
        }
        int index = DataSource.orders.FindIndex(x => x?.ID == current.ID); // find the order with a matching ID as the inputted order

        //take the instance and add it to the array
        DataSource.orders.Add(current);
        return current.ID;
    }

    /// <summary>
    /// deletes an instace from the main array
    /// </summary>
    /// <param name="currentID"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int currentID)
    {
        int index = -1;
        foreach (DO.Order? ord in DataSource.orders)
        {
            if (ord?.ID == currentID)
            {
                index = DataSource.orders.IndexOf(ord); // save the index of the product with the matching ID#
                break;
            }
        }
        DO.Order DelOrder = (Order)DataSource.orders[index]!; // save the product in the found index
        DataSource.orders.Remove(DelOrder); // remove the product
    }

    /// <summary>
    /// returns the instance with the provided identifier
    /// </summary>
    /// <param name="currentID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order? Get(int currentID)
    {
        DO.Order? ord = DataSource.orders.Find(x => x?.ID == currentID); // find an order with a matching ID
        if (ord == null)
        {
            throw new EntityNotFound();
        }
        return ord;
    }
    /// <summary>
    /// Sends back the instances in the main array so it can be printed 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
    {
        return (IEnumerable<Order?>)DataSource.orders.ToList();

    }

    /// <summary>
    /// changes attributes of the instance
    /// </summary>
    /// <param name="current"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order? current)
    {
        int index = DataSource.orders.FindIndex(x => x?.ID == current?.ID);
        if (index == -1) //item doesn't exist
            throw new EntityNotFound("Order does not exist!");
        DataSource.orders[index] = current;
    }
    /// <summary>
    /// Get's an entity using a filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    Order ICrud<Order>.GetByFilter(Func<Order?, bool>? filter)
    {
        
        throw new Exception("Does not exist\n");

    }

}