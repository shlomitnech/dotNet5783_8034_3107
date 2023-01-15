using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public Enums.OrderStatus Status { get; set; }
    public List<Tuple<DateTime?, string>>? Tracking { set; get; }


    public override string ToString() => $@"
    The order's ID is: {ID}
    The order's status is: {Status}

";
}
