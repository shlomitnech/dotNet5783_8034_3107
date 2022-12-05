
using DO;
using System.Security.Cryptography.X509Certificates;

namespace Dal;

public class DalProducts
{
  //  DataSource ds = DataSource.s_instance;
    
    public int AddProducts(Product prod)
    {

        if (DataSource.products.Length >= 50)
        {
            throw new Exception("Too many products!");
        }

        if (prod.ID ==0) //the product doesn't exist
        {
            prod.ID = DataSource.Config.NextProductNumber; //Take the next available number
            DataSource.products; //[] the 
            return prod.ID;
        }

    }
    public void ReadProducts(Product prod) //delete a product
    {


    }
    public void ReadListOfProducts(Product prod) //delete a product
    {


    }
    public Product UpdateProducts(Product prod)
    {

        return prod;
    }
    public void DelProduct(Product prod)
    {

    }

    //update
}
