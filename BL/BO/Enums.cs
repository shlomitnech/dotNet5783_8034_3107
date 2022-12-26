using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Enums
{
    public enum Category { Bread, Dips, MainCourse, Sides, Desserts, Other };
    public enum OrderStatus {JustPlaced, Processing, Shipped, Arrived, Recieved };
    public enum Action {Add, Delete, Update, GetList };
    public enum Type {Product, Order, Cart };

}

