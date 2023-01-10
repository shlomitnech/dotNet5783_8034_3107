using DalApi;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;

public class DalList : IDal //make sealed internal
{
    public IProduct Product => new DalProduct();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
    public DalList() { } //make private
    public static IDal Instance { get; } = new DalList();
}
