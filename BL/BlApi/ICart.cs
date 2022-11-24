using BO;

namespace BlApi;
public interface ICart
{
    /// <summary>
    /// Adding Product To Cart
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="myCart"></param>
    /// <returns>the update cart after adding the product</returns>
    public Cart AddProductToCart(int productId, Cart myCart);
    /// <summary>
    /// Update Amount Of Product In Cart
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="myCart"></param>
    /// <param name="newAmount"></param>
    /// <returns>the update cart after updating amount of product in cart product</returns>
    public Cart UpdateAmountOfProductInCart(int productId, Cart myCart,int newAmount);
    /// <summary>
    /// Order Confirmation and updating the order to the data layer
    /// </summary>
    /// <param name="myCart"></param>
    /// <param name="customerName"></param>
    /// <param name="customerEmail"></param>
    /// <param name="customerAddress"></param>
    public void OrderConfirmation(Cart myCart, string customerName, string customerEmail, string customerAddress);
}
