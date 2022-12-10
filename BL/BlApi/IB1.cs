using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;


public interface IB1
{
    public ICart Cart { get; }
    public IOrder Order { get; }
    public IOrder Product { get; }
}
