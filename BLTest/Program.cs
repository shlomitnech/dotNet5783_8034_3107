namespace BLTest;
using BlApi;
using BO;
using BlImplementation;
using System;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Dal;

internal class Program
{
    static IBl bl = new Bl();
    static void Main(string[] args)
    {
        DataSource ds = new(); //to enable DataSource to call its constructors
        Cart? cart = new Cart() { Items = new List<BO.OrderItem?>() }; //create a new cart
        BO.Product? product = new();
        BO.Order? order = new();
        int num1, num2;
        int id;
        while (true)
        {
            Console.WriteLine("Choose a category: \n" +
                "To edit PRODUCTS, press 1 \n"
            + "To edit ORDERS, press 2 \n"
            + "To edit CART press 3 \n"
            + "To EXIT the program, press 0 ");
            num1 = 0;
            Int32.TryParse(Console.ReadLine(), out num1);
            while (num1 > 3 || num1 < 0)
            {
                Console.WriteLine("Please click a number 0-3");
                num1 = Convert.ToInt32(Console.ReadLine());
            }
            if (num1 == 0)
            {
                Console.WriteLine("You've chosen to close the program!");
                break; //leave the while loop
            }
            num1--;
            BO.Enums.Type choice = (BO.Enums.Type)num1; //set the enums to the number
            switch (choice)
            {
                case BO.Enums.Type.Product:
                    Console.WriteLine("To read all the lists, press 1 \n" +
                        "(Manager)To view a product, press 2 \n" +
                        "(Manager)To add a product press 3 \n" +
                        "(Manager)To delete a product, press 4 \n" +
                        "(Manger) To update a product, press 5 \n" +
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
                        case 1: //to view the whole list
                            try
                            {
                                IEnumerable<BO.ProductForList?> pr = bl.Product.GetProductForList();

                                foreach (ProductForList? f in pr)
                                {
                                    Console.WriteLine(f);
                                }
                            }
                            catch (BO.EntityNotFound err)
                            {
                                Console.WriteLine(err.Message);
                            }
                            break;

                        case 2: //to view a product
                            Console.WriteLine("What is the product ID? ");
                            id = ReadFromUser();
                            try
                            {
                                Console.WriteLine(bl.Product.ManagerProduct(id));
                            }
                            catch (EntityNotFound err)
                            {
                                Console.WriteLine(err.Message);
                            }
                            break;
                        case 3: //add a product
                            Console.WriteLine("What is the product's name? ");
                            string n =  Console.ReadLine() ?? "";
                            Console.WriteLine("What is the product's category number? ");
                            int categ = ReadFromUser();
                            product.Name = n;
                            product.Category = (BO.Enums.Category)categ;
                            Console.WriteLine("What is the stock?");
                            product.InStock = ReadFromUser();
                            Console.WriteLine("What is the price of the product? ");
                            product.Price = ReadFromUser();
                            try
                            {
                                Console.WriteLine("Your new product ID is: " + bl.Product.AddProduct(product));
                            }
                            catch (IdExistException err)
                            {
                                Console.WriteLine(err.Message);
                            }

                            break;
                        case 4: //delete a proudct
                            Console.WriteLine("What is the product's ID? ");
                            id = ReadFromUser();
                            try
                            {
                                bl.Product.DeleteProduct(id);
                            }
                            catch (EntityNotFound err)
                            {
                                Console.WriteLine(err.Message);
                            }

                            break;
                        case 5:
                            Console.WriteLine("What is the ID? ");
                            product.ID = ReadFromUser();
                            Console.WriteLine("What is the product's name? ");
                            n = Console.ReadLine() ?? "";
                            Console.WriteLine("What is the product's category number? ");
                            categ = ReadFromUser();
                            product.Name = n;
                            product.Category = (BO.Enums.Category)categ;
                            Console.WriteLine("What is the stock?");
                            product.InStock = ReadFromUser();
                            Console.WriteLine("What is the price of the product? ");
                            product.Price = ReadFromUser();
                            try
                            {
                                bl.Product.UpdateProduct(product);
                            }
                            catch (IdExistException err)
                            {
                                Console.WriteLine(err.Message);
                            }
                            break;
                        case 6:
                            try
                            {
                                IEnumerable<BO.ProductItem?> pr = bl.Product.GetCatalog();

                                foreach (ProductItem? f in pr)
                                {
                                    Console.WriteLine(f);
                                }
                            }
                            catch (BO.EntityNotFound err)
                            {
                                Console.WriteLine(err.Message);
                            }
                            break;


                    }

                    break;

                case BO.Enums.Type.Order:
                    Console.WriteLine("To read all the orders, press 1 \n" +
                        "To view an order, press 2 \n" +
                        "(Manger)To update the order ship date, press 3 \n" +
                        "(Manager)To update the order delivery date, press 4 \n" +
           
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
                            try
                            {
                                IEnumerable<BO.OrderForList?> shopping = bl.Order.GetAllOrderForList();
                                foreach (OrderForList? f in shopping)
                                {
                                    Console.WriteLine(f);
                                }

                            }
                            catch (Exception err)
                            {
                                Console.WriteLine(err.Message);
                            }
                            break;
                        case 2:
                            Console.WriteLine("What is the order ID? \n");
                                id = ReadFromUser();
                            try
                            {
                                Console.WriteLine(bl.Order.GetBOOrder(id));

                            }
                            catch (Exception err)
                            {
                                Console.WriteLine(err.Message);
                            }
                            break;
                        case 3: //to update the shpping update
                            Console.WriteLine("What is the order ID? \n");
                            id = ReadFromUser();

                            try
                            {
                      
                                Console.WriteLine("Enter the updated shipping date: \n");
                                DateTime date = DateTime.Parse(Console.ReadLine());
                                Console.WriteLine("The updated order is: \n" + bl.Order.ShipUpdate(id, date));
                            }
                            catch (Exception err)
                            {
                                Console.WriteLine(err.Message);
                            }
                            break;
                        case 4: //to update the delivery date
                            Console.WriteLine("What is the order ID? \n");
                            id = ReadFromUser();

                            try
                            {
                                Console.WriteLine(bl.Order.DeliveryUpdate);

                            }
                            catch (Exception err)
                            {
                                Console.WriteLine(err.Message);
                            }
                            break;
                       // case 5: //to track an order
                         //   Console.WriteLine("What is the order ID? \n");
                           // id = ReadFromUser();

                         //   try
                           // {
                             //   Console.WriteLine(bl.Order.GetOrderTracking);

                      //      }
                    //        catch (Exception err)
                        //    {
                            //    Console.WriteLine(err.Message);
                          //  }
                            //break;
                    }
                    break;

                case BO.Enums.Type.Cart:
                    {
                        Console.WriteLine("To add to the cart, press 1 \n" +
                            "To view cart, press 2 \n" +
                            "To update cart, press 3 \n" +
                            "To delete cart, press 4 \n" +
                            "To place an order, press 5 \n" +
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
                            case 1: //to add to
                                int amt;
                                Console.WriteLine("Please enter the product ID: ");
                                id = ReadFromUser();
                                Console.WriteLine("What is the amount you would like to place in the cart?: ");
                                amt = ReadFromUser();
                                try
                                {
                                    cart = bl.Cart.AddToCart(cart, id, amt);
                                }
                                catch (Exception err)
                                {
                                    Console.WriteLine(err.Message);
                                }
                                break;
                            case 2: //to view
                                Console.WriteLine(cart);
                                List<string> list = bl.Cart.GetItemNames(cart);
                                foreach (string item in list)
                                {
                                    Console.WriteLine(item);
                                    Console.WriteLine("\n");
                                }
                               
                                
                                break;
                            case 3: //to update
                                Console.WriteLine("Please enter the product ID: ");
                                id = ReadFromUser();
                                Console.WriteLine("What is the amount you would like to place in the cart?: ");
                                amt = ReadFromUser();
                                try
                                {
                                    bl.Cart.UpdateCart(cart, id, amt); //update the cart to have more or less products and the total price
                                }
                                catch (EntityNotFound err)
                                {
                                    Console.WriteLine(err.Message);
                                }

                                break;
                            case 4: //to delete
                                bl.Cart.DeleteCart(cart);
                                break;
                            case 5:
                                Console.WriteLine("What is the Customer's Name? ");
                                string n = Console.ReadLine() ?? "";
                                Console.WriteLine("What is the Customer's email? ");
                                string em = Console.ReadLine() ?? "";
                                Console.WriteLine("What is the shipping address? ");
                                string ship = Console.ReadLine() ?? "";
                                try
                                {
                                    bl.Cart.MakeOrder(cart, n, em, ship);
                                    Console.WriteLine("The Cart has been made");
                                }
                                catch (Exception err)
                                {
                                    Console.WriteLine(err.Message);
                                }
                                break;

                        }
                        break;
                    }
            }
        }
    }
    static int ReadFromUser()
    {
        int num;
        while (!System.Int32.TryParse(Console.ReadLine(), out num))
        {
            Console.WriteLine("Please put an integer value in \n");
        }
        return num;
    }

}