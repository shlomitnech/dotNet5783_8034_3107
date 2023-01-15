using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Collections;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    readonly private static DalApi.IDal? dal = DalApi.Factory.Get() ?? throw new BO.Exceptions("Factory does not exist\n");
    public IEnumerable<BO.ProductForList?> GetProductForList() //returns the product list (for the manager to see)
    {
        var v = from prods in dal?.Product.GetAll()
                where prods != null
                select new ProductForList()
                {
                    ID = prods?.ID ?? throw new BO.EntityNotFound("The product id does not exist"),
                    Name = prods?.Name,
                    Price = (double)prods?.Price,
                    Category = (BO.Enums.Category)prods?.Category,
                };
        return v;
        throw new BO.Exceptions("List is empty");
    }
    //returns the product list (for the manager to see)
    public BO.Product ManagerProduct(int id) //returns a BO product of DO product with id
    {
        BO.Product prod1 = new();
        DO.Product prod2 = new();
        try
        {
            prod2 = (DO.Product)(dal?.Product.Get(id));
        }
        catch (DalApi.EntityNotFound)
        {
            throw new BO.EntityNotFound("The ID does not exist!");
        }
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
        if (prod.Name == "" || prod.Price <= 0 || prod.InStock < 0 || prod.Category < 0 || prod.Category > Enums.Category.Other || prod.Name == "Enter Name")
        {
            throw new BO.IncorrectInput("Invalid Input");
        }
        DO.Product p = new DO.Product
        {
            Name = prod.Name,
            Price = prod.Price,
            inStock = prod.InStock,
            Category = (DO.Enums.Category?)prod.Category
        };
        try
        {
            return dal!.Product.Add(p); //the the product to the list and return the ID
        }
        catch (DalApi.Duplicates)
        {
            throw new BO.IdExistException("Product ID already exists");
        }
    }
    public void DeleteProduct(int id) //check in every order that DO product is deleted 
    {
        dal!.Product.Delete(id);//delete product
        /*
              var v = from ords in DOList?.Order.GetAll()
                where ords != null && ords?.IsDeleted == false
                select from oi in DOList?.OrderItem.GetAll()
                       where oi != null && oi?.IsDeleted == false && oi?.OrderID == ords?.ID && oi?.ProductID == id
                       select oi;
        if (v.Any() == false)//no matching order items were found
        {
            throw new BO.UnfoundException();//id not found
        }
        try
        {
            DOList?.Product.Delete(id);//remove orderItem
        }
        catch (DalApi.IdNotExistException)
        {
            throw new BO.IdNotExistException("Product does not exist");
        }
         */
    }
    public void UpdateProduct(BO.Product prod) //Get BO product, and update the DO product
    {
        if (prod.Name == "" || prod.Price <= 0 || prod.InStock < 0 || prod.Category < 0 || prod.Category > Enums.Category.Other)
        {
            throw new IncorrectInput("Invalid input");
        }

        DO.Product p = new DO.Product(prod.ID)
        {
           // ID = prod.ID,
            Name = prod.Name,
            Price = prod.Price,
            inStock = prod.InStock,
            Category = (DO.Enums.Category?)prod.Category
        };
        try
        {
            dal?.Product.Update(p);
        }
        catch (DalApi.EntityNotFound)
        {
            throw new BO.EntityNotFound("The product does not exist!");
        }

    }

    //The Customer Functions
    public IEnumerable<ProductItem?> GetCatalog()
    {
        var v = from prods in dal?.Product.GetAll()
                where prods != null
                select new ProductItem()
                {
                    ID = prods?.ID ?? throw new BO.EntityNotFound("The product id does not exist"),
                    Name = prods?.Name,
                    Price = (double)prods?.Price,
                    Amount = (int)prods?.inStock,
                    Category = (BO.Enums.Category)prods?.Category,
                    InStock = prods?.inStock == 0 ? false : true
                };
        return v;

    }

    public int NextID()
    {
        return DO.Product.productCounter + 1;

    }
}
