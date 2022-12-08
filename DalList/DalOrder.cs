﻿using DO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading;
using DalApi;
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
        //delete the order from the array and update the rest of the array

        int index = DataSource.orders.FindIndex(x => x.ID == currentID); // Is this correct?

        if (index == -1) // Item doesn't exist
            throw new EntityNotFound("Order does not exist");

        DataSource.orders.RemoveAt(index);    

        return;
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
    public IEnumerable<Order> GetAll() //!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
    {
        /*
        //check that list is not empty
        if (DataSource.orders == null) throw new Exception("There are no orders!");
        //make a new array with only the orders we have
        Order[] tempOrders = new Order[DataSource.countOrders];

        for (int i = 0; i < tempOrders.Length; i++)
        {
            tempOrders[i] = DataSource.orders[i];
        }

            return tempOrders;
        */
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


}
