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

        DataSource.products.Add(current);
        return current.ID;
    }
    /// <summary>
    /// returns all instances in the main array to be printed
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {
        if (filter == null)//select whole list
        {
            return from prod in DataSource.products
                   where prod != null
                   select prod;
        }
        return from myProd in DataSource.products//select with filter
               where myProd != null && filter(myProd)
               select myProd;

    }

    public Product GetByFilter(Func<Product?, bool>? filter)
    {
        if (filter == null)
        {
            throw new ArgumentNullException(nameof(filter));//filter is null
        }
        foreach (Product? product in DataSource.products)
        {
            if (product != null && filter(product))
            {
                return (Product)product;
            }
        }
        throw new EntityNotFound();
    }

    /// <summary>
    /// returns the instance based on the identifier provided by the user so it can be printed
    /// </summary>
    /// <param name="currentID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product? Get(int currentID)
    {
        DO.Product? prod = DataSource.products.Find(x => x?.ID == currentID); // find an order with a matching ID
        if (prod == null)
        {
            throw new EntityNotFound();
        }
        return prod;
    }

    /// <summary>
    /// changes attributes of the instance
    /// </summary>
    /// <param name="prod"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product? prod)
    {
        int index = DataSource.products.FindIndex(x => x?.ID == prod?.ID);
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
        int index = -1;
        foreach (DO.Product? prod in DataSource.products)
        {
            if (prod?.ID == currentID)
            {
                index = DataSource.products.IndexOf(prod); // save the index of the product with the matching ID#
                break;
            }
        }
        DO.Product DelProd = (Product)DataSource.products[index]!; // save the product in the found index
        DataSource.products.Remove(DelProd); // remove the product
  

    }

}

 