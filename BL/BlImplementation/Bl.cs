using BlApi;

namespace BlImplementation;

internal class Bl:IBl
{
    public IOrder Order => new Order();
    public ICart Cart => new Cart();
    public IProduct Product => new Product();

}
