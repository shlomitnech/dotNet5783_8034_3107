using DO;
using System.Security.Cryptography;

namespace Dal;

public class DalOrderItems
{
    /// <summary>
    /// Inserts a new Order Item to the main array
    /// </summary>
    /// <param name="current"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int InsertOrderItem(OrderItem current )
    {
        bool ifOrderExists = false;
        bool ifProductExists = false;

        //Check if the order item already exists, and if the order id exists and product id exists
        for (int i = 0; i < (DataSource.countOrderItems); i++)
        {
            if (current.ID == DataSource.orderItems[i].ID)
                throw new Exception("Item already exists\n");

            if(current.orderID == DataSource.orders[i].ID)
                ifOrderExists = true;

            if (current.productID == DataSource.products[i].ID)
            {
                ifProductExists = true;
                if (DataSource.products[i].inStock - (current.amount) < 0)// find out how many are in stock and see if there are enough
                {
                    throw new Exception("There are not enough in stock");
                }
                else
                    DataSource.products[i].inStock = (DataSource.products[i].inStock - current.amount);//update the stock count
            }
        }

        //If the order or product does not exist it will throw an exception
        if (!ifOrderExists) throw new Exception("order does not exist");
        if (!ifProductExists) throw new Exception("product does not exist");
        
        //If we already have 200 products then throw error
        if ((DataSource.countOrderItems >= 200)) //DOES this update the order count?
            throw new Exception("Too many order items!\n");


        //If the orderItem is not out of stock and there is space, insert it into products
        {
            int newID = DataSource.Config.NextOrderItemNumber;
            current.ID = newID;
            DataSource.orderItems[DataSource.countOrderItems++] = current;
            return newID;
             
        }
    }

    /// <summary>
    /// Takes the ID and returns the insance so it can be printed
    /// </summary>
    /// <param name="currentID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem ReadOrderItem(int currentID)
    {
        for (int i = 0; i < (DataSource.countOrderItems); i++)
        {
            if (currentID == DataSource.orderItems[i].ID) //IS THIS CORRECT? yes!
                return DataSource.orderItems[i]; //return the product
        }
        
        throw new Exception("No product has that ID");

    }

    /// <summary>
    /// Returns only the existing instances of Orders Items to be printed to the screen
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem[] ReadAllOrderItems()
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
    public void UpdateOrderItems(OrderItem item)
    {
        bool ifOrderExists = false;
        bool ifProductExists = false;
        for (int i = 0; i < (DataSource.countOrderItems); i++) //check that the order and product chosen exists!
        {
            if (item.orderID == DataSource.orders[i].ID)
                ifOrderExists = true;

            if (item.productID == DataSource.products[i].ID)
            {
                ifProductExists = true;
                if ((DataSource.products[i].inStock - ((item.amount))) < 0)// find out how many are in stock and see if there are enough
                {
                    throw new Exception("There are not enough in stock");
                }
                else
                    DataSource.products[i].inStock = (DataSource.products[i].inStock - item.amount);//update the stock count
            }
        }
       
        //If the order or product does not exist it will throw an exception
        if (!ifOrderExists) throw new Exception("order does not exist");
        if (!ifProductExists) throw new Exception("product does not exist");

        for (int i = 0; i < (DataSource.countOrderItems); i++)
        {
            if (item.ID == DataSource.orderItems[i].ID)
            {
                DataSource.orderItems[i] = item;
                return;
            }
        }
        throw new Exception("Order item doesn't exist! \n");

    }

    /// <summary>
    /// Deletes the chosen instance
    /// </summary>
    /// <param name="currentID"></param>
    /// <exception cref="Exception"></exception>
    public void DeleteOrderItem(int currentID)
    {
        bool deleted = false;
        for (int i = 0; i < DataSource.countOrderItems; i++) //run through the products until the product with this idea is found
        {
            if (DataSource.orderItems[i].ID == currentID) //delete product and update the array
            {
                deleted = true;
                for (int j = i; j < DataSource.countOrderItems; j++)
                {
                    DataSource.orderItems[j] = DataSource.orderItems[j + 1]; //Update the list
                }
                DataSource.countOrderItems--;
                break;
            }
        }
        if (!deleted) //if didn't find/delete the product
            throw new Exception("The order item didn't exist \n");

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
