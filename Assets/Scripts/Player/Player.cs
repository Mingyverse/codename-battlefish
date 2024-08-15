using System.Collections.Generic;
using CodenameBattleFish.Fish;
using CodenameBattleFish.Habitat;

namespace CodenameBattleFish.Player;

public class Player
{
    public string Name;

    public List<BattleFish> OwnedFishes = new List<BattleFish>();
    public Inventory Inventory;
    public List<Aquarium> OwnedAquariums = new List<Aquarium>();

    public void AddItem(Item.Item item, int quantity)
    {
        Inventory.AddItem(item, quantity);
    }

    public bool DeductMoney(int quantity)
    {
        return Inventory.DeductMoney(quantity);
    }
}