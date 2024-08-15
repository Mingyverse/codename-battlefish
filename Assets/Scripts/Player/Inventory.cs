using System;
using System.Collections.Generic;

namespace CodenameBattleFish.Player;

public class Inventory
{
    public int Money;
    public Dictionary<Item.Item, int> Items = new Dictionary<Item.Item, int>();

    public void AddItem(Item.Item item, int quantity)
    {
        Items.TryGetValue(item, out _);  // create default value
        Items[item] += quantity;
    }
    
    public bool UseItem(Item.Item item)
    {
        throw new NotImplementedException();
    }

    public bool DeductMoney(int quantity)
    {
        if (Money < quantity)
            return false;

        Money -= quantity;
        return true;
    }
}