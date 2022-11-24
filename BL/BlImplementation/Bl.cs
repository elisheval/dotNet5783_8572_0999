using BlApi;


namespace BlImplementation;

public class Bl:IBl
{
    public IOrder Order => new Order();
    public ICart Cart => new Cart();
    public IProduct Product => new Product();

}
