﻿using Dal;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using static DO.Enums;

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
        DalOrderItems dalOrdItem = new();
        //DataSource _dataSource = new DataSource();
        int num1, num2;
        bool flag = true;

        while (flag) //while user input is not 0
        {
            Console.WriteLine("To edit the products, press 1 \n"
             + "To edit orders, press 2 \n"
             + "To edit order items press 3 \n"
             + "To exit the program, press 0"
           );
            num1 = Convert.ToInt32(Console.ReadLine());
            while (num1 > 3)
            {
                Console.WriteLine("Please click a number 0-3");
                num1 = Convert.ToInt32(Console.ReadLine());

            }
            if (num1 == 0)
                break; //leave the while loop
            Enums.Type type = (Enums.Type)num1;
            Console.WriteLine("To add, press 1 \n"
              + "To view one, press 2 \n"
              + "to view the whole list, press 3 \n"
              + "to delete, press 4 \n"
              + "to exit, press 0 \n"
              );
            num2 = Convert.ToInt32(Console.ReadLine());
            while (num2 > 4) //check for error
            {
                Console.WriteLine("Please click a number 0-4");

                num2 = Convert.ToInt32(Console.ReadLine());
            }

            if (num2 == 0)
                break;    //Leave the while loop

            Enums.Action action = (Enums.Action)num2;
            switch (type, action)
            {
                case (Enums.Type.Product, Enums.Action.Add):
                    try
                    {
                        Console.WriteLine("Write the product's name: \n");
                        _product.Name = Console.ReadLine() ?? "";
                        Console.WriteLine("Write the product's price \n");
                        _product.Price = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write the product's category number? " +
                              "1 = Bread" +
                              "2 = Dips" +
                              "3 = MainCourse" +
                              "4 =  Sides" +
                              "5 =  Desserts \n");
                        _product.Category = (Enums.Category)(Convert.ToInt32(Console.ReadLine()));
                        Console.WriteLine("Write how many products are in stock");
                        _product.inStock = Convert.ToInt32(Console.ReadLine());
                        int prodsID = dalprod.InsertProduct(_product);
                        Console.WriteLine("Your product's ID is: " + prodsID);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error!" + ex); //print out the error to the screen
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
                        _product.Price = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write the product's category number? " +
                           "1 = Bread" +
                           "2 = Dips" +
                           "3 = MainCourse" +
                           "4 =  Sides" +
                           "5 =  Desserts ");
                        _product.Category = (Enums.Category)(Convert.ToInt32(Console.ReadLine()));
                        Console.WriteLine("How many items are in stock? \n");
                        _product.inStock = Convert.ToInt32(Console.ReadLine());
                        dalprod.UpdateProducts(_product);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error!" + ex); //print out the error to the screen
                    }
                    break;
                case (Enums.Type.Product, Enums.Action.getItem):
                    try
                    {
                        Console.WriteLine("What is the product's ID #? \n");
                        int currID = Convert.ToInt32(Console.ReadLine());
                        Product p = dalprod.ReadProduct(currID);
                        Console.WriteLine(p); //print out the information on the product
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error!" + ex); //print out the error 
                    }
                    break;

                case (Enums.Type.Product, Enums.Action.GetList):
                    try
                    {
                        dalprod.ReadAllProducts(); // call function to read all the products 
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error!" + ex); //print out the error 
                    }
                    break;
                case (Enums.Type.Product, Enums.Action.Delete):
                    try
                    {
                        Console.WriteLine("What is the product's ID #? \n");
                        int currID = Convert.ToInt32(Console.ReadLine());
                        dalprod.DeleteProduct(currID);
                        //Should we then print out the updated list?
                        //dalprod.ReadAllProducts();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error!" + ex); //print out the error 
                    }
                    break;
                case (Enums.Type.Order, Enums.Action.Add):
                    try
                    {
                        Console.WriteLine("Write the customers's name: \n");
                        _order.CustomerName = Console.ReadLine() ?? "";
                        Console.WriteLine("Write the customer's email: \n");
                        _order.CustomerEmail = Console.ReadLine() ?? "";
                        Console.WriteLine("Write the customer's shipping address: \n");
                        _order.ShippingAddress = Console.ReadLine() ?? "";
                        // ADD DATE TIME!!!!
                        int ordID = dalord.InsertOrder(_order);
                        Console.WriteLine("Your product's ID is: " + ordID);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error!" + ex); //print out the error 
                    }
                    break;

                case (Enums.Type.Order, Enums.Action.Update):
                    try
                    {
                        Console.WriteLine("Write the customers's name: \n");
                        _order.CustomerName = Console.ReadLine() ?? "";
                        Console.WriteLine("What is the customer ID?: ");
                        _order.ID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write the customer's email: \n");
                        _order.CustomerEmail = Console.ReadLine() ?? "";
                        Console.WriteLine("Write the customer's shipping address: \n");
                        _order.ShippingAddress = Console.ReadLine() ?? "";
                        _order.ShippingAddress = Console.ReadLine() ?? "";


                        // ADD DATE TIMES!!!!
                        dalord.UpdateOrders(_order); //update the order
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error!" + ex); //print out the error 
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
                        Console.WriteLine("Error!" + ex); //print out the error 
                    }
                    break;

                case (Enums.Type.Order, Enums.Action.GetList):
                    try
                    {
                        dalord.ReadAllOrders(); // call function to read all the orders 

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error!" + ex); //print out the error 
                    }
                    break;
                case (Enums.Type.Order, Enums.Action.Delete):
                    try
                    {
                        Console.WriteLine("What is the orders's ID #? \n");
                        int currID = Convert.ToInt32(Console.ReadLine());
                        dalord.DeleteOrder(currID);
                        //Should we then print out the updated list?
                        //dalord.ReadAllProducts();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error!" + ex); //print out the error 
                    }
                    break;
                case (Enums.Type.OrderItem, Enums.Action.Add):
                    try
                    {
                        Console.WriteLine("Write the order items's name: \n");
                        _product.Name = Console.ReadLine() ?? "";
                        Console.WriteLine("Write the product's price \n");
                        _orderItem.price = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write the product's ID \n");
                        _orderItem.productID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write the order's ID? \n");
                        _orderItem.orderID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write how many products they put in the order: \n");
                        _orderItem.amount = Convert.ToInt32(Console.ReadLine());
                        int ordItID = dalOrdItem.InsertOrderItem(_orderItem);
                        Console.WriteLine("Your order Item's ID is: " + ordItID);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error!" + ex); //print out the error 
                    }
                    break;

                case (Enums.Type.OrderItem, Enums.Action.Update):
                    try
                    {
                        Console.WriteLine("Write the order items's name: \n");
                        _product.Name = Console.ReadLine() ?? "";
                        Console.WriteLine("Write the product's price \n");
                        _orderItem.price = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write the product's ID \n");
                        _orderItem.productID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write the order's ID? \n");
                        _orderItem.orderID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write how many products they put in the order: \n");
                        _orderItem.amount = Convert.ToInt32(Console.ReadLine());
                        dalOrdItem.UpdateOrderItems(_orderItem); //send to function to be updated

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error!" + ex); //print out the error 
                    }
                    break;
                case (Enums.Type.OrderItem, Enums.Action.getItem):
                    try
                    {
                        Console.WriteLine("What is the order item's ID #? \n");
                        int currID = Convert.ToInt32(Console.ReadLine());
                        OrderItem p = dalOrdItem.ReadOrderItem(currID);
                        Console.WriteLine(p); //print out the information on the product
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error!" + ex); //print out the error 
                    }
                    break;

                case (Enums.Type.OrderItem, Enums.Action.GetList):
                    try
                    {
                        dalOrdItem.ReadAllOrderItems();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error!" + ex); //print out the error 
                    }
                    break;
                case (Enums.Type.OrderItem, Enums.Action.Delete):
                    try
                    {
                        Console.WriteLine("What is the ID of the orderItem your wish to delete? \n ");
                        int currID = Convert.ToInt32(Console.ReadLine());
                        dalOrdItem.DeleteOrderItem(currID);
                        //PRINT updated list?


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error!" + ex); //print out the error 
                    }
                    break;
            }
        }



    }
}