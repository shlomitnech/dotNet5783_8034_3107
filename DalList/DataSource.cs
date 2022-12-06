namespace Dal;
using DO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

internal static class DataSource
{
    readonly static Random random = new Random(); //random number generatior

    internal static Order[] orders = new Order[100];
    internal static Product[] products = new Product[50];
    internal static OrderItem[] orderItems = new OrderItem[200];
    internal static class Config{ //Will help us create our data list 

       //Variables for the Order
        internal const int startingOrderNumber = 1000; //Order #'s are 4 digits long 
        public static int s_nextOrderNumber = startingOrderNumber;
        internal static int NextOrderNumber { get => ++s_nextOrderItemNumber; } //increases by one when called

        //Variables for the Product
        internal const int startingProductNumber = 100000; //Product #'s are 6 digits long
        public static int s_nextProductNumber = startingProductNumber;
        internal static int NextProductNumber { get => ++s_nextProductNumber; }

        //Variables for the OrderItem
        internal const int startingOrderItemNumber = 0;
        public static int s_nextOrderItemNumber = startingOrderItemNumber;
        internal static int NextOrderItemNumber { get=> ++s_nextOrderItemNumber; }

        //Internal Static Flag
        internal static bool debug = true;
    }
  //  internal static DataSource s_instance { get; } //returns a copy of the data

    static DataSource() { s_Initialize(); } //default constructor called
  
    static void s_Initialize() //Calls the constructors to initialize the data entities
    {
        NewOrder(); //function that creates a new order
        NewProduct(); //function that creates a new product
        NewOrderItem(); //function that creates order Items
    }
    static private void NewOrder() //function to initialize orders
    {
        string[] customerName = { "Hudi", "Maeven", "Shana", "Michal", "Adina", "Jessica", "Avigayil", "Binny", "Shlomit", "Mordechai", "Nava", "Avigail", "Refael", "Yona", "Guy", "Joyce", "Meir", "Daniella", "Yaakovah", "Shira" };
        string[] customerEmail = { "Hudi@jct.ac.il", "Maeven@jct.ac.il", "Shana@jct.ac.il", "Michal@jct.ac.il", "Adina@jct.ac.il", "Jessica@jct.ac.il", "Avigayil@jct.ac.il", "Binny@jct.ac.il", "Shlomit@jct.ac.il", "Mordechai@jct.ac.il", "Nava@jct.ac.il", "Avigail@jct.ac.il", "Refael@jct.ac.il", "Yona@jct.ac.il", "Guy@jct.ac.il", "Joyce@jct.ac.il", "Meir@jct.ac.il", "Daniella@jct.ac.il", "Yaakovah@jct.ac.il", "Shira@jct.ac.il" };
        string[] customerAddress = { "1 apple", "2 banana", "3 carrot", "4 dumpling", "5 eel", "6 fox", "7 guava", "8 haloumi", "9 ice cream", "10 juice", "11 krispy kreme", "12 licorice", "13 meatball", "14 noodles", "15 orange", "16 papaya", "17 quince", "18 rice", "19 soupppp", "20 tisch" };
        for (int i = 0; i < 20; i++)
        {
            Order myOrder = new()
            {
            customerName = customerName[random.Next(customerName.Length)],
            customerEmail = customerEmail[random.Next(customerEmail.Length)],
            shippingAddress = customerAddress[random.Next(customerAddress.Length)],
            orderDate = DateTime.Now,
            shippingDate = new DateTime(orderDate) ,
            DeliveryDate = DateTime.Now,
            ID = Config.NextOrderNumber,
            IsDeleted = false
        };
            orders[i] = myOrder;
        }
    }
   static private void NewProduct()
    {
        string[] nameOfProduct = {"Challah", "Rolls", //Challah
                                 "Onion Dip", "Garlic Dip", "Chummus", "Matbucha", "Schug", //Dips
                                 "Poppers", "Chicken", "Steak", "Veal", "Lamb", "Eggplant", "Tofu",    //Main
                                 "Rice",  "Kugel",  "Beans", "Broccoli", "Zucchini","Cauliflower",  //Sides
                                 "Brownies", "Cookies", "RiceKrispyTreats", "Rugelach" }; //Desserts

        //initialize 10 products in the array
        for (int i = 0; i < 10; i++)
        {
            //create a new product
            Product myProduct = new()
            {
                Name = nameOfProduct[random.Next(nameOfProduct.Length)],
                ID = Config.NextProductNumber,
                Price = random.Next(25, 200),
                Category = (Enums.Category)random.Next(0, 5),
                inStock = random.Next(15, 60),
                isDeleted = false,
            };
            
            if (Config.s_nextProductNumber - 100000 < 3) myProduct.inStock = 0;   //make sure 5% is out of stock (Dr. Kelman told us to do this)
            
            products[i] = myProduct;  //push product into the array
        }

    }
   static private void NewOrderItem()
    {
        //initialize 40 order items in the array
        for (int i = 0; i < 40; i++)
        {
            Product prod = products[random.Next(Config.startingProductNumber, Config.s_nextProductNumber)];
            OrderItem myOrderItem = new()
            {
                ID = Config.NextOrderItemNumber,
                productID = prod.ID,
                price = prod.Price,
                orderID = random.Next(Config.startingOrderNumber, Config.s_nextOrderNumber),
                amount = random.Next(3)
            };
            
            //push the order item into the array
            orderItems[i] = myOrderItem;

        }
    }
}

