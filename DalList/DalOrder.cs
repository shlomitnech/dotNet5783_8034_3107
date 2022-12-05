namespace Dal;
using DO;



public class DalOrder
{
    DataSource ds = DataSource.s_Initialize();
    public void Insert()
    {
        if (DataSource.orders.Length == 100)
        {
            throw new Exception("Can't take more orders");
        }

        Order order = new Order();
        order.ID = DataSource.NextOrderNumber();

        DataSource.orders[DataSource.orders.Length + 1] = order;

    }

    public void Delete()
    {
        if(orders.Length == 0)
    }
}
