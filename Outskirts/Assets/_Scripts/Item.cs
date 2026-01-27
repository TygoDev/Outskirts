using UnityEngine;

public enum ItemType
{
    Resource,
    Consumable,
    Equipment,
    Weapon,
    Quest,
    Misc
}

[CreateAssetMenu(
    fileName = "NewItem",
    menuName = "Items/Item",
    order = 1)]
public class Item : ScriptableObject
{
    [Header("Basic Info")]
    public string itemName;
    [TextArea]
    public string description;
    public Sprite icon;
    public ItemType itemType;
    public bool stackable = true;
    public bool sellable = true;

    [Header("Item stats")]
    public int attackDamage;

    [Header("Value")]
    public int sellPrice;
    public int buyPrice;

    /// <summary>
    /// Called when the item is used.
    /// Override or extend this in derived item types.
    /// </summary>
    public virtual void Use()
    {
        Debug.Log($"Used item: {itemName}");
    }
}
