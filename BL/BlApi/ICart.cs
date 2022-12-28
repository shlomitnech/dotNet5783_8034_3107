using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;

public interface ICart
{
    public Cart AddToCart(Cart cart, int id, int amount); //check if the product is in the cart, if not add it from DO product if it is in stock
    public Cart UpdateCart(Cart cart, int id, int newAmount); //update the cart to have more or less products and the total price
    public void MakeOrder(Cart cart, string CustomerName, string Customeremail, string customerAddress); //approve the items in the cart and make the real order
    public List<string> GetItemNames(Cart cart);
    public void DeleteCart(Cart cart);

}
