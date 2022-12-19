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
    readonly private static IDal DOList = DalApi.Kitchen.Get();
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
        DO.Product p = new DO.Product();
        p.ID = 0;
        p.Name = prod.Name;
        p.Price = prod.Price;
        p.inStock = prod.inStock;
        p.Category = (DO.Enums.Category)prod.Category;

        p.ID = DOList.Product.Add(p);

    }
    public void DeleteProduct(int id) //check in every order that DO product is deleted 
    {

    }
    public void UpdateProduct(BO.Product prod) //Get BO product, and update the DO product
    {
        DO.Product tempProduct = new DO.Product();
        tempProduct.ID = prod.ID;
        tempProduct.Name = prod.Name;
        tempProduct.Price = prod.Price;
        tempProduct.inStock = prod.inStock;
        tempProduct.Category = (DO.Enums.Category)prod.Category;

        DOList.Products.Update(tempProduct);

    }

    //The Customer Functions
    public IEnumerable<ProductItem?> GetCatalog()
    {

    }

}
