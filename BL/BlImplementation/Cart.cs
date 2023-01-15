using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using DO;

namespace BlImplementation;

internal class Cart : BlApi.ICart
{
    DalApi.IDal? dal = DalApi.Factory.Get();
    BlApi.IBl? blay = BlApi.Factory.Get();
    public BO.Cart AddToCart(BO.Cart cart, int id, int amount) //check if the product is in the cart, if not add it from DO product if it is in stock
    {
        int index = cart.Items!.FindIndex(x => x != null && x.ID == id);
        DO.Product? p = new();
        p = (DO.Product?)dal!.Product.Get(id);
        if (amount < 0) throw new BO.IncorrectInput();
        if (p?.inStock < 1) throw new BO.Exceptions("Product in unavailable");
        if (p?.inStock < amount) throw new BO.Exceptions("There aren't enough in stock!");
        if (index != -1)
        {
            cart.Items[index]!.Amount = amount;
            cart.TotalPrice += (cart.Items[index]!.Price*amount);
            return cart;
        }

        BO.OrderItem? item = new()
        {
            ID = id,
            Price = p?.Price,
            Amount = amount,
            ProductID = (int)p?.ID!
        };

        cart.Items?.Add(item);
        cart.TotalPrice = CalculateTotalPrice(cart);
        return cart;
    }
    public BO.Cart UpdateCart(BO.Cart cart, int id, int newAmount) //update the cart to have more or less products and the total price
    {
        int index = cart.Items!.FindIndex(x => x != null && x.ID == id);

        DO.Product? p = new DO.Product();
        p = (DO.Product?)dal!.Product.Get(id);

        if (p?.inStock < 1) throw new Exception("Product in unavailable");
        if (index == -1) throw new BO.EntityNotFound("Order does not exist");
        if (newAmount > p?.inStock) throw new Exception("Product in unavailable");

        cart.Items[index]!.Amount = newAmount;
        cart.TotalPrice = CalculateTotalPrice(cart);

        return cart;

    }

    public void MakeOrder(BO.Cart cart, string n, string em, string add) //approve the items in the cart and make the real order
    {
        if (n == "" || em == "" || add == "")//check input
        {
            throw new BO.UnfoundException("Incorrect Input");
        }
        IEnumerable<DO.Product?> productList = dal?.Product.GetAll()!;//get all products from dal
        IEnumerable<string> checkOrderItem = from BO.OrderItem item in cart.Items!
                                             let product = productList.FirstOrDefault(x => x?.ID == item.ID)
                                             where item.Amount < 1 || product?.inStock < item.Amount
                                             select item.ProductName + " is not in stock\n";//check if all of the products in cart are in stock
        if (checkOrderItem.Any())
            throw new BO.EntityNotFound("That product doesn't exist");

        int? orderId = dal?.Order.Add(new DO.Order()
        {
            ShippingAddress = cart.CustomerAddress!,
            CustomerEmail = cart.CustomerEmail!,
            CustomerName = cart.CustomerName!,
            OrderDate = DateTime.Now
        });//add a new order for the cart and get order ID
        try
        {
            cart.Items!.ForEach(x => dal.OrderItem.Add(new DO.OrderItem()
            {
                amount = (int)x?.Amount!,
                ID = (int)x.ID!,
                orderID = (int)orderId!,
                Price = x.Price,
                productID = x.ProductID
            }));//go over cart order items and add each to dal
        }
        catch (DalApi.Exceptions ex)
        {
            throw new BO.Exceptions(ex.Message);
        }
        catch (DalApi.Duplicates ex)
        {
            throw new BO.IdExistException(ex.Message);
        }
        IEnumerable<DO.Product?> products;
        try
        {
            products = from item in cart.Items
                       select dal?.Product.Get(item.ProductID);//list of products in cart
        }
        catch (DalApi.EntityNotFound)
        {
            throw new BO.EntityNotFound("The product does not exist");
        }

    }
    public List<string> GetItemNames(BO.Cart cart)
    {
        int prodId;
        DO.Product? product = new(); 
        List<string> list = new();
        foreach (BO.OrderItem? item in cart.Items!) //find all the products in the cart
        {
            prodId = (int)item.ID!;
            product = dal!.Product.Get(prodId);
            list?.Add(product?.Name!);
        }
        return list;

    }
    public void DeleteCart(BO.Cart mycart)
    {
        mycart.CustomerName = "";
        mycart.CustomerEmail = "";
        mycart.CustomerAddress = "";
        mycart.Items!.Clear();
        mycart.TotalPrice = 0;

    }


    static public double? CalculateTotalPrice(BO.Cart cart)
    {
        double? totalPrice = 0;
        
        foreach(var gogo in cart.Items!)
        {
            totalPrice += (gogo!.Amount* gogo.Price);
        }

        return totalPrice;
    }
}
