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

        int index;

        try      //get the index where the order we want to delete is
        {
            Order other = ReadOrder(currentID);
            index = other.ID;  //the place in the array where the order that we want to delete is
        }
        catch(Exception)    //if order does not exist send an error
        {
            throw new Exception("Order does not exist");
        }

        //delete the order from the array and update the rest of the array
        for(int i = index; i < DataSource.Config.s_nextOrderNumber-1000; i++)
        {
            DataSource.orders[i] = DataSource.orders[i+1];
        }

    }

    public Order ReadOrder(int currentID)
    {
        Console.WriteLine(DataSource.orders[0]);



        //find the order based on the identifier in the array
        for (int i = 0; i < (DataSource.Config.s_nextOrderNumber -1000); i++)
        {
            Console.WriteLine(DataSource.orders[i]);

            if (currentID == DataSource.orders[i].ID) 
                return DataSource.orders[i]; //return the product
        }
        throw new Exception("No order has that ID");    //if we cant find order throw an exception

    }

    public Order[] ReadAllOrders()
    {
        //check that array is not empty
        if (DataSource.orders == null) throw new Exception("There are no orders!");

        /*
        foreach(Order order in DataSource.orders)
        {
            order.ToString();
        }*/

        //make a new array with only the orders we have
        Order[] tempOrders = new Order[DataSource.Config.s_nextOrderNumber-1000];

        for (int i = 0; i < tempOrders.Length; i++)
        {
            tempOrders[i] = DataSource.orders[i];
        }

            return tempOrders;
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
