
using DO;
using System.Security.Cryptography.X509Certificates;

namespace Dal;

public class DalProducts
{
  //  DataSource ds = DataSource.s_instance;
    
    public void InsertProduct(Product current)
    {

        //Check if the product already exists
        for (int i = 0; i < DataSource.products.Length; i++)
        {
            if (current.ID == DataSource.products[i].ID)
                throw new Exception("Item already exists\n");
        }
        //If we already have 50 products then throw error
        if (DataSource.products.Length >= 50) //DOES this update the order count?
        {
            throw new Exception("Too many products!\n");
        }
        //If the product doesn't exist and there is space, insert it into products
        {
            DataSource.products[DataSource.Config.NextProductNumber - 1000] = current;
        }
    }
    //Read out all the products
    public void ReadAllProducts()
    {
        for (int i = 0; i < DataSource.products.Length; i++)
        {

                
        }
    }
    public Product ReadProduct(int currentID)
    {
        for (int i = 0; i < DataSource.products.Length; i++)
        {
            if (currentID == DataSource.products[i].ID) //IS THIS CORRECT?
                return DataSource.products[i]; //return the product
        }
        throw new Exception("No product has that ID");

    }

    public Product UpdateProducts(Product prod)
    {

        return prod;
    }
    public void DeleteProduct(int currentID)
    {
        bool deleted = false;
        for (int i = 0; i < DataSource.products.Length; i++ ) //run through the products until the product with this idea is found
        {
            if (DataSource.products[i].ID == currentID) //delete product and update the array
            {
                deleted = true;
                for (int j = i; j< DataSource.products.Length; i++)
                {
                    DataSource.products[j] = DataSource.products[j + 1]; //Update the list
                }
                break;
            }
        }
        if (!deleted) //if didn't find/delete the product
          throw new Exception("The product didn't exist \n")


    }
}
