using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using DO;
namespace BlImplementation;

internal class Cart : ICart
{
    readonly private static IDal Dos = DalApi.Kitchen.Get();
    public BO.Cart AddToCart(BO.Cart cart, int id) //check if the product is in the cart, if not add it from DO product if it is in stock
    {
        int index = cart.Items.FindIndex(x => x != null && x.ID == id);

        DO.Product p = new DO.Product();
        p = Dos.Product.Get(id);

        if (p.inStock < 1) throw new Exception("Product in unavailable");

        if (index != -1)
        {
            cart.Items[index].Amount++;
            cart.Items[index].Price += cart.TotalPrice;
        }

        BO.OrderItem item = new BO.OrderItem()
        {
            ID = id,
            Price = p?.Price,
            Amount = 1,
            ProductID = p.ID
        };

        cart.Items.Add(item);
        cart.TotalPrice += item.Price;

        return cart;
    }
    public void UpdateCart(Cart cart, int id, int newAmount) //update the cart to have more or less products and the total price
    {

    }
    public void MakeOrder(Cart cart, string CustomerName, string Customeremail, string customerAddress) //approve the items in the cart and make the real order
    {

    }
}
