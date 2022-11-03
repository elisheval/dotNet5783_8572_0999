﻿

namespace DO;

public struct OrderItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }

    public override string ToString() => $@"
        orderItem:
        ProductId:{ProductId}, 
        OrderId - {OrderId}
    	Price: {Price}
        Amount:{Amount}
         ";
}
