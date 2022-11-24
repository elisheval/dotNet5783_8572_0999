
namespace BO;

//Exception classes that inherit from Exception class

#region InvalidValueException
/// <summary>
/// This exception is made when there is an invalid input
/// </summary>
public class InvalidValueException:Exception
{
    public InvalidValueException(string message, Exception inner)
       : base(message, inner) { }

    public InvalidValueException(string message) : base(message) { }
    public override string ToString()
    {
        return ($@" exception name: InvalidValueException,
                    exception message: {Message}
                     ");
    }
}
#endregion

#region ProductInOrderException
/// <summary>
/// This exception is made when there is an attempt to delete a product
/// and it cannot be done because it exists in one of the orders
/// </summary>
public class ProductInOrderException : Exception
{
    public ProductInOrderException(string message) : base(message) { }
    public override string ToString()
    {
        return ($@" exception name: ProductInOrderException,
                    exception message: {Message}");
    }
}
#endregion

#region ProductOutOfStockException
/// <summary>
/// This exception is made when there is not enough of a certain product in stock
/// </summary>
public class ProductOutOfStockException : Exception
{
    public ProductOutOfStockException(string message):base(message){ }
    public override string ToString()
    {
        return ($@" exception name: ProductOutOfStockException,
                    exception message: {Message}");
    }
}
#endregion

#region ProductNotInCartException
/// <summary>
/// This exception is made when a certain product is not available in the cart
/// </summary>
public class ProductNotInCartException : Exception
{
    public ProductNotInCartException(string message) : base(message) { }
    public override string ToString()
    {
        return ($@" exception name: ProductNotInCartException,
                    exception message: {Message}");
    }
}
#endregion

#region NoFoundItemExceptions
/// <summary>
/// This exception is made when there is no item with the received ID number
/// </summary>
public class NoFoundItemExceptions : Exception
{
    public NoFoundItemExceptions(string message, Exception inner)
    : base(message, inner) { }

    public NoFoundItemExceptions(string massege) : base(massege) { }
    public override string ToString()
    {
        return ($@" exception name: NoFoundItemExceptions,
                    exception message: {Message}");
    }
}
#endregion

#region ItemAlresdyExsistException
public class ItemAlresdyExsistException : Exception {
    public ItemAlresdyExsistException(string message, Exception inner)
       : base(message, inner) { }
    public override string ToString()
    {
        return ($@" exception name: ItemAlresdyExsistException,
                    exception message: {Message}");
    }
}
#endregion

#region OrderAlreadySend
/// <summary>
/// This exception is made when the status of a sent order is updated and it has already been sent
/// </summary>
public class OrderAlreadySend : Exception
{
    public OrderAlreadySend(string massege) : base(massege) { }
    public override string ToString()
    {
        return ($@" exception name: OrderAlreadySend,
                    exception message: {Message}
                     ");
    }
}
#endregion

#region OrderAlreadyDelivery
/// <summary>
/// This exception is made when updating the status of an order that has already been delivered
/// </summary>
public class OrderAlreadyDelivery : Exception
{
    public OrderAlreadyDelivery(string massege) : base(massege) { }
    public override string ToString()
    {
        return ($@" exception name: OrderAlreadyDelivery,
                    exception message: {Message}");
    }

}
#endregion