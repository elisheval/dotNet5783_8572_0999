
using static BO.Enums;

namespace BO;
public class Product
{
    #region properties
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }
    public int InStock { get; set; }
    #endregion

    #region ToString
    /// <returns> the props of this object</returns>
    public override string ToString() =>
    $@" product id: {Id},
        name: {Name},
        price: {Price},
        category: {Category},
        in stock: {InStock}";
    #endregion
}
