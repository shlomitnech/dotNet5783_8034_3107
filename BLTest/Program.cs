namespace BLTest;
using BlApi;
using BO;
using BlImplementation;
using System;
using System.Net.Http.Headers;
using System.Collections.Generic;

internal class Program
{
    static IBl bl = new Bl();
    static void Main(string[] args)
    {
        Cart? cart = new Cart() { Items = new List<BO.OrderItem?>() }; //create a new cart
        BO.Product? product = new();
        BO.Order? order = new();
        int num1;
        int num2;
        int id;
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

                        case 2:
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
                        case 3:
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
                                bl.Product.AddProduct(product);
                            }
                            catch (IdExistException err)
                            {
                                Console.WriteLine(err.Message);
                            }

                            break;
                        case 4:
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
                    Console.WriteLine("To read all the lists of orders, press 1 \n" +
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
                                Console.WriteLine(bl.Order.ShipUpdate);

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
                        case 5: //to track an order
                            Console.WriteLine("What is the order ID? \n");
                            id = ReadFromUser();

                            try
                            {
                                Console.WriteLine(bl.Order.GetOrderTracking);

                            }
                            catch (Exception err)
                            {
                                Console.WriteLine(err.Message);
                            }
                            break;
                    }
                    break;

                case BO.Enums.Type.Cart:
                    {
                        Console.WriteLine("To add to the cart, press 1 \n" +
                            "To update a cart, press 2 \n" +
                            "To place an order, press 3 \n" +
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
                                Console.WriteLine("Please enter the product ID: ");
                                id = ReadFromUser();
                                try
                                {
                                    cart = bl.Cart.AddToCart(cart, id);
                                }
                                catch (Exception err)
                                {
                                    Console.WriteLine(err.Message);
                                }
                                break;
                            case 2:
                                int amt;
                                Console.WriteLine("Please enter the product ID: ");
                                id = ReadFromUser();
                                Console.WriteLine("What is the amount you would like to place in the cart?: ");
                                amt = ReadFromUser();
                                try
                                {
                                    cart = bl.Cart.UpdateCart(cart, id, amt); //update the cart to have more or less products and the total price
                                }
                                catch (EntityNotFound err)
                                {
                                    Console.WriteLine(err.Message);
                                }

                                break;
                            case 3:
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