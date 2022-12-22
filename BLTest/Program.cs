namespace BLTest;
using BlApi;
using BO;
using BlImplementation;
using System;
using System.Net.Http.Headers;

internal class Program
{
    static IBl bl = new BL();
    static void Main(string[] args)
    {
        Cart? cart = new Cart() { Items = new List<BO.OrderItem?>() }; //create a new cart
        BO.Product? product = new();
        BO.Order? order = new();
        int num1;
        int num2;
        while (true)
        {
            Console.WriteLine("Choose a category: " +
                "To edit PRODUCTS, press 1 \n"
            + "To edit ORDERS, press 2 \n"
            + "To edit CART press 3 \n"
            + "To EXIT the program, press 0 ");
            num1 = 0;
            System.Int32.TryParse(Console.ReadLine(), out num1);
            while (num1 > 3 || num1 < 0)
            {
                Console.WriteLine("Please click a number 0-3");
                num1 = Convert.ToInt32(Console.ReadLine());
            }
            if (num1 == 0)
            {
                Console.WriteLine("You've chosen to stop editing!");
                break; //leave the while loop
            }
            num1--;
            BO.Enums.Type choice = (BO.Enums.Type)num1; //set the enums to the number
            switch (choice)
            {
                case BO.Enums.Type.Product:
                    Console.WriteLine("To read all the lists from product manger, press 1 \n" +
                        "To get info of products for the manager press 2 \n" +
                        "To add a product press 3 \n" +
                        "To delete a product, press 4 \n" +
                        "To update a product, press 5 \n" +
                        "To get the catalog for a customer, press 6 \n" +
                        "To return to the main menu, press 0 \n");
                    num2 = 0;
                    System.Int32.TryParse(Console.ReadLine(), out num2);
                    while (num2 > 6 || num2 < 0)
                    {
                        Console.WriteLine("Please click a number 0-6");
                        num2 = Convert.ToInt32(Console.ReadLine());
                    }
                    switch (num2)
                    {
                        case 0:
                            Console.WriteLine("You've chosen to stop editing!");
                            break;
                        case 1:
                            break;

                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;


                    }

                    break;

                case BO.Enums.Type.Order:
                    Console.WriteLine("To read all the lists of products, press 1 \n" +
                        "To get info of orders, press 2 \n" +
                        "To update the order ship date, press 3 \n" +
                        "To update the order delivery date, press 4 \n" +
                        "To track an order, press 5 \n" +
                        "To return to the main menu, press 0 \n");
                    num2 = 0;
                    System.Int32.TryParse(Console.ReadLine(), out num2);
                    while (num2 > 5 || num2 < 0)
                    {
                        Console.WriteLine("Please click a number 0-5");
                        num2 = Convert.ToInt32(Console.ReadLine());
                    }
                    switch (num2)
                    {
                        case 0:
                            Console.WriteLine("You've chosen to stop editing!");
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                    }


                    break;

                case BO.Enums.Type.Cart:
                    Console.WriteLine();
                    num2 = 0;
                    System.Int32.TryParse(Console.ReadLine(), out num2);
                    while (num2 > 3 || num2 < 0)
                    {
                        Console.WriteLine("Please click a number 0-3");
                        num1 = Convert.ToInt32(Console.ReadLine());
                    }
                    if (num2 == 0)
                    {
                        Console.WriteLine("You've chosen to stop editing!");
                        break; //leave the while loop
                    }
                    num2--;



                    break;

            }

            }



        }

    }

}