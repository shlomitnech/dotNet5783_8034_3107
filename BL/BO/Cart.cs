﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set;}
    public string? CustomerAddress { get; set;}
    public List<BO.OrderItem?>? Items { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
	Customer's Name is: {CustomerName}
	Customer's email is: {CustomerEmail}
	Customer's address is: {CustomerAddress}
    The order's items' are: {Items}
    The order's total price is: {TotalPrice}
";
}

