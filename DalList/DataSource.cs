﻿namespace Dal;
namespace DO;

public class DataSource
{
    internal static DataSource s_instance { get; } //returns a copy of the data
    readonly static Random random = new Random() //random number generatior
    internal Order orders[100];
    internal Product product[50];
    internal OrderItem orderItem[200]
    internal static class Config{

        internal const int s_startingOrderNumber = 20;
        public static int s_nextOrderNumber = startingOrderNumber;
        internal static int nextOrderNumber { get => ++s_nextOrderItemNumber; }
        internal const int s_startingProductNumber = 10;
        public static int s_nextProductNumber = startingProductNumber;
        internal static int nextProductNumber { get => ++s_nextProductNumber; }
        internal const int s_startingOrderItemNumber = 40;
        public static int s_nextOrderItemNumber = startingOrderItemNumber;
        internal static int nextOrderItemNumber { get=> ++s_nextOrderItemNumber; }
    }

    void s_Initialize() //Calls the constructors to initialize the data entities
    {
        newOrder(); //function that creates a new order
        newProduct(); //function that creates a new product
        newOrderItem(); //function that creates order Items
    }
    private void newOrder() //default constructor to make a new order
    {
        

        ID = 0;
        customerName = "";
        customerEmail = "";
        shippingAddress = "";
        orderDate = DateTime.MinValue;
        shippingDate = DateTime.MinValue;
        DeliveryDate = DateTime.MinValue;
        IsDeleted = false;
    }
    private void newProduct()
    {
        ID = 0;
        name = "";
        price = 0;
        Category = "";
        inStock = 0;
        isDeleted = false;
    }
    private void newOrderItem()
    {
        ID = 0;
        productID = 0;
        orderID = 0;
        price = 0;
        amount = 0;
        isDeleted = false;
    }
}

