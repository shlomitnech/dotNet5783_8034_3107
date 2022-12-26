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
        BO.Product prod1 = new BO.Product();
        DO.Product prod2 = new DO.Product();

        prod2 = DOList.Product.Get(id);

        throw new Exception(NotDeleted);

        prod1.ID = id;
        prod1.Name = prod2.Name;
        prod1.Price = prod2.Price;
        prod1.Category = (BO.Enums.Category)prod2.Category;
        prod1.inStock = prod2.inStock;

        return prod1;
        
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

        DOList.Product.Update(tempProduct);

    }

    //The Customer Functions
    public IEnumerable<ProductItem?> GetCatalog()
    {

    }

}
