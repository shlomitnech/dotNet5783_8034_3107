using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;


public interface IOrder
{
    //The manager functions
    public IEnumerable<OrderForList> GetAllOrderForLists(); //returns the product list (for the manager to see)
    public Order ManagerOrder(int id); //returns a BO product of DO product with id
    public void AddOrder(Order ord); //gets a BO product, and adds it to DO product
    public void DeleteOrder(int id); //check in every order that DO product is deleted 
    public void UpdateOrder(Order ord); //Get BO product, and update the DO product

    //The Customer Functions
    public IEnumerable<OrderItem?> GetCatalog(); //Get the product list of DO and return the ProductItem list of BO


}
