


namespace DO;

public struct Enums
{
  public enum Category
    {
        percussions,
        keyboards,
        exhalation,
        strings,
        additional
    }

    public enum Entity
    {
        exit,
        Product,
        Order,
        OrderItem
    }
    public enum CRUD{
        exit,
        Create,
        Read,
        ReadAll,
        Delete,
        Update,
        ReadByOrderAndProductIds,
        ReadByOrderId
    }
}
