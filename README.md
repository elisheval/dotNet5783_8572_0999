hello;
# dotNet5783_8572_0999dotNet5783_8572_0999
Stage0 final commit


bunos:
stage1: we used tryparse and parse method in the program when we get input from the user.
stage2: this is the method to update, we tryed it and its work:
    #region UpdateOrder bonus
    public void UpdateOrder(int orderId, int productId, int amount)
    {
        try
        {
            if (orderId <= 0)
                throw new BO.InvalidValueException("invalid order id");
            DO.Order dalOrder = new DO.Order();
            dalOrder = _dal.Order.GetByCondition(x=>x!=null&&x?.ID==orderId);
            if (dalOrder.ShipDate !=null)//if the order sent the manager cannot change it
                throw new BO.NoAccessToSentOrder("the order already sent, you can't change the date");
            if (amount < 0)//invalid
                throw new BO.InvalidValueException("invalid amount of product");
            if (amount == 0)//This is a sign that the manager wants to delete
            {
                DO.Product p = _dal.Product.GetByCondition(x=>x!=null&&x?.Id==productId);
                DO.OrderItem orderitem = _dal.OrderItem.GetByCondition(x=>x!=null&&x?.ProductId==productId &&x?.OrderId==orderId);
                p.InStock += orderitem.Amount;
                _dal.OrderItem.Delete(orderitem.Id);
                _dal.Product.Update(p);
            }
            else
            {
                IEnumerable<DO.OrderItem?> OrderItems = _dal.OrderItem.GetAll();
                bool flag = false;
                foreach (var oi in OrderItems)//look for the order item,
                {
                    if (oi!=null){
                        if (oi?.ProductId == productId && oi?.OrderId == orderId)//if the product exist- change the amount to be the getter amount
                        {
                            flag = true;
                            if (oi?.Amount != amount)
                            {
                                DO.Product p = _dal.Product.GetByCondition(x=> x != null && x?.Id == productId);
                                DO.OrderItem OItoChange = new()
                                {
                                    OrderId = oi?.OrderId??0,
                                    Id = oi?.Id??0,
                                    Price = oi?.Price??0,
                                    ProductId = oi?.ProductId??0
                                };
                                if (p.InStock < amount)
                                {
                                    OItoChange.Amount = oi?.Amount??0 + p.InStock;
                                    p.InStock = 0;
                                }
                                else
                                {
                                    OItoChange.Amount = amount;
                                    p.InStock -= amount - oi?.Amount??0;
                                }
                                _dal.Product.Update(p);
                                _dal.OrderItem.Update(OItoChange);
                            }
                        }
                    }
                }
                if (!flag)//if not exist enter the product to the order
                {
                    DO.Product p = _dal.Product.GetByCondition(x=>x != null && x?.Id == productId);
                    if (p.InStock < amount)
                    {
                        p.InStock = 0;
                        amount = p.InStock;
                    }
                    else
                    {
                        p.InStock -= amount;
                    }
                    DO.OrderItem d1 = new() { ProductId=productId,OrderId=orderId,Price= p.Price,Amount=amount };
                    _dal.OrderItem.Add(d1);
                    _dal.Product.Update(p);

                }
            }
        }
        catch (DO.NoFoundItemExceptions exe)
        {
            throw new BO.NoFoundItemExceptions(exe.Message, exe);
        }

    }
    #endregion

    stage3:
    genery function to print object with out getting specific type of the object

    #region print object
    private static void _print<T>(T obj)
    {
        var type = obj?.GetType();
        if (type != null)
            foreach (var pInfo in type.GetProperties())
            {
                Console.Write(pInfo.Name + ": ");
                Console.WriteLine(pInfo.GetValue(obj, null));

            }
    }
    and we use regular expretion for the input from the client.
    #endregion

    stage4:
    we used lazy initilize with nested class,
    so that,instantiation is triggered by the first reference
    to the static member of the nested class, which only occurs in Instance.
    We also used a locking function, 
    locking is used so that only one thread can enter valid code snippets at a time.
    we can also use Lazy class for it and in the default itis also safetly,
    but we used the nested class because its was in the presentation of the material
