using DO;
using System.Security.Cryptography;
using System.Threading;
using DalApi;
using System.Runtime.CompilerServices;

namespace Dal;

public class DalOrderItem : IOrderItem //change to be internal?
{
    /// <summary>
    /// Inserts a new Order Item to the main array
    /// </summary>
    /// <param name="current"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(OrderItem item ) //FIX THIS TO WORK WITH LISTTT
    {
        bool isOrder = DataSource.orders.Exists(x => x.ID == item.orderID); //see if the order exists
        if (!isOrder) throw new Exception("order does not exist");
        int productIndex = DataSource.products.FindIndex(x => x.ID == item.productID);//see if the product exists
        if (productIndex < 0) throw new EntityNotFound("Product does not exist");

        //Check the Product Stock Count
        if (DataSource.products[productIndex].inStock - item.amount < 0)
            throw new EntityNotFound("There are not enough in stock");
        // else
        //      DataSource.products[productIndex].inStock = (DataSource.products[productIndex].inStock - item.amount); //update the stock

        //If we already have 200 products then throw error
        if ((DataSource.orderItems.Count >= 200)) 
            throw new EntityNotFound("Too many order items!\n");

        //If the orderItem is not out of stock and there is space, insert it into products
        {
            int newID = DataSource.Config.NextOrderItemNumber;
            item.ID = newID;
            DataSource.orderItems.Add(item);
            return newID;  
        }
    }

    /// <summary>
    /// Takes the ID and returns the insance so it can be printed
    /// </summary>
    /// <param name="currentID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem Get(int currentID)
    {

        OrderItem thisOrdItem = DataSource.orderItems.Find(x => x.ID == currentID);
        if (thisOrdItem.ID != currentID)
            throw new Exception("No product has that ID");    //if product is not found
        return thisOrdItem;
    }
    

    /// <summary>
    /// Returns only the existing instances of Orders Items to be printed to the screen
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem[] GetAll()
    {
        //check that the array is not empty
        if(DataSource.orderItems==null)  throw new Exception("There are currently no orderitems \n");

        //put all existing order items in new array to return it
        OrderItem[] tempItems = new OrderItem[DataSource.countOrderItems];

        for(int i = 0; i < tempItems.Length; i++)
        {
            tempItems[i] = DataSource.orderItems[i];
        }

        return tempItems;
    
    }

    /// <summary>
    /// Changes the info of an existing instance
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem item)
    {
        int indexItem = DataSource.orderItems.FindIndex(x => x.ID == item.ID);
        if (indexItem < 0) { throw new EntityNotFound("Order Item does not exist!"); }
        bool isOrder = DataSource.orders.Exists(x => x.ID == item.orderID); //see if the order exists
        if (!isOrder) throw new Exception("order does not exist");
        int productIndex = DataSource.products.FindIndex(x => x.ID == item.productID);//see if the product exists
        if (productIndex < 0) throw new EntityNotFound("Product does not exist");

        //Check the Product Stock Count
        if (DataSource.products[productIndex].inStock - item.amount < 0)
            throw new EntityNotFound("There are not enough in stock");
       // else
      //      DataSource.products[productIndex].inStock = (DataSource.products[productIndex].inStock - item.amount); //update the stock

        //If everything is good, update the item
        DataSource.orderItems[indexItem] = item;

    }

    /// <summary>
    /// Deletes the chosen instance
    /// </summary>
    /// <param name="currentID"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int currentID)
    {
       //delete the order from the array and update the rest of the array

        int index = DataSource.orderItems.FindIndex(x => x.ID == currentID); // Is this correct?

         if (index == -1) // Item doesn't exist
                throw new Exception("Product does not exist");

         DataSource.products.RemoveAt(index);
         return;
    }

    /// <summary>
    /// Sends back the instances with the given order ID
    /// </summary>
    /// <param name="currID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem[] sameOrder(int currID)
    {
        int count1 = 0;
        int count2 = 0;

        for(int i = 0; i < DataSource.countOrderItems; i++)
        {
            if(DataSource.orderItems[i].orderID == currID)
            {
                count1++;
            }
        }

        if (count1 == 0) throw new Exception("There are no products in this order");
        
        OrderItem[] tempSameOrder = new OrderItem[count1];

        for(int i = 0; i<DataSource.countOrderItems; i++)
        {
            if (DataSource.orderItems[i].orderID == currID)
            {
                tempSameOrder[count2++] = DataSource.orderItems[i];
            }
        }

        return tempSameOrder;
    }

}
