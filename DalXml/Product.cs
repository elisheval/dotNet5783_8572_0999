using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;

using System.Globalization;
using System.Xml.Linq;

internal class Product : IProduct
{
    string productPath = @"Product.xml";
    public int Add(DO.Product item)
    {
        XElement ProductData = XMLTools.LoadListFromXMLElement(productPath);
        XElement product = ProductData.Elements().FirstOrDefault(x => int.Parse(x.Element("Id").Value) == item.Id);
        if (product != null)
            throw new DO.ItemAlresdyExsistException("product already exsist");
        XElement productToAdd = new XElement("Product");
        productToAdd.Add(new XElement("Id", item.Id.ToString()),
                         new XElement("Name", item.Name.ToString()),
                         new XElement("Price", item.Name.ToString()),
                         new XElement("Category", item.Category.ToString()),
                         new XElement("InStock", item.InStock.ToString()));
        ProductData.Add(productToAdd);
        XMLTools.SaveListToXMLElement(ProductData, productPath);
        return item.Id;
    }

    public void Delete(int id)
    {
        try
        {
            XElement ProductData = XMLTools.LoadListFromXMLElement(productPath);
            XElement product = ProductData.Elements().FirstOrDefault(x => int.Parse(x.Element("Id").Value) == id);
            if (product != null)
            {
                product.Remove();
                XMLTools.SaveListToXMLElement(ProductData, productPath);
            }
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw new();
        }
        catch (DO.NoFoundItemExceptions ex)
        {
            throw new();
        }
    }

    public IEnumerable<DO.Product?> GetAll(Predicate<DO.Product?>? func = null)
    {
        try
        {
            XElement ProductData = XMLTools.LoadListFromXMLElement(productPath);
            IEnumerable<DO.Product> p = ProductData.Elements().Where(x => x != null).Select(x => new DO.Product()
            {
                Id = int.Parse(x.Element("Id").Value),
                Name = x.Element("Name").Value,
                Price = Double.Parse(x.Element("Price").Value),
                Category = (DO.Enums.Category?)(int.Parse)(x.Element("Category").Value),
                InStock = int.Parse(x.Element("Instock").Value)
            }).Where(x => func == null || func(x));
            IEnumerable<DO.Product?> tmpProducts = (IEnumerable<DO.Product?>)p.ToList();
            if (p == null) { throw new(); }
            return tmpProducts;
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw new();
        }
    }

    private DO.Product? _convertFromXMLToProduct(XElement x)
    {
        DO.Product product = new()
        {
            Id = int.Parse(x.Element("Id").Value.ToString()),
            Name = x.Element("Name").Value.ToString(),
            Price = Double.Parse(x.Element("Price").Value.ToString()),
            //Category = (DO.Enums.Category?)(int.Parse)(x.Element("Category").Value),
            InStock = int.Parse(x.Element("InStock").Value.ToString())
        };
        return (DO.Product?)product;
    }

    public DO.Product GetByCondition(Predicate<DO.Product?> func)
    {
        try
        {
            XElement ProductData = XMLTools.LoadListFromXMLElement(productPath);
            DO.Product? p = ProductData.Elements().Select(x => _convertFromXMLToProduct(x)).FirstOrDefault(x => func(x));
            if (p == null)
                throw new DO.NoFoundItemExceptions("not found item with this id");
            return (DO.Product)p;
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw new();
        }
    }

    public void Update(DO.Product item)
    {
        try
        {
            XElement ProductData = XMLTools.LoadListFromXMLElement(productPath);
            XElement product = ProductData.Elements().FirstOrDefault(x => int.Parse(x.Element("Id").Value) == item.Id);
            if (product != null)
            {
                product.Element("Name").Value = item.Name;
                product.Element("Price").Value = item.Name;
                product.Element("Category").Value = item.Name;
                product.Element("InStock").Value = item.Name;
            }
            XMLTools.SaveListToXMLElement(ProductData, productPath);
        }
        catch (DO.XMLFileLoadCreateException)
        {
            throw new();
        }
    }
}
