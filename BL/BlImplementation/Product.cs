using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using Dal;
using System.Collections;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    readonly static IDal? Dos = new DalList();
    public IEnumerable<ProductForList?> GetProductForList() //returns the product list (for the manager to see)
    {
        return from DO.Product? food in Dos.Product.GetAll()
               where food != null
               select new ProductForList
               {
                   ID = food.Value.ID,
                   Name = food?.Name,
                   Price = (double?)food?.Price,
                   Category = (Enums.Category?)food?.Category
               };
        throw new BO.Exceptions("List is empty");
    }
         //returns the product list (for the manager to see)
    public BO.Product ManagerProduct(int id) //returns a BO product of DO product with id
    {
        BO.Product prod1 = new BO.Product();
        DO.Product prod2 = new DO.Product();
        prod2 = Dos.Product.Get(id); // put the product with this ID into prod2
        if (true) 
        {
            prod1.ID = id;
            prod1.Name = prod2.Name;
            prod1.Price = prod2.Price;
            prod1.Category = (Enums.Category?)prod2.Category;
            prod1.InStock = prod2.inStock;
            return prod1;
        }
        throw new BO.EntityNotFound("The product doesn't exist");
    }
    public int AddProduct(BO.Product prod) //gets a BO product, and adds it to DO product
    {
        if (prod.Name == "" || prod.Price <= 0 || prod.InStock < 0 || prod.Category < 0 || prod.Category > Enums.Category.Other )
        {
            throw new BO.IncorrectInput("Invalid Input");
        }
        DO.Product p = new DO.Product
        {
            ID = 0,
            Name = prod.Name,
            Price = prod.Price,
            inStock = prod.InStock,
            Category = (DO.Enums.Category?)prod.Category
        };

        p.ID = Dos.Product.Add(p);
        return p.ID;

    }
    public void DeleteProduct(int id) //check in every order that DO product is deleted 
    {
        Dos.Product.Delete(id);//delete product
    }
    public void UpdateProduct(BO.Product prod) //Get BO product, and update the DO product
    {
        if (prod.Name == "" || prod.Price <= 0 || prod.InStock < 0 || prod.Category < 0 || prod.Category > Enums.Category.Other)
        {
            throw new IncorrectInput("Invalid input");
        }
        DO.Product p = new DO.Product
        {
            ID = prod.ID,
            Name = prod.Name,
            Price = prod.Price,
            inStock = prod.InStock,
            Category = (DO.Enums.Category?)prod.Category
        };

        Dos?.Product.Update(p);

    }

    //The Customer Functions
    public IEnumerable<ProductItem?> GetCatalog()
    {
        var prodList = from latkes in Dos?.Product.GetAll() //send the list of products to prodList
                     select new ProductItem() //make a new product item for each product
                     {
                         ID = latkes.ID,
                         Name = latkes.Name,
                         Price = (double?)latkes.Price,
                         Amount = latkes.inStock,
                         Category = (BO.Enums.Category?)latkes.Category,
                         InStock = (latkes.inStock> 0)
                     };
        return prodList;
    }

}
