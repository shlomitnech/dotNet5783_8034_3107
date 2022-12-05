
using DO;
using System.Security.Cryptography.X509Certificates;

namespace Dal;

public class DalProducts
{
  //  DataSource ds = DataSource.s_instance;
    
    public int AddProd(Product prod)
    {
       if (prod.ID ==0) //the product doesn't exist
        {
            prod.ID = DataSource.Config.NextProductNumber; //Take the next available number
            DataSource.products; //[] the 
            return prod.ID;
        }


    }
    public void ReadProd(Product prod) //delete a product
    {


    }
    public Product UpdateProd(Product prod)
    {

        return prod;
    }
    public void DelProduct(Product prod)
    {

    }

    //update
}
