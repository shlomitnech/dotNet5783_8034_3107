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
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter)
    {

        if (DataSource.orderItems != null)
        {
            return (IEnumerable<OrderItem?>)DataSource.orderItems;

        }
        throw new Exception("There are no orders!");

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
        int index = -1;
        foreach (DO.OrderItem ordi in DataSource.orderItems)
        {
            if (ordi.ID == currentID)
            {
                index = DataSource.orderItems.IndexOf(ordi); // save the index of the product with the matching ID#
                break;
            }
        }
        DO.OrderItem DeleteOrderItem = DataSource.orderItems[index]; // save the product in the found index
        DataSource.orderItems.Remove(DeleteOrderItem); // remove the product
    }

    /// <summary>
    /// Sends back the instances with the given order ID
    /// </summary>
    /// <param name="currID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
   // public OrderItem[] sameOrder(int currID)
  //  {
        /*
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
        */
        // return tempSameOrder;
  //  }
  
    public OrderItem GetByFilter(Func<OrderItem?, bool>? filter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));

        foreach (OrderItem ord in DataSource.orderItems)
        {
            if (filter!(ord))
            {
                return ord;
            }
        }
        throw new Exception("Does not exist\n");
    }


}
