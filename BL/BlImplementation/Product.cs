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
    readonly private static IDal Dos = DalApi.Kitchen.Get(); 
    public IEnumerable<ProductForList?> GetProductForList() //returns the product list (for the manager to see)
    {
        return from DO.Product? food in Dos.Product.GetAll()
               where food != null
               select new ProductForList
               {
                   ID = food.Value.ID,
                   Name = food?.Name,
                   Price = (double)food?.Price,
                   Category = (Enums.Category)food?.Category
               };

    }
         //returns the product list (for the manager to see)
    public BO.Product ManagerProduct(int id) //returns a BO product of DO product with id
    {
        BO.Product prod1 = new BO.Product();
        DO.Product prod2 = new DO.Product();
        prod2 = Dos.Product.Get(id); // put the product with this ID into prod2
        if (true) //if it isn't deleted!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            prod1.ID = id;
            prod1.Name = prod2.Name;
            prod1.Price = prod2.Price;
            prod1.Category = (Enums.Category?)prod2.Category;
            prod1.InStock = prod2.inStock;
        }
        throw new Exception();
    }
    public void AddProduct(BO.Product prod) //gets a BO product, and adds it to DO product
    {
        if (prod.Name == "" || prod.Price <= 0 || prod.InStock < 0 || prod.Category < 0 || prod.Category > Enums.Category.Other )
        {
            throw new IncorrectInput("Invalid Input");
        }
        DO.Product p = new DO.Product();
        p.ID = 0;
        p.Name = prod.Name;
        p.Price = prod.Price;
        p.inStock = prod.InStock;
        p.Category = (DO.Enums.Category?)prod.Category;

        p.ID = Dos.Product.Add(p);

    }
    public void DeleteProduct(int id) //check in every order that DO product is deleted 
    {

    }
    public void UpdateProduct(BO.Product prod) //Get BO product, and update the DO product
    {
        if (prod.Name == "" || prod.Price <= 0 || prod.InStock < 0 || prod.Category < 0 || prod.Category > Enums.Category.Other)
        {
            throw new IncorrectInput("Invalid input");
        }
        DO.Product p = new DO.Product();
        p.ID = prod.ID;
        p.Name = prod.Name;
        p.Price = prod.Price;
        p.inStock = prod.InStock;
        p.Category = (DO.Enums.Category?)prod.Category;

        Dos.Product.Update(p);

    }

    //The Customer Functions
    public IEnumerable<ProductItem?> GetCatalog()
    {
        var potato = from latkes in Dos.Product.GetAll()
                     where latkes != null
                     select new ProductItem()
                     {
                         ID = latkes.ID,
                         Name = latkes.Name,
                         Price = (double)latkes.Price,
                         Amount = latkes.inStock,
                         Category = (BO.Enums.Category)latkes.Category
                     };

        foreach (ProductItem item in potato)
        {
            if (item.Amount > 0)
                item.InStock = true;
            item.InStock = false;
        }

        return potato;
    }

}
