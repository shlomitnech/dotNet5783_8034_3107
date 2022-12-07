using DO;
using System.Security.Cryptography;

namespace Dal;

public class DalOrderItems
{
    public int InsertOrderItem(OrderItem current )
    {
        //Check if the order item already exists
        for (int i = 0; i < (DataSource.countOrderItems); i++)
        {
            if (current.ID == DataSource.orderItems[i].ID)
                throw new Exception("Item already exists\n");
        }
        //If we already have 200 products then throw error
        if ((DataSource.countOrderItems >= 200)) //DOES this update the order count?
        {
            throw new Exception("Too many order items!\n");
        }
        //If the orderItem is not out of stock and there is space, insert it into products
        {
            int newID = DataSource.Config.NextOrderItemNumber;
            DataSource.orderItems[newID] = current;
            DataSource.countOrderItems++;
            return newID;
             
        }
    }
    public OrderItem ReadOrderItem(int currentID)
    {
        for (int i = 0; i < (DataSource.countOrderItems); i++)
        {
            if (currentID == DataSource.orderItems[i].ID) //IS THIS CORRECT? yes!
                return DataSource.orderItems[i]; //return the product
        }
        throw new Exception("No product has that ID");

    }
    //Read out all the orderItems
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

    public void UpdateOrderItems(OrderItem item)
    {
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


}
