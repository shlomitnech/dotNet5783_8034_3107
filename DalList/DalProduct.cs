using DO;
using System.Security.Cryptography;
using System.Threading;
using DalApi;
using System.Linq;
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
        int _ID = current.ID;
        DO.Product? prevProd = DataSource.products.Find(x => x?.ID == _ID); // find a product with a matching ID
        if (prevProd != null) //no ID already exists
        {
            throw new Duplicates("Product already exists");
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
        foreach (Product? prod in DataSource.products)
        {
            if (prod != null && filter(prod))
            {
                return (Product)prod;
            }
        }
        throw new EntityNotFound("Product does not exist");
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
    public void Update(Product prod)
    {
        int _ID = prod.ID;
        DO.Product? prevProd = DataSource.products.Find(x => x?.ID == _ID); // find a product with a matching ID
        if (prevProd == null)
        { 
            throw new EntityNotFound();
        }
        int index = DataSource.products.IndexOf(prevProd); // save the index of the product with matching ID
        DataSource.products[index] = prod; // add the updated product to the found location of the old product

    }

    /// <summary>
    /// removes the instance from the main array
    /// </summary>
    /// <param name="currentID"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int currentID)
    {
        //int index = -1;
        /*foreach (DO.Product? prod in DataSource.products)
        {
            if (prod?.ID == currentID)
            {
                index = DataSource.products.IndexOf(prod); // save the index of the product with the matching ID#
                break;
            }
        }

       var delProd = from prod in DataSource.products
                     where prod != null && prod.Value.ID == currentID
                     let index = DataSource.products.IndexOf(prod)
                     select (Product)DataSource.products[index]!;

        //{
        //   DelProd = (Product)DataSource.products[index]!
        //}).Remove(DelProd);
        //DO.Product DelProd = (Product)DataSource.products[index]!; // save the product in the found index
       */
        DO.Product? deleprod = DataSource.products.Single(x => x?.ID == currentID);
        DataSource.products.Remove(deleprod); // remove the product

        
    }

   

}

 