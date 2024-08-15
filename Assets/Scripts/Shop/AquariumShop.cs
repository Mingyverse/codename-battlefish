using CodenameBattleFish.Context;
using CodenameBattleFish.Item;

namespace System.Runtime.CompilerServices.Shop;

public class AquariumShop : ItemShop<AquariumItem>
{
    public bool PurchaseItem(PurchaseContext<AquariumShop> context)
    {
        throw new NotImplementedException();
    }
}