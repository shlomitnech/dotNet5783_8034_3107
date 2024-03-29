﻿using DalApi;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;

sealed internal class DalList : IDal 
{
    public IProduct Product => new DalProduct();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
    private DalList() { } 
    public static IDal Instance { get; } = new DalList();
}
 