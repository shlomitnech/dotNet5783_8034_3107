using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// <summary>
    /// creating instances of dl and bl
    /// </summary>
    DalApi.IDal? dal = DalApi.Factory.Get();
    BlApi.IBl? blay = BlApi.Factory.Get();

    /// <summary>
    /// adding order items to the customer's cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    /// <exception cref="BO.IncorrectInput"></exception>
    /// <exception cref="BO.Exceptions"></exception>
    public BO.Cart AddToCart(BO.Cart cart, int id, int amt) //check if the product is in the cart, if not add it from DO product if it is in stock
    {
        if (cart.Items == null)
        {
            DO.Product? prod = new DO.Product();
            try
            {
                prod = dal!.Product.Get(id);
            }
            catch
            {
                throw new BO.EntityNotFound();
            }
            if (amt < 0)
            {
                throw new BO.IncorrectInput();
            }
            if (prod?.inStock < amt || prod?.inStock < 1) //not enough items in stock
            {
                throw new BO.Exceptions("Not enough items in stock");
            }
            BO.OrderItem myItem = new BO.OrderItem //Create a new orderitem
            {
                ID = id,
                ProductID = (int)prod?.ID!,
                ProductName = prod?.Name!,
                Amount = amt,
                Price = prod?.Price!,
            };
            cart.Items = new List<BO.OrderItem?>(); 
            cart.Items!.Add(myItem);//add it to my cart
            cart.TotalPrice = (double)prod?.Price!;

            return cart;

        }
        int index = cart.Items.FindIndex(x =>  x.ID == id); // find the index of where the product is sitting in the Items list
        DO.Product? product = new DO.Product(-1); // create a DO product
        product = dal!.Product.Get(id); // get the DO product with the matching ID.
        if (amt < 0) // if the amount is negative
        { 
            throw new BO.IncorrectInput();
        }
        if (product?.inStock < amt || product?.inStock < 1) // if there is not enough products in stock
        {
            throw new BO.Exceptions("This product is out of stock");
        }
        if (index != -1) 
        {
            cart.Items[index]!.Amount += amt;
            cart.TotalPrice += cart.Items[index]!.Price * amt; // adjust the total price accordingly
            return cart;
        }
        BO.OrderItem item = new BO.OrderItem // create new orderitem that is being added to the list
        {
            ID = id,
            ProductName = product?.Name!,
            Price = (double)product?.Price!,
            Amount = amt,
            ProductID = (int)product?.ID!
        };
        cart.Items.Add(item); 
        cart.TotalPrice += item.Price * amt; //update the price according to the amount
        return cart;
    }
    public BO.Cart RemoveFromCart(BO.Cart cart, int id, int amt) //remove an item from a cart
    {
        if (cart.Items == null)
        {
            
        }
        int index = cart.Items.FindIndex(x => x.ID == id); // find the index of where the product is sitting in the Items list
        DO.Product? product = new DO.Product(-1); // create a DO product
        product = dal!.Product.Get(id); // get the DO product with the matching ID.
        if (index != -1)
        {
            cart.Items[index]!.Amount -= amt;
            cart.TotalPrice -= cart.Items[index]!.Price * amt; // adjust the total price accordingly
            return cart;
        }
        BO.OrderItem item = new BO.OrderItem // create new orderitem that is being added to the list
        {
            ID = id,
            ProductName = product?.Name!,
            Price = (double)product?.Price!,
            Amount = amt,
            ProductID = (int)product?.ID!
        };
        cart.Items.Remove(item);
        cart.TotalPrice -= item.Price * amt; //update the price according to the amount
        return cart;
    }

    /// <summary>
    /// updated the desired attributes of the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <param name="newAmount"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="BO.EntityNotFound"></exception>
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

        /// <summary>
        /// makes a new instance of a order using the cart
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="n"></param>
        /// <param name="em"></param>
        /// <param name="add"></param>
        /// <exception cref="BO.UnfoundException"></exception>
        /// <exception cref="BO.EntityNotFound"></exception>
        /// <exception cref="BO.Exceptions"></exception>
        /// <exception cref="BO.IdExistException"></exception>
        public int? MakeOrder(BO.Cart cart, string n, string em, string add) //approve the items in the cart and make the real order
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
                ShippingAddress = add,
                CustomerEmail = em!,
                CustomerName = n,
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
           return orderId;
        }

        /// <summary>
        /// returns the instances of the names of order items in the cart
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public List<string> GetItemNames(BO.Cart cart)
        {
            //int prodId;
            //DO.Product? product = new(); 
            IEnumerable<string> list;
            /*foreach (BO.OrderItem? item in cart.Items!) //find all the products in the cart
            {
                prodId = (int)item.ID!;
                product = dal!.Product.Get(prodId);
                list?.Add(product?.Name!);
            }*/
            /*var v = from item in cart.Items!
                    where item != null
                    //let prodId = (int)item.ID!
                    let product = dal!.Product.Get((int)item!.ID)
                    select product?.Name;
                    list!.Add(v);

            return list;*/
            throw new NotImplementedException();

        }

        /// <summary>
        /// deletes the entire cart
        /// </summary>
        /// <param name="mycart"></param>
        public void DeleteCart(BO.Cart mycart)
        {
            mycart.CustomerName = "";
            mycart.CustomerEmail = "";
            mycart.CustomerAddress = "";
            mycart.Items!.Clear();
            mycart.TotalPrice = 0;

        }

        /// <summary>
        /// helper function to calculate the total price of the cart
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
       static public double? CalculateTotalPrice(BO.Cart cart)
        {
            double? totalPrice = 0;

            foreach (var gogo in cart.Items!)
            {
                totalPrice += (gogo!.Amount * gogo.Price);
            }
            return totalPrice;
        }
       public IEnumerable<BO.OrderItem> getCart(BO.Cart cart)
       {
        var v = from items in cart.Items
                where items != null
                select items;
        return v;

        throw new BO.Exceptions("List is empty");

       }
}
