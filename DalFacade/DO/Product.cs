using static DO.Enums;

namespace DO;

public struct Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }
    public int InStock { get; set; }

    public override string ToString() {
        return ($@"
           Product ID={Id}: {Name}, 
            category - {Category}
        	Price: {Price}
    	     Amount in stock: {InStock}
            ");
    }

}
