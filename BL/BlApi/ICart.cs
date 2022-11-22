using BO;

namespace BlApi;

public interface ICart
{
    public Cart AddProductToCart(int productId, Cart myCart);
    public Cart UpdateAmountOfProductInCart(int productId, Cart myCart,int newAmount);
    public void OrderConfirmation(Cart myCart, string customerName, string customerEmail, string customerAddress);

}
