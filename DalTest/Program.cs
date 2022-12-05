using Dal;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using static DO.Enums;

using DO;
namespace Dal;

internal class DalTest
{
    static void Main()
    {
        Product _product = new Product();
        DalProducts dalprod = new DalProducts();
        Order _order = new Order();
        DalOrder dalord = new DalOrder();
        OrderItem _orderItem = new OrderItem();
        DalOrderItems dalOrdItem = new DalOrderItems();
        // DataSource _dataSource = new DataSource();
        int num1, num2;
        string response1, response2;
        bool flag = true;

        while (flag) //while user input is not 0
        {
            Console.WriteLine("To edit the products, press 1 \n"
             + "To edit orders, press 2 \n"
             + "To edit order items press 3 \n"
             + "To exit the program, press 0"
           );
            response1 = Console.ReadLine();
            num1 = Convert.ToInt32(response1);
            while (num1 > 3) 
            {
                Console.WriteLine("Please click a number 0-3");
                response1 = Console.ReadLine();
                num1 = Convert.ToInt32(response1);
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
            response2 = Console.ReadLine();
            num2 = Convert.ToInt32(response2);
            while (num2 > 4)
            {
                Console.WriteLine("Please click a number 0-4");
                response2 = Console.ReadLine();
                num1 = Convert.ToInt32(response1);
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
                        Console.WriteLine("Write the product's category number ( \n"));
                        _product.Category = (Enums.Category)(Convert.ToInt32(Console.ReadLine()));
                        Console.WriteLine("Write how many products are in stock");
                        _product.inStock = Convert.ToInt32(Console.ReadLine());
                        int prodsID = dalprod.UpdateProducts(_product); //fix this
                        Console.WriteLine("Your product's ID is: " + prodsID);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error!");
                    }
                    break;

                case (Enums.Type.Product, Enums.Action.Update):
                    try
                    {

                    }
                    catch (Exception ex)
                    {


                    }
                    break;
                case (Enums.Type.Product, Enums.Action.getItem):
                    try
                    {

                    }
                    catch (Exception ex)
                    {


                    }
                    break;

                case (Enums.Type.Product, Enums.Action.GetList):
                    try
                    {

                    }
                    catch (Exception ex)
                    {


                    }
                    break;
                case (Enums.Type.Product, Enums.Action.Delete ):
                    try
                    {

                    }
                    catch (Exception ex)
                    {


                    }
                    break;
                case (Enums.Type.Order, Enums.Action.Add):
                    try
                    {

                    }
                    catch (Exception ex)
                    {


                    }
                    break;

                case (Enums.Type.Order, Enums.Action.Update):
                    try
                    {

                    }
                    catch (Exception ex)
                    {


                    }
                    break;
                case (Enums.Type.Order, Enums.Action.getItem):
                    try
                    {

                    }
                    catch (Exception ex)
                    {


                    }
                    break;

                case (Enums.Type.Order, Enums.Action.GetList):
                    try
                    {

                    }
                    catch (Exception ex)
                    {


                    }
                    break;
                case (Enums.Type.Order, Enums.Action.Delete):
                    try
                    {

                    }
                    catch (Exception ex)
                    {


                    }
                    break;
                case (Enums.Type.OrderItem, Enums.Action.Add):
                    try
                    {

                    }
                    catch (Exception ex)
                    {


                    }
                    break;

                case (Enums.Type.OrderItem, Enums.Action.Update):
                    try
                    {

                    }
                    catch (Exception ex)
                    {


                    }
                    break;
                case (Enums.Type.OrderItem, Enums.Action.getItem):
                    try
                    {

                    }
                    catch (Exception ex)
                    {


                    }
                    break;

                case (Enums.Type.OrderItem, Enums.Action.GetList):
                    try
                    {

                    }
                    catch (Exception ex)
                    {


                    }
                    break;
                case (Enums.Type.OrderItem, Enums.Action.Delete):
                    try
                    {

                    }
                    catch (Exception ex)
                    {


                    }
                    break;

            }
        }
            


    }
}