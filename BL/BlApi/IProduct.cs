using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;


public interface IProduct
{
    //The manager functions
    public IEnumerable<ProductForList?> GetProductForList(); //returns the product list (for the manager to see)
    public Product ManagerProduct(int id); //returns a BO product of DO product with id
    public int AddProduct(Product prod); //gets a BO product, and adds it to DO product
    public void DeleteProduct(int id); //check in every order that DO product is deleted 
    public void UpdateProduct(Product prod); //Get BO product, and update the DO product

    public int NewID();

 //   public Product FilterProductList(object value);

    //The Customer Functions
    public IEnumerable<ProductItem?> GetCatalog(); //Get the product list of DO and return the ProductItem list of BO
}
