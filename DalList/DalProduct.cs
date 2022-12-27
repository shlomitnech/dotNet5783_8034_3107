using DO;
using System.Security.Cryptography;
using System.Threading;
using DalApi;
namespace Dal;

public class DalProduct : IProduct
{
    /// <summary>
    /// adding an instance to the main array
    /// </summary>
    /// <param name="current"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(Product current) //this is out of order
    {

        //If we already have 50 products then it will send an error
        if (DataSource.products.Count >= 50)
        {
            throw new EntityNotFound("Can't take more orders");
        }
        /*
        int index = DataSource.products.FindIndex(x => x.ID == current.ID);
        //Check if the product already exists
        if (index != -1)
            throw new Exception("Product already exists\n");
        */
        //take the instance and add it to the list
        int newID = DataSource.Config.NextProductNumber;
        current.ID = newID;
        DataSource.products.Add(current);
        return newID;
    }
    /// <summary>
    /// returns all instances in the main array to be printed
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<Product> GetAll() 
    {  
        if (DataSource.products != null)
        {
            return DataSource.products;     
        }
            throw new Exception("There are no orders!");
}
public Product GetByFilter(Func<Product, bool>? filter)
    {
        foreach (Product prod in DataSource.products)
        {
            //  if (ord.IsDeleted == false && filter(o))
            {
                return prod;
            }
        }
        throw new Exception("Does not exist\n");
    }


/// <summary>
/// returns the instance based on the identifier provided by the user so it can be printed
/// </summary>
/// <param name="currentID"></param>
/// <returns></returns>
/// <exception cref="Exception"></exception>
    public Product Get(int currentID)
    {
        Product thisProd = DataSource.products.Find(x=> x.ID == currentID);
        if (thisProd.ID != currentID)
           throw new Exception("No product has that ID");    //if product is not found
        return thisProd;
    }

    /// <summary>
    /// changes attributes of the instance
    /// </summary>
    /// <param name="prod"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product prod)
    {
        int index = DataSource.products.FindIndex(x => x.ID == prod.ID);
        if (index == -1) //item doesn't exist
            throw new EntityNotFound("The product does not exist");
        DataSource.products[index] = prod;
    }

    /// <summary>
    /// removes the instance from the main array
    /// </summary>
    /// <param name="currentID"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int currentID)
    {
        //delete the product from the array and update the rest of the array

        int index = DataSource.products.FindIndex(x => x.ID == currentID); // Is this correct?

        if (index == -1) // Item doesn't exist
            throw new Exception("Order does not exist");

        DataSource.products.RemoveAt(index);
    }

    public IEnumerable<Product> GetAll(Func<Product, bool>? filter = null)
    {
        throw new NotImplementedException();
    }
}