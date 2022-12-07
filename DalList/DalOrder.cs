namespace Dal;
using DO;



public class DalOrder
{ 
    public int InsertOrder(Order current)
    {
        //If we already have 100 orders then it will send an error
        if (DataSource.Config.s_nextOrderNumber-1100 >= 100)
        {
            throw new Exception("Can't take more orders");
        }
        //take the instance and add it to the array
        int newID = DataSource.Config.NextOrderNumber;
        current.ID = newID;
        DataSource.orders[newID - 1000] = current;
        return newID;
    }
    public void DeleteOrder(int currentID)
    {
        //If there are no orders it will send an error 
        if(DataSource.orders == null)
        {
            throw new Exception("There are no orders");
        }

        //delete the order from the array and update the rest of the array
        for(int i = 0; i < DataSource.Config.s_nextOrderNumber-1000; i++)
        {
            if (DataSource.orders[i].ID == currentID)
            {
                for (int j = i; j < DataSource.Config.s_nextOrderNumber - 1000; j++)
                { 
                    DataSource.orders[j] = DataSource.orders[j + 1];
                }
                return;
            }
        }

        throw new Exception("Order does not exist");

    }

    public Order ReadOrder(int currentID)
    {

        //find the order based on the identifier in the array
        for (int i = 0; i < (DataSource.Config.s_nextOrderNumber -999); i++)
        {

            if (currentID == DataSource.orders[i].ID) 
                return DataSource.orders[i]; //return the product
        }
        throw new Exception("No order has that ID");    //if we cant find order throw an exception

    }

    public Order[] ReadAllOrders()
    {
        //check that array is not empty
        if (DataSource.orders == null) throw new Exception("There are no orders!");
        //make a new array with only the orders we have
        Order[] tempOrders = new Order[DataSource.Config.s_nextOrderNumber-999];

        for (int i = 0; i < tempOrders.Length; i++)
        {
            tempOrders[i] = DataSource.orders[i];
        }

            return tempOrders;
    }

    public void UpdateOrders(Order current)
    {
        for(int i = 0; i < DataSource.Config.s_nextOrderNumber-999; i++)
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
