using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;


public interface IBl
{
    public ICart Cart { get; }
    public IOrder Order { get; }
    public IProduct Product { get; }
}
