namespace Dal;
using DO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class DataSource
{
    readonly static Random random = new Random(); //random number generater
    internal static List<Order> orders { get; set; } = new List<Order>();
    internal static List<Product> products { get; set; } = new List<Product>();
    internal static List<OrderItem> orderItems { get; set; } = new List<OrderItem>();
    internal static DataSource? s_instance { get; } //This caused double ...
    public DataSource() { s_Initialize(); } //default constructor called
    
    /// <summary>
    /// Class that defines auto incremental IDs for all data entities
    /// </summary>
    internal static class Config{ //Will help us create our data list 

       //Variables for the Order
        internal const int startingOrderNumber = 1000; //Order #'s are 4 digits long starting with the first instance of it being called
        public static int s_nextOrderNumber = startingOrderNumber;
        internal static int NextOrderNumber { get => ++s_nextOrderNumber; } //increases by one when called

        //Variables for the Product
        internal const int startingProductNumber = (100000); //Product #'s are 6 digits long after the first next call
        public static int s_nextProductNumber = startingProductNumber;
        internal static int NextProductNumber { get => ++s_nextProductNumber; }

        //Variables for the OrderItem
        internal const int startingOrderItemNumber = 0;
        public static int s_nextOrderItemNumber = startingOrderItemNumber;
        internal static int NextOrderItemNumber { get=> ++s_nextOrderItemNumber; }

        //Internal Static Flag
        internal static bool debug = true;
    }

    static void s_Initialize() //Calls the constructors to initialize the data entities
    {
        NewOrder(); //function that creates a new order
        NewProduct(); //function that creates a new product
        NewOrderItem(); //function that creates order Items
    }
    /// <summary>
    /// Methods for the DataSource initialization
    /// </summary>
    static private void NewOrder() //function to initialize orders
    {
        string[] customerName = { "Hudi", "Maeven", "Shana", "Michal", "Adina", "Jessica", "Avigayil", "Binny", "Shlomit", "Mordechai", "Nava", "Avigail", "Refael", "Yona", "Guy", "Joyce", "Meir", "Daniella", "Yaakovah", "Shira" };
        string[] customerEmail = { "Hudi@jct.ac.il", "Maeven@jct.ac.il", "Shana@jct.ac.il", "Michal@jct.ac.il", "Adina@jct.ac.il", "Jessica@jct.ac.il", "Avigayil@jct.ac.il", "Binny@jct.ac.il", "Shlomit@jct.ac.il", "Mordechai@jct.ac.il", "Nava@jct.ac.il", "Avigail@jct.ac.il", "Refael@jct.ac.il", "Yona@jct.ac.il", "Guy@jct.ac.il", "Joyce@jct.ac.il", "Meir@jct.ac.il", "Daniella@jct.ac.il", "Yaakovah@jct.ac.il", "Shira@jct.ac.il" };
        string[] customerAddress = { "1 apple", "2 banana", "3 carrot", "4 dumpling", "5 eel", "6 fox", "7 guava", "8 haloumi", "9 ice cream", "10 juice", "11 krispy kreme", "12 licorice", "13 meatball", "14 noodles", "15 orange", "16 papaya", "17 quince", "18 rice", "19 soupppp", "20 tisch" };
        for (int i = 0; i < 20; i++)
        {
            Order myOrder = new()
            {
            CustomerName = customerName[random.Next(customerName.Length)],
            CustomerEmail = customerEmail[random.Next(customerEmail.Length)],
            ShippingAddress = customerAddress[random.Next(customerAddress.Length)],
            OrderDate = DateTime.Now.AddDays(random.Next(365)),
            ShippingDate = DateTime.MinValue ,
            DeliveryDate = DateTime.MinValue,
            ID = Config.NextOrderNumber,
            };
            if (i<4) orders.Add(myOrder); ;   //the first 4 orders won't have ship dates

            myOrder.ShippingDate = myOrder.OrderDate?.AddDays(random.Next(3, 7));    //order will ship 3-7 days after ordered

            if(i>=4 && i < 10) orders.Add(myOrder); ;    //the first 10 orders won't have delivery dates

            myOrder.DeliveryDate = myOrder.ShippingDate?.AddDays(random.Next(7, 21));   //order will be delivered 7-21 days after shipped because this is Israel we're talking about

            if(i>=5 && i < 20) orders.Add(myOrder);   //all other orders will have everything initialized




        }
    }
   static private void NewProduct()
    {
        string[] nameOfProduct = {"Challah", "Rolls", //Challah (0-1)
                                 "Onion Dip", "Garlic Dip", "Chummus", "Matbucha", "Schug", //Dips (2-6)
                                 "Poppers", "Chicken", "Steak", "Veal", "Lamb", "Eggplant", "Tofu",    //Main (7-13)
                                 "Rice",  "Kugel",  "Beans", "Broccoli", "Zucchini","Cauliflower",  //Sides (14-19)
                                 "Brownies", "Cookies", "RiceKrispyTreats", "Rugelach" }; //Desserts (20-23)

        //initialize 10 products in the array
        for (int i = 0; i < 10; i++)
        {
            //create a new product
            Product myProduct = new()
            {
                Name = nameOfProduct[random.Next(nameOfProduct.Length)],
                ID = Config.NextProductNumber,
                Price = random.Next(25, 200),
                Category = (Enums.Category)random.Next(0, 5), // change? to make category match ?
                inStock = random.Next(15, 50),
                isDeleted = false,
            };
            
            if (i < 3) myProduct.inStock = 0;   //make sure 5% is out of stock (Guy Kelman told us to do this)
            
            products.Add(myProduct);  //push product
        }
    }
   static private void NewOrderItem()
    {
        //initialize 40 order items in the array
        for (int i = 0; i < 40; i++)
        {
            Product prod = products[random.Next(products.Count)];
            orderItems.Add(  
                new OrderItem
            {
                ID = Config.NextOrderItemNumber,
                productID = prod.ID,
                Price = (double)prod.Price,
                orderID = random.Next(Config.startingOrderNumber, Config.s_nextOrderNumber),
                amount = random.Next(3)
            });
            
       

        }
    }
}

