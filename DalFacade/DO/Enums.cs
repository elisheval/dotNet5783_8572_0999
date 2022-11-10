


namespace DO;

public struct Enums
{
    #region category
    /// <summary>
    /// The item's categories in the store
    /// </summary>
    public enum Category
    {
        percussions,//כלי הקשה
        keyboards,//כלי מקלדת
        exhalation,//כלי נשיפה
        strings,//כלי מיתר
        additional//נלווים 
    }
    #endregion

    #region entity
    /// <summary>
    /// The Entity options to navigate
    /// </summary>
    public enum Entity
    {
        exit,
        Product,
        Order,
        OrderItem
    }
    #endregion

    #region crud
    /// <summary>
    /// access data methods 
    /// </summary>
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
    #endregion
}
