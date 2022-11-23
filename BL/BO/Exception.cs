
namespace BO;

public class InvalidValueException:Exception
{
    public InvalidValueException(string message) : base(message) { }
}
public class ProductInOrderException : Exception
{
    public ProductInOrderException(string message) : base(message) { }
}
public class ProductOutOfStockException : Exception
{
    public ProductOutOfStockException(string message):base(message){ }
}

public class ProductNotInCartException : Exception
{
    public ProductNotInCartException(string message) : base(message) { }
}

public class NoFoundItemExceptions : Exception
{
    public NoFoundItemExceptions(string massege,Exception ex){ }
    public NoFoundItemExceptions(string massege) : base(massege) { }
}

public class ItemAlresdyExsistException : Exception { 
    public ItemAlresdyExsistException(string massege, Exception ex) { }

}
