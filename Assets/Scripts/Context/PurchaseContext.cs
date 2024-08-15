namespace CodenameBattleFish.Context;

public record PurchaseContext<T>(Player.Player Player, T Item, int Quantity);