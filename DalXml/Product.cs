using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;

internal class Product : IProduct
{
    string productPath = @"Product.xml";

    #region private _convertFromXMLToProduct
    /// <summary>
    /// inner function that converts Xelement to DO.product element
    /// </summary>
    /// <param name="x">element to convert</param>
    /// <returns>element who convert</returns>
    private DO.Product? _convertFromXMLToProduct(XElement x)
    {
        Enum.TryParse<DO.Enums.Category>(x.Element("Category")!.Value, out DO.Enums.Category category);
        DO.Product product = new()
        {
            Id = int.Parse(x.Element("Id")!.Value.ToString()),
            Name = x.Element("Name")!.Value.ToString(),
            Price = Double.Parse(x.Element("Price")!.Value.ToString()),
            Category = category,
            InStock = int.Parse(x.Element("InStock")!.Value.ToString())
        };
        return (DO.Product?)product;
    }
    #endregion

    #region Add
    /// <summary>
    ///get the productList file from the xmlproduct file, add item to the list and save the file
    /// </summary>
    /// <param name="item">get product to add</param>
    /// <returns>the id of the new product</returns>
    /// <exception cref="DO.ItemAlresdyExsistException">if product already exist</exception>
    public int Add(DO.Product item)
    {
        try
        {
            XElement ProductData = XMLTools.LoadListFromXMLElement(productPath);
            XElement product = ProductData.Elements().FirstOrDefault(x => int.Parse(x.Element("Id")!.Value) == item.Id)!;
            if (product != null)
                throw new DO.ItemAlresdyExsistException("product already exsist");
            XElement productToAdd = new XElement("Product");
            productToAdd.Add(new XElement("Id", item.Id.ToString()),
                             new XElement("Name", item.Name!.ToString()),
                             new XElement("Price", item.Price.ToString()),
                             new XElement("Category", item.Category.ToString()),
                             new XElement("InStock", item.InStock.ToString()));
            ProductData.Add(productToAdd);
            XMLTools.SaveListToXMLElement(ProductData, productPath);
            return item.Id;
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Delete
    /// <param name="id">get id of product to delete</param>
    /// <summary>
    /// get the productList file from the xmlOroduct file, delete item from the list and save the file
    /// </summary>
    /// <exception cref="DO.NoFoundItemExceptions">if the id doesnt exist</exception>
    public void Delete(int id)
    {
        try
        {
            XElement ProductData = XMLTools.LoadListFromXMLElement(productPath);
            XElement product = ProductData.Elements().FirstOrDefault(x => int.Parse(x.Element("Id")!.Value) == id)!;
            if (product == null)
            {
                throw new NoFoundItemExceptions("no product found to delete with this ID");
            }
            product.Remove();
            XMLTools.SaveListToXMLElement(ProductData, productPath);
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw ex;
        }

    }
    #endregion

    #region GetAll
    /// <summary>
    /// get productList from the xmlProduct file  
    /// </summary>
    /// <param name="func">filter the productList by func if func!=null</param>
    /// <returns>the ProductList</returns>
    public IEnumerable<DO.Product?> GetAll(Predicate<DO.Product?>? func = null)
    {
        try
        {
            XElement ProductData = XMLTools.LoadListFromXMLElement(productPath);
            IEnumerable<DO.Product?> p = ProductData.Elements().Where(x => x != null).Select(x => _convertFromXMLToProduct(x)
            ).Where(x => func == null || func(x));
            IEnumerable<DO.Product?> tmpProducts = (IEnumerable<DO.Product?>)p.ToList();
            if (p == null) { throw new(); }
            return tmpProducts;
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw ex;
        }
    }
    #endregion

    #region GetByCondition
    /// <summary>
    /// get the productList from the xmlProduct file and looking for 1 product who stands in the condition
    /// </summary>
    /// <param name="func">filtering predicate</param>
    /// <returns>filtering productList</returns>
    /// <exception cref="NoFoundItemExceptions">if no one stands in the condition</exception>
    public DO.Product GetByCondition(Predicate<DO.Product?> func)
    {
        try
        {
            XElement ProductData = XMLTools.LoadListFromXMLElement(productPath);
            DO.Product? p = ProductData.Elements().Select(x => _convertFromXMLToProduct(x)).FirstOrDefault(x => func(x));
            if (p == null)
                throw new NoFoundItemExceptions("no found p item with this condition");
            return (DO.Product)p;
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Update
    /// <param name="item">get item to update</param>
    /// <summary>
    /// get the productList file from the xmlProduct file, update item in the list and save the file
    /// </summary>
    /// <exception cref="NoFoundItemExceptions">if item to update doesnt exist</exception>
    public void Update(DO.Product item)
    {
        try
        {
            XElement ProductData = XMLTools.LoadListFromXMLElement(productPath);
            XElement product = ProductData.Elements().FirstOrDefault(x => int.Parse(x.Element("Id")!.Value) == item.Id)!;
            if (product == null)
            {
                throw new NoFoundItemExceptions("no product found to update with this ID");
            }
            product.Element("Name")!.Value = item.Name!;
            product.Element("Price")!.Value = item.Price.ToString();
            product.Element("Category")!.Value = item.Category.ToString()!;
            product.Element("InStock")!.Value = item.InStock.ToString();
            XMLTools.SaveListToXMLElement(ProductData, productPath);
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw ex;
        }
    }
    #endregion
}
