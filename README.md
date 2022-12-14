hello;
# dotNet5783_8572_0999dotNet5783_8572_0999
Stage0 final commit


bunos:
stage1: we used tryparse and parse method in the program when we get input from the user.
stage2: this is the method to update, we tryed it and its work:
    #region UpdateOrder bonus
    /// <param name="orderId">get order of id</param>
    /// <param name="productId">get id of product to add, delete or change amount</param>
    /// <param name="amount">get amount</param>
    /// <summary>
    /// the manager can add, delete or change amount of product in confirm order
    /// </summary>
    /// <exception cref="BO.InvalidValueException"></exception>
    /// <exception cref="BO.NoAccessToSentOrder"></exception>
    /// <exception cref="BO.NoFoundItemExceptions"></exception>
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
