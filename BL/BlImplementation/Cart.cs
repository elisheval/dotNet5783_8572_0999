using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Cart:ICart
{
    private IDal Dal = new DalList();
}
