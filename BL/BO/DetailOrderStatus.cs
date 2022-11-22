using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO;
public class DetailOrderStatus
{
    public DateTime Date { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public override string ToString()
    {
        return $@" date: {Date}
        order status: {OrderStatus.ToString()}";
    }

}
