﻿
using DO;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Dal;

public class DalProducts
{
    
    public int InsertProduct(Product current)
    {

        //Check if the product already exists
        for (int i = 0; i < (DataSource.countProducts); i++)
        {
            if (current.Name == DataSource.products[i].Name)
                throw new Exception("Product already exists\n");
        }
        //If we already have 50 products then throw error
        if ((DataSource.countProducts) >= 50) 
        {
            throw new Exception("Too many products!\n");
        }
        //If the product doesn't exist and there is space, insert it into products
        {
            int newID = DataSource.Config.NextProductNumber;
            current.ID = newID;
            DataSource.products[DataSource.countProducts++] = current; //put the product into the array
            return newID;
        }
    }
    //Read out all the products
    public Product[] ReadAllProducts()
    {
        //check that the array is not empty
        if (DataSource.products == null) throw new Exception("There are no products");

        //send back an array with the products
        Product[] tempProducts = new Product[DataSource.countProducts];

        for(int i = 0; i < tempProducts.Length; i++)
        {
            tempProducts[i] = DataSource.products[i];
        }

        return tempProducts; //returning a complete array
    }
    public Product ReadProduct(int currentID)
    {
        //iterate through the array and return the instance of the product based on the identifier that the user inputed
        for (int i = 0; i < (DataSource.countProducts); i++)
        {
            if (currentID == DataSource.products[i].ID) 
                return DataSource.products[i]; //return the product once found
        }
        throw new Exception("No product has that ID");    //if product is not found

    }

    public void UpdateProducts(Product prod)
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
    public void DeleteProduct(int currentID)
    {
        bool deleted = false;
        for (int i = 0; i < (DataSource.countProducts); i++ ) //run through the products until the product with this idea is found
        {
            if (DataSource.products[i].ID == currentID) //delete product and update the array
            {
                deleted = true;
                for (int j = i; j< (DataSource.countProducts); j++)
                {
                    DataSource.products[j] = DataSource.products[j + 1]; //Update the list
                }
                DataSource.countProducts--;
                break;
            }
        }
        if (!deleted) //if didn't find/delete the product
            throw new Exception("The product didn't exist \n");
    }
}
