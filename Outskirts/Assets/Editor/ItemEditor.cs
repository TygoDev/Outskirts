using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Item item = (Item)target;

        // Draw default fields
        item.itemName = EditorGUILayout.TextField("Item Name", item.itemName);
        item.description = EditorGUILayout.TextArea(item.description);
        item.icon = (Sprite)EditorGUILayout.ObjectField("Icon", item.icon, typeof(Sprite), false);
        item.itemType = (ItemType)EditorGUILayout.EnumPopup("Item Type", item.itemType);

        if (item.itemType == ItemType.Weapon || item.itemType == ItemType.Equipment)
            item.stackable = EditorGUILayout.Toggle("Stackable", false);
        else
            item.stackable = EditorGUILayout.Toggle("Stackable", item.stackable);

        if(item.itemType == ItemType.Quest)
            item.sellable = EditorGUILayout.Toggle("Sellable", false);
        else
            item.sellable = EditorGUILayout.Toggle("Sellable", item.sellable);

        // Conditional field: only show attackDamage if itemType is Equipment
        if (item.itemType == ItemType.Weapon)
        {
            item.attackDamage = EditorGUILayout.IntField("Attack Damage", item.attackDamage);
        }

        // Value and gameplay fields
        item.sellPrice = EditorGUILayout.IntField("Sell Price", item.sellPrice);
        item.buyPrice = EditorGUILayout.IntField("Buy Price", item.buyPrice);

        // Save changes
        if (GUI.changed)
        {
            EditorUtility.SetDirty(item);
        }
    }
}
