using System.Collections.Generic;
using CodenameBattleFish.Context;
using CodenameBattleFish.Item;

namespace System.Runtime.CompilerServices.Shop;

public abstract class ItemShop<T> where T: Item
{
    public List<T> ItemsSold = new List<T>();

    public bool PurchaseItem(PurchaseContext<T> context)
    {
        if (context.Player.Inventory.Money < context.Item.Price * context.Quantity)
            return false;
        
        context.Player.AddItem(context.Item, context.Quantity);
        context.Player.DeductMoney(context.Item.Price * context.Quantity);
        return true;
    }
}