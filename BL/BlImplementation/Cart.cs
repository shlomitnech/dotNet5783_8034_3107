﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using Dal;
using DO;
using System.Net;
using System.Xml.Linq;

namespace BlImplementation;

internal class Cart : ICart
{
    static IDal Dos = new DalList();
    static IBl blay = new Bl();
    public BO.Cart AddToCart(BO.Cart cart, int id, int amount) //check if the product is in the cart, if not add it from DO product if it is in stock
    {
        int index = cart.Items.FindIndex(x => x != null && x.ID == id);
        DO.Product p = new DO.Product();
        p = Dos.Product.Get(id);
        if (amount < 0) throw new BO.IncorrectInput();
        if (p.inStock < 1) throw new BO.Exceptions("Product in unavailable");
        if (p.inStock < amount) throw new BO.Exceptions("There aren't enough in stock!");
        if (index != -1)
        {
            cart.Items[index].Amount = amount;
            cart.TotalPrice += (cart.Items[index].Price*amount);
            return cart;
        }

        BO.OrderItem item = new BO.OrderItem()
        {
            ID = id,
            Price = p.Price,
            Amount = amount,
            ProductID = p.ID
        };

        cart.Items.Add(item);
        cart.TotalPrice = CalculateTotalPrice(cart);
        return cart;
    }
    public BO.Cart UpdateCart(BO.Cart cart, int id, int newAmount) //update the cart to have more or less products and the total price
    {
        int index = cart.Items.FindIndex(x => x != null && x.ID == id);

        DO.Product p = new DO.Product();
        p = Dos.Product.Get(id);

        if (p.inStock < 1) throw new Exception("Product in unavailable");
        if (index == -1) throw new BO.EntityNotFound("Order does not exist");
        if (newAmount > p.inStock) throw new Exception("Product in unavailable");

        cart.Items[index].Amount = newAmount;
        cart.TotalPrice = CalculateTotalPrice(cart);

        return cart;

    }

    public void MakeOrder(BO.Cart cart, string n, string em, string add) //approve the items in the cart and make the real order
    {
        if (n == "" || em == "" || add == "")//check input
        {
            throw new BO.UnfoundException("Incorrect Input");
        }
        cart.CustomerName = n;
        cart.CustomerEmail = em;
        cart.CustomerAddress = add;
        DO.Product product = new DO.Product();
        DO.Order order = new DO.Order(); // create an instance of order
        BO.Order orderBO = new BO.Order();
        int ordID = Dos.Order.Add(order); // adding a new order to the list (this is the new order)
        order.OrderDate = DateTime.Now;
        orderBO.ID = ordID;
        orderBO.CustomerName = order.CustomerName;
        orderBO.CustomerEmail = order.CustomerEmail;
        orderBO.CustomerAddress = order.ShippingAddress;
        orderBO.PaymentDate = order.OrderDate;
        orderBO.ShipDate = order.ShippingDate;
        orderBO.DeliveryDate = order.DeliveryDate;
        orderBO.Status = blay.Order.GetStatus(order); 
        orderBO.TotalPrice = (double)cart.TotalPrice;
        int quantity = 0;
        foreach (BO.OrderItem? item in cart.Items)
        {
            orderBO.Items.Add(item);
            quantity++;
        }
        try
        {
            foreach (BO.OrderItem it in cart.Items)
            {
                int quant = it.Amount;
                DO.OrderItem item = new DO.OrderItem();
                item.productID = it.ProductID;
                item.orderID = ordID;
                product = Dos.Product.Get(it.ProductID);
                if (product.inStock < quant)
                {
                    throw new BO.Exceptions("Not enough are in stock! ");
                }
                product.inStock -= quant;
                Dos.Product.Update(product);
            }
        }
        catch
        {
            throw new Exception("ERROR! ");
        }
  
    }

    public double? CalculateTotalPrice(BO.Cart cart)
    {
        double? totalPrice = 0;
        
        foreach(var gogo in cart.Items)
        {
            totalPrice += (gogo.Amount* gogo.Price);
        }

        return totalPrice;
    }
}
