using static DO.Enums;

namespace DO;

public struct Product
{
    #region properties
    public int Id { get; set; } = -1;
    public string Name { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; } = 0;
    public int InStock { get; set; }
    #endregion

    #region Product constructor
    public Product(string name, double price, Category category, int inStock)
     {
        Name = name;
        Price= price;
        Category = category;
        InStock = inStock;
     }
    #endregion

    /// <returns> the props of this object</returns>
    public override string ToString() {
        return ($@"
            Product ID={Id}: {Name}, 
            category - {Category.ToString()}
        	Price: {Price}
    	     Amount in stock: {InStock}
            ");
    }

}
