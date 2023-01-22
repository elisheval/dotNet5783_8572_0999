using BlApi;

namespace BlImplementation;

public class Bl:IBl
{
    public IOrder Order { get; } = new Order();
    public ICart Cart { get; } = new Cart();
    public IProduct Product { get; } = new Product();

}
