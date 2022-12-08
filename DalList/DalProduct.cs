﻿using DO;
using System.Security.Cryptography;
using System.Threading;
using DalApi;
namespace Dal;

public class DalProducts : IProduct
{
    /// <summary>
    /// adding an instance to the main array
    /// </summary>
    /// <param name="current"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(Product current) //this is out of order
    {

        //If we already have 50 products then it will send an error
        if (DataSource.products.Count >= 50)
        {
            throw new Exception("Can't take more orders");
        }
        /*
        int index = DataSource.products.FindIndex(x => x.ID == current.ID);
        //Check if the product already exists
        if (index != -1)
            throw new Exception("Product already exists\n");
        */
        //take the instance and add it to the list
        int newID = DataSource.Config.NextProductNumber;
        current.ID = newID;
        DataSource.products.Add(current);
        return newID;

 
 

  
    }

    /// <summary>
    /// returns all instances in the main array to be printed
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product[] ReadAllProducts()
    {
        //check that the array is not empty
        if (DataSource.products == null) throw new Exception("There are no products");

        //send back an array with the products
        Product[] tempProducts = new Product[DataSource.countProducts];

        for (int i = 0; i < tempProducts.Length; i++)
        {
            tempProducts[i] = DataSource.products[i];
        }

        return tempProducts; //returning a complete array
    }

    /// <summary>
    /// returns the instance based on the identifier provided by the user so it can be printed
    /// </summary>
    /// <param name="currentID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product Get(int currentID)
    {
        Product thisProd = DataSource.products.Find(x=> x.ID == currentID);
        if (thisProd.ID != currentID)
           throw new Exception("No product has that ID");    //if product is not found
        return thisProd;
    }

    /// <summary>
    /// changes attributes of the instance
    /// </summary>
    /// <param name="prod"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product prod)
    {
        //iterate through the array to find the procuct we want to update
        for (int i = 0; i < (DataSource.countProducts); i++)
        {
            if (prod.ID == DataSource.products[i].ID)
            {
                DataSource.products[i] = prod;
                return;
            }
        }
        throw new Exception("Product doesn't exist! \n");

    }

    /// <summary>
    /// removes the instance from the main array
    /// </summary>
    /// <param name="currentID"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int currentID)
    {
        //delete the product from the array and update the rest of the array

        int index = DataSource.products.FindIndex(x => x.ID == currentID); // Is this correct?

        if (index == -1) // Item doesn't exist
            throw new Exception("Order does not exist");

        DataSource.products.RemoveAt(index);

        return;
    }
}