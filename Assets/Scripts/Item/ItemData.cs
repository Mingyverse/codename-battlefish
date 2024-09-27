using UnityEngine;

namespace Item;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Codename Battlefish/Item Data", order = 0)]
public class ItemData : ScriptableObject
{
    public string id = "";
    public string itemName = "";
    public string description = "";
    public float price;
    public Sprite previewSprite = default!;
}