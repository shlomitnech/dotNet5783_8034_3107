using Dal;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using static DO.Enums;
//namespace DalAPI;

using DO;
namespace Dal;

internal class DalTest
{
    static void Main(string[] args)
    {
        Product _product = new();
        DalProducts dalprod = new();
        Order _order = new();
        DalOrder dalord = new();
        OrderItem _orderItem = new();
        DalOrderItems dalOrdItem = new ();
        int num1, num2;
        bool flag = true;
        DataSource ds = new(); //to enable DataSource to call its constructors
        Random _random = new Random(); //to make random times/variables in the main

        while (flag) //while user input is not 0
        {
            Console.WriteLine("To edit PRODUCTS, press 1 \n"
             + "To edit ORDERS, press 2 \n"
             + "To edit ORDER ITEMS press 3 \n"
             + "To EXIT the program, press 0 "
           );
            num1 = Convert.ToInt32(Console.ReadLine());
            while (num1 > 3 || num1 < 0) 
            {
                Console.WriteLine("Please click a number 0-3");
                num1 = Convert.ToInt32(Console.ReadLine());
           
            }
            if (num1 == 0)
                break; //leave the while loop
            Enums.Type type = (Enums.Type)num1 - 1;

            if (num1 == 3)  //They asked for Order Items
            {
                Console.WriteLine("To ADD, press 1 \n"
                     + "To UPDATE, press 2 \n"
                     + "To VIEW ONE, press 3 \n"
                     + "to VIEW WHOLE LIST, press 4 \n"
                     + "to DELETE, press 5 \n"
                     + "to VIEW ALL ORDER items in an order, press 6 \n"
                     + "to exit, press 0 " );
                num2 = Convert.ToInt32(Console.ReadLine());
                while (num2 > 6 || num2 < 0) //check for error
                {
                    Console.WriteLine("Please click a number 0-6");

                    num2 = Convert.ToInt32(Console.ReadLine());
                }
            }
            else //They asked for Order or Product
            {
                Console.WriteLine("To ADD, press 1 \n"
                  + "To UPDATE, press 2 \n"
                  + "To VIEW ONE, press 3 \n"
                  + "to VIEW WHOLE LIST, press 4 \n"
                  + "to DELETE, press 5 \n"
                  + "to exit, press 0 "
                  );

                num2 = Convert.ToInt32(Console.ReadLine());
                while (num2 > 5 || num2 < 0) //check for error
                {
                    Console.WriteLine("Please click a number 0-5");

                    num2 = Convert.ToInt32(Console.ReadLine());
                }

            }


            if (num2 == 0)
                break;    //Leave the while loop
   
            Enums.Action action = (Enums.Action)num2-1;
            switch (type, action)
            {
                case (Enums.Type.Product, Enums.Action.Add):
                    try
                    {
                        Console.WriteLine("Write the product's name:");
                        _product.Name = Console.ReadLine() ?? "";
                        Console.WriteLine("Write the product's price ");
                        _product.Price = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write the product's category number? " +
                              "1 = Bread \n" +
                              "2 = Dips \n" +
                              "3 = MainCourse \n" +
                              "4 =  Sides \n" +
                              "5 =  Desserts ");
                        int cat = Convert.ToInt32(Console.ReadLine());
                        while (cat < 1 || cat > 5)
                        {
                            Console.WriteLine("Please only choose a number 1-5");
                            cat = Convert.ToInt32(Console.ReadLine());
                        }
                        _product.Category = (Enums.Category)(cat-1);
                        Console.WriteLine("Write how many products are in stock?");
                        _product.inStock = Convert.ToInt32(Console.ReadLine());
                        int prodsID = dalprod.InsertProduct(_product);
                        Console.WriteLine("Your product's ID is: " + prodsID);
                        

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! " + ex.Message); //print out the error to the screen
                    }
                    break;

                case (Enums.Type.Product, Enums.Action.Update):
                    try
                    {
                        Console.WriteLine("What is the products name?: ");
                        _product.Name = Console.ReadLine() ?? "";
                        
                        Console.WriteLine("What is the products ID?: ");
                        _product.ID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("What is the products price?: ");
                        _product.Price = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Write the product's category number? \n" +
                           "1 = Bread \n" +
                           "2 = Dips \n" +
                           "3 = MainCourse \n" +
                           "4 =  Sides \n" +
                           "5 =  Desserts \n ");
                        int cat = Convert.ToInt32(Console.ReadLine());
                        while (cat < 1 || cat > 5)
                        {
                            Console.WriteLine("Please only choose a number 1-5");
                            cat = Convert.ToInt32(Console.ReadLine());
                        }
                        _product.Category = (Enums.Category)(cat-1);
                        Console.WriteLine("How many items are in stock?");
                        _product.inStock = Convert.ToInt32(Console.ReadLine());
                        dalprod.UpdateProducts(_product);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! " + ex.Message); //print out the error to the screen
                    }
                    break;
                case (Enums.Type.Product, Enums.Action.getItem):
                    try
                    {
                        Console.WriteLine("What is the product's ID #?");
                        int currID = Convert.ToInt32(Console.ReadLine());
                        Product p = dalprod.ReadProduct(currID);
                        Console.WriteLine(p); //print out the information on the product
                    }
                    catch (Exception ex)
                    {
                       Console.WriteLine("Error! " + ex.Message); //print out the error 
                    }
                    break;

                case (Enums.Type.Product, Enums.Action.GetList):
                    try
                    {
                    Product[] productlist = dalprod.ReadAllProducts(); // call function to read all the products 

                        foreach (Product pizza in productlist)
                        {
                            Console.WriteLine(pizza);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! " + ex.Message); //print out the error 
                    }
                    break;
                case (Enums.Type.Product, Enums.Action.Delete ):
                    try
                    {
                        Console.WriteLine("What is the product's ID #?");
                        int currID = Convert.ToInt32(Console.ReadLine());
                        dalprod.DeleteProduct(currID);
                        //Should we then print out the updated list?
                        //dalprod.ReadAllProducts();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! " + ex.Message); //print out the error 
                    }
                    break;
                case (Enums.Type.Order, Enums.Action.Add):
                    try
                    {
                       
                        Console.WriteLine("Write the customers's name: ");
                        _order.CustomerName = Console.ReadLine() ?? "";
                        Console.WriteLine("Write the customer's email: ");
                        _order.CustomerEmail = Console.ReadLine() ?? "";
                        Console.WriteLine("Write the customer's shipping address: ");
                        _order.ShippingAddress = Console.ReadLine() ?? "";
                        _order.OrderDate = DateTime.Now;
                        _order.ShippingDate = DateTime.Now.AddDays(_random.Next(3, 7));
                        _order.DeliveryDate = DateTime.Now.AddDays(_random.Next(8, 21));
                        int ordID = dalord.InsertOrder(_order);
                        Console.WriteLine("Your product's ID is: " + ordID);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! " + ex.Message); //print out the error 
                    }
                    break;

                case (Enums.Type.Order, Enums.Action.Update):
                    try
                    {
                        Console.WriteLine("Write the customers's name: ");
                        _order.CustomerName = Console.ReadLine() ?? "";
                        Console.WriteLine("What is the order ID?: ");
                        _order.ID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write the customer's email: ");
                        _order.CustomerEmail = Console.ReadLine() ?? "";
                        Console.WriteLine("Write the customer's shipping address: ");
                        _order.ShippingAddress = Console.ReadLine() ?? "";
                        dalord.UpdateOrders(_order); //update the order
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! " + ex.Message); //print out the error 
                    }
                    break;
                case (Enums.Type.Order, Enums.Action.getItem):
                    try
                    {
                        Console.WriteLine("What is the order ID #? \n");
                        int currID = Convert.ToInt32(Console.ReadLine());
                        Order p = dalord.ReadOrder(currID);
                        Console.WriteLine(p); //print out the information on the product
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error!" + ex.Message); //print out the error 
                    }
                    break;

                case (Enums.Type.Order, Enums.Action.GetList):
                    try
                    {
                        Order[] Orderlist = dalord.ReadAllOrders(); // call function to read all the products 

                        foreach (Order shoes in Orderlist)
                        { 
                            Console.WriteLine(shoes);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! " + ex.Message); //print out the error 
                    }
                    break;
                case (Enums.Type.Order, Enums.Action.Delete):
                    try
                    {
                        Console.WriteLine("What is the orders's ID #? ");
                        int currID = Convert.ToInt32(Console.ReadLine());
                        dalord.DeleteOrder(currID);
                        //Should we then print out the updated list?
                        //dalord.ReadAllProducts();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error!" + ex.Message); //print out the error 
                    }
                    break;
                case (Enums.Type.OrderItem, Enums.Action.Add):
                    try
                    {
                        Console.WriteLine("Write the order's ID? ");
                        _orderItem.orderID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write the product's price ");
                        _orderItem.price = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write the product's ID ");
                        _orderItem.productID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write how many products they put in the order: ");
                        _orderItem.amount = Convert.ToInt32(Console.ReadLine());
                        int ordItID = dalOrdItem.InsertOrderItem(_orderItem); 
                        Console.WriteLine("Your order Item's ID is: " + ordItID);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! " + ex.Message); //print out the error 
                    }
                    break;

                case (Enums.Type.OrderItem, Enums.Action.Update):
                    try
                    {
                        Console.WriteLine("Write the order items's ID: ");
                        _orderItem.ID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write the product's price ");
                        _orderItem.price = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write the product's ID ");
                        _orderItem.productID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write the order's ID? ");
                        _orderItem.orderID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write how many products they put in the order: ");
                        _orderItem.amount = Convert.ToInt32(Console.ReadLine());
                        dalOrdItem.UpdateOrderItems(_orderItem); //send to function to be updated

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! " + ex.Message); //print out the error 
                    }
                    break;
                case (Enums.Type.OrderItem, Enums.Action.getItem):
                    try
                    {
                        Console.WriteLine("What is the order item's ID #? ");
                        int currID = Convert.ToInt32(Console.ReadLine());
                        OrderItem p = dalOrdItem.ReadOrderItem(currID);
                        Console.WriteLine(p); //print out the information on the product
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error!" + ex.Message); //print out the error 
                    }
                    break;

                case (Enums.Type.OrderItem, Enums.Action.GetList):
                    try
                    {
                        OrderItem[] OrderItemList = dalOrdItem.ReadAllOrderItems(); // call function to read all the products 

                        foreach (OrderItem pizza in OrderItemList)
                        {
                            Console.WriteLine(pizza);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! " + ex.Message); //print out the error 
                    }
                    break;
                case (Enums.Type.OrderItem, Enums.Action.Delete):
                    try
                    {
                        Console.WriteLine("What is the ID of the orderItem your wish to delete? \n "); 
                        int currID = Convert.ToInt32(Console.ReadLine());
                        dalOrdItem.DeleteOrderItem(currID);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! " + ex.Message); //print out the error 
                    }
                    break;
                case (Enums.Type.OrderItem, Enums.Action.readOrder):
                    try
                    {
                        Console.WriteLine("What is the ID of the order your wish to read? \n ");
                        int ordID = Convert.ToInt32(Console.ReadLine()); //order number


                        OrderItem[] OrderItemList = dalOrdItem.sameOrder(ordID); // call function to read all the products 

                        foreach (OrderItem squash in OrderItemList)
                        {
                            Console.WriteLine(squash);
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! " + ex.Message); //print out the error 
                    }
                    break;

            }
        }
            


    }
}