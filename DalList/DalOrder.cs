using DO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading;
using DalApi;
using System;

namespace Dal;

public class DalOrder : IOrder //change to be internal?
{
    /// <summary>
    /// Adds an instance to the main array
    /// </summary>
    /// <param name="current"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(Order current)
    {
        //If we already have 100 orders then it will send an error
        if (DataSource.orders.Count >= 100)
        {
            throw new EntityNotFound("Can't take more orders");
        }
        //take the instance and add it to the array
        int newID = DataSource.Config.NextOrderNumber;
        current.ID = newID;
        DataSource.orders.Add(current);
        return newID;
    }

    /// <summary>
    /// deletes an instace from the main array
    /// </summary>
    /// <param name="currentID"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int currentID)
    {
        int index = -1;
        foreach (DO.Order ord in DataSource.orders)
        {
            if (ord.ID == currentID)
            {
                index = DataSource.orders.IndexOf(ord); // save the index of the product with the matching ID#
                break;
            }
        }
        DO.Order DelOrder = DataSource.orders[index]; // save the product in the found index
        DataSource.orders.Remove(DelOrder); // remove the product
    }

    /// <summary>
    /// returns the instance with the provided identifier
    /// </summary>
    /// <param name="currentID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order Get(int currentID)
    {
        Order thisOrd = DataSource.orders.Find(x => x.ID == currentID);
        if (thisOrd.ID != currentID)
            throw new Exception("No product has that ID");    //if product is not found
        return thisOrd;
    }
    /// <summary>
    /// Sends back the instances in the main array so it can be printed 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter)
    {

        if (filter == null)//select whole list
        {
            return (IEnumerable<Order?>)DataSource.orders;

        }
        


        throw new Exception("There are no orders!");
    }

    /// <summary>
    /// changes attributes of the instance
    /// </summary>
    /// <param name="current"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order current)
    {
        int index = DataSource.orders.FindIndex(x => x.ID == current.ID);
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
        if (filter == null) throw new ArgumentNullException(nameof(filter));
        foreach (Order ord in DataSource.orders)
        {
            if (filter!(ord))
            {
                return ord;
            }
        }
        throw new Exception("Does not exist\n");

    }

}