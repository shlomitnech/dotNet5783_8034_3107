using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    public IEnumerable<ProductForList?> GetProductForList()
    {


    }
         //returns the product list (for the manager to see)
    public BO.Product ManagerProduct(int id) //returns a BO product of DO product with id
    {
        BO.Product p = new BO.Product();



        return p;
        
    }
    public void AddProduct(BO.Product prod) //gets a BO product, and adds it to DO product
    {

    }
    public void DeleteProduct(int id) //check in every order that DO product is deleted 
    {

    }
    public void UpdateProduct(BO.Product prod) //Get BO product, and update the DO product
    {

    }

    //The Customer Functions
    public IEnumerable<ProductItem?> GetCatalog()
    {

    }

}
