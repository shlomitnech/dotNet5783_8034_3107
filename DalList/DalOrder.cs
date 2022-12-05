namespace Dal;
using DO;



public class DalOrder
{ 
    public void InsertOrder(Order current)
    {
        //If we already have 100 orders then it will send an error
        if (DataSource.Config.s_nextOrderNumber-1100 >= 100)
        {
            throw new Exception("Can't take more orders");
        }

        //take the instance and add it to the array
        DataSource.orders[DataSource.Config.NextOrderNumber - 1000] = current;

    }

    public void DeleteOrder(int currentID)
    {
        //If there are no orders it will send an error 
        if(DataSource.orders == null)
        {
            throw new Exception("There are no orders");
        }

        Order other = ReadOrder(currentID, false);
        int index = other.ID;  //the place in the array where the order that we want to delete is

        //delete the order from the array and update the rest of the array
        for(int i = index; i < DataSource.Config.s_nextOrderNumber-1000; i++)
        {
            DataSource.orders[i] = DataSource.orders[i+1];
        }

    }

    public Order ReadOrder(int currentID, bool flag1 = DataSource.Config.debug)
    {
        int index = 0;
        bool flag2 = false;

        //finding the order
        for (int i = 0; i < DataSource.orders.Length; i++)
        {
            if (DataSource.orders[i].ID == currentID)   //when the order is found
            {
                index = i;        //keep the place of order
                flag2 = true;     
                break;
            }
        }

        //if we went through all the orders and the ID was never found
        if (DataSource.orders[DataSource.orders.Length].ID != currentID && !flag2)
        {
            throw new Exception("Order does not exist");
        }

        if (flag1)  DataSource.orders[index].ToString();
     

        return DataSource.orders[index];
    }

    public void ReadAllOrders()
    {
        foreach(Order order in DataSource.orders)
        {
            order.ToString();
        }
    }

    public void UpdateOrders(Order current)
    {
        for(int i = 0; i < DataSource.Config.s_nextOrderNumber-1000; i++)
        {
            if(DataSource.orders[i].ID == current.ID)
            {
                DataSource.orders[i] = current;
                return;
            }
        }

        throw new Exception("Order does not exist!");
    }


}
