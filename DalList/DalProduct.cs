
using DO;
using System.Security.Cryptography.X509Certificates;

namespace Dal;

public class DalProducts
{
    
    public void InsertProduct(Product current)
    {

        //Check if the product already exists
        for (int i = 0; i < (DataSource.Config.s_nextProductNumber - 100000); i++)
        {
            if (current.ID == DataSource.products[i].ID)
                throw new Exception("Item already exists\n");
        }
        //If we already have 50 products then throw error
        if ((DataSource.Config.s_nextProductNumber - 100000) >= 50) //DOES this update the order count?
        {
            throw new Exception("Too many products!\n");
        }
        //If the product doesn't exist and there is space, insert it into products
        {
            DataSource.products[DataSource.Config.NextProductNumber - 100000] = current;
        }
    }
    //Read out all the products
    public void ReadAllProducts()
    {
        for (int i = 0; i < (DataSource.Config.s_nextProductNumber - 100000); i++)
        {
            Console.Write(DataSource.products[i].ToString());
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

    public void UpdateProducts(Product prod)
    {
        for (int i = 0; i < (DataSource.Config.s_nextProductNumber); i++)
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
        for (int i = 0; i < (DataSource.Config.s_nextProductNumber - 100000 - 100000); i++ ) //run through the products until the product with this idea is found
        {
            if (DataSource.products[i].ID == currentID) //delete product and update the array
            {
                deleted = true;
                for (int j = i; j< (DataSource.Config.s_nextProductNumber - 100000); j++)
                {
                    DataSource.products[j] = DataSource.products[j + 1]; //Update the list
                }
                break;
            }
        }
        if (!deleted) //if didn't find/delete the product
            throw new Exception("The product didn't exist \n");


    }
}
