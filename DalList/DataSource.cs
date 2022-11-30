namespace Dal;
using DO;
using System.Collections.Generic;



public class DataSource
{
    internal static DataSource s_instance { get; } //returns a copy of the data
    readonly static Random random = new Random(); //random number generatior
    public Order[] orders = new Order[100];
    public Product[] product = new Product[50];
    public OrderItem[] orderItem = new OrderItem[200];
    internal static class Config{

        internal const int startingOrderNumber = 20;
        public static int s_nextOrderNumber = startingOrderNumber;
        internal static int nextOrderNumber { get => ++s_nextOrderItemNumber; }
        internal const int startingProductNumber = 10;
        public static int s_nextProductNumber = startingProductNumber;
        internal static int nextProductNumber { get => ++s_nextProductNumber; }
        internal const int startingOrderItemNumber = 40;
        public static int s_nextOrderItemNumber = startingOrderItemNumber;
        internal static int nextOrderItemNumber { get=> ++s_nextOrderItemNumber; }
    }

    void s_Initialize() //Calls the constructors to initialize the data entities
    {
        newOrder(); //function that creates a new order
        newProduct(); //function that creates a new product
        newOrderItem(); //function that creates order Items
    }
    private void newOrder() //function to initialize orders
    {
        string[] customerName = { "Hudi", "Maeven", "Shana", "Michal", "Adina", "Jessica", "Avigayil", "Binny", "Shlomit", "Mordechai", "Nava", "Avigail", "Refael", "Yona", "Guy", "Joyce", "Meir", "Daniella", "Yaakovah", "Shira" };
        string[] customerEmail = { "Hudi@jct.ac.il", "Maeven@jct.ac.il", "Shana@jct.ac.il", "Michal@jct.ac.il", "Adina@jct.ac.il", "Jessica@jct.ac.il", "Avigayil@jct.ac.il", "Binny@jct.ac.il", "Shlomit@jct.ac.il", "Mordechai@jct.ac.il", "Nava@jct.ac.il", "Avigail@jct.ac.il", "Refael@jct.ac.il", "Yona@jct.ac.il", "Guy@jct.ac.il", "Joyce@jct.ac.il", "Meir@jct.ac.il", "Daniella@jct.ac.il", "Yaakovah@jct.ac.il", "Shira@jct.ac.il" };
        string[] customerAddress = { "1 apple", "2 banana", "3 carrot", "4 dumpling", "5 eel", "6 fox", "7 guava", "8 haloumi", "9 ice cream", "10 juice", "11 krispy kreme", "12 licorice", "13 meatball", "14 noodles", "15 orange", "16 papaya", "17 quince", "18 rice", "19 soupppp", "20 tisch" };
        for(int i = 0; i<20; i++)
        {
            Order myOrder = new(); 
            myOrder.customerName = customerName[random.Next(customerName.Length)];
            myOrder.customerEmail = customerEmail[random.Next(customerEmail.Length)];
            myOrder.shippingAddress = customerAddress[random.Next(customerAddress.Length)];
            myOrder.shippingDate = DateTime.MinValue;
            myOrder.orderDate = DateTime.MinValue;
            myOrder.DeliveryDate = DateTime.MinValue;
            myOrder.ID = Config.nextOrderNumber;
            myOrder.IsDeleted = false;

        }
    }
    private void newProduct()
    {
        string[] nameOfProduct = {"Challah", "Poppers", "Kugel", "Onion Dip", "Garlic Dip", "Chicken on the Bone", "Rice", "Grilled Chicken",
                                  "Green Beans", "Broccoli", "Zucchini", "Cauliflower", "Brownies", "Chocolate Chip Cookies", "Rice Krispy Treats" };
        for(int i = 0; i<10; i++)
        {
            Product myProduct = new();
            myProduct.name = nameOfProduct[random.Next(nameOfProduct.Length)];
            myProduct.ID = Config.nextProductNumber;
            myProduct.price = random.Next(25, 200);
            myProduct.Category = (Enums.Category)random.Next(0, 5);
            myProduct.inStock = random.Next(15, 60);
            myProduct.isDeleted = false;
        }
    }
    private void newOrderItem()
    {
        ///finish this

    }
}

