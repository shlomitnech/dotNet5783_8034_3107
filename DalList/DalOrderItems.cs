using DO;
using System.Security.Cryptography;

namespace Dal;

public class DalOrderItems
{
    public void InsertOrderItem(OrderItem current )
    {
        //Check if the order item already exists
        for (int i = 0; i < (DataSource.Config.s_nextOrderItemNumber); i++)
        {
            if (current.ID == DataSource.orderItems[i].ID)
                throw new Exception("Item already exists\n");
        }
        //If we already have 200 products then throw error
        if ((DataSource.Config.s_nextOrderItemNumber) >= 200) //DOES this update the order count?
        {
            throw new Exception("Too many order items!\n");
        }
        //If the orderItem is out of stock and there is space, insert it into products
        {
            DataSource.orderItems[DataSource.Config.NextOrderNumber] = current;
        }
    }
    //Read out all the orderItems
    public void ReadAllOrderItems()
    {
        int index = 0;
        foreach(OrderItem current in DataSource.orderItems) 
        {
            DataSource.orderItems[index].ToString();
        }

        throw new Exception("No orderItem has that ID \n");
    }

    public void UpdateOrderItems(OrderItem item)
    {
        for (int i = 0; i < (DataSource.Config.s_nextOrderItemNumber); i++)
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
        for (int i = 0; i < DataSource.Config.s_nextOrderItemNumber; i++) //run through the products until the product with this idea is found
        {
            if (DataSource.orderItems[i].ID == currentID) //delete product and update the array
            {
                deleted = true;
                for (int j = i; j < DataSource.Config.s_nextOrderItemNumber; j++)
                {
                    DataSource.orderItems[j] = DataSource.orderItems[j + 1]; //Update the list
                }
                break;
            }
        }
        if (!deleted) //if didn't find/delete the product
            throw new Exception("The order item didn't exist \n");


    }


}
