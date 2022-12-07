namespace Dal;
using DO;



public class DalOrder
{ 
    /// <summary>
    /// Adds an instance to the main array
    /// </summary>
    /// <param name="current"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int InsertOrder(Order current)
    {
        //If we already have 100 orders then it will send an error
        if (DataSource.countOrders >= 100)
        {
            throw new Exception("Can't take more orders");
        }
        //take the instance and add it to the array
        int newID = DataSource.Config.NextOrderNumber;
        current.ID = newID;
        DataSource.orders[DataSource.countOrders++] = current;
        return newID;
    }

    /// <summary>
    /// deletes an instace from the main array
    /// </summary>
    /// <param name="currentID"></param>
    /// <exception cref="Exception"></exception>
    public void DeleteOrder(int currentID)
    {
        //If there are no orders it will send an error 
        if(DataSource.orders == null)
        {
            throw new Exception("There are no orders");
        }

        //delete the order from the array and update the rest of the array
        for(int i = 0; i < DataSource.countOrders; i++)
        {
            if (DataSource.orders[i].ID == currentID)
            {
                for (int j = i; j < DataSource.countOrders; j++)
                { 
                    DataSource.orders[j] = DataSource.orders[j + 1];
                }
                DataSource.countOrders--;
                return;
            }
        }
        throw new Exception("Order does not exist");
    }

    /// <summary>
    /// returns the instance with the provided identifier
    /// </summary>
    /// <param name="currentID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order ReadOrder(int currentID)
    {

        //find the order based on the identifier in the array
        for (int i = 0; i < (DataSource.countOrders); i++)
        {

            if (currentID == DataSource.orders[i].ID) 
                return DataSource.orders[i]; //return the product
        }
        throw new Exception("No order has that ID");    //if we cant find order throw an exception

    }

    /// <summary>
    /// Sends back the instances in the main array so it can be printed 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order[] ReadAllOrders()
    {
        //check that array is not empty
        if (DataSource.orders == null) throw new Exception("There are no orders!");
        //make a new array with only the orders we have
        Order[] tempOrders = new Order[DataSource.countOrders];

        for (int i = 0; i < tempOrders.Length; i++)
        {
            tempOrders[i] = DataSource.orders[i];
        }

            return tempOrders;
    }

    /// <summary>
    /// changes attributes of the instance
    /// </summary>
    /// <param name="current"></param>
    /// <exception cref="Exception"></exception>
    public void UpdateOrders(Order current)
    {
        for(int i = 0; i < DataSource.countOrders; i++)
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
