using static DO.Enums;
using System.Collections.Generic;

namespace DO;

public struct Product
{
    #region properties
    public int Id { get; set; } = -1;
    public string Name { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }
    public int InStock { get; set; }
    #endregion

    #region Product constructor
    public Product(int id ,string name, double price, Category category, int inStock)
     {
        Id = id;
        Name = name;
        Price= price;
        Category = category;
        InStock = inStock;
     }
    #endregion

    #region ToString
    /// <returns> the props of this object</returns>
    public override string ToString() {
        return (
      $@"Product ID={Id}: {Name}, 
         category - {Category.ToString()}
         Price: {Price}
    	 Amount in stock: {InStock}");
    }
    #endregion
}
