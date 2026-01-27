using System;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Inventory shopInventory = null;
    [SerializeField] private Inventory otherInventory = null;

    public void MoveItem(Item pItem, Inventory origin)
    {
        if (origin == shopInventory)
        {
            if (otherInventory.InventoryBalance >= pItem.buyPrice)
            {
                otherInventory.AddItem(pItem, 1);
                shopInventory.RemoveItem(pItem, 1);

                otherInventory.ModifyBalance(-pItem.buyPrice);
                shopInventory.ModifyBalance(pItem.buyPrice);
            }
        }
        else
        {
            if (shopInventory.InventoryBalance >= pItem.sellPrice)
            {
                shopInventory.AddItem(pItem, 1);
                otherInventory.RemoveItem(pItem, 1);

                shopInventory.ModifyBalance(-pItem.sellPrice);
                otherInventory.ModifyBalance(pItem.sellPrice);
            }          
        }
    }
}

