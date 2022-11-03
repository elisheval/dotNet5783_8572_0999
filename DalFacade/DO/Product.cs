using static DO.Enums;

namespace DO;

public struct Product
{

    public int Id { get; set; } = -1;
    public string Name { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }
    public int InStock { get; set; }

     Product(string name, double price, Category category, int inStock)
     {
        Name = name;
        Price= price;
        Category = category;
        InStock = inStock;
     }
    public override string ToString() {
        return ($@"
           Product ID={Id}: {Name}, 
            category - {Category}
        	Price: {Price}
    	     Amount in stock: {InStock}
            ");
    }

}
