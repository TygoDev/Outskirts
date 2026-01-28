using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public List<Recipe> recipes = new List<Recipe>();

    [SerializeField] private Inventory inventory;

    public void Craft(Recipe recipe)
    {
        foreach (InventoryEntry inventoryEntry in recipe.itemsNeeded)
        {
            if (!inventory.HasItem(inventoryEntry.item, inventoryEntry.amount))
            {
                return;
            }
        }

        foreach (InventoryEntry inventoryEntry in recipe.itemsNeeded)
        {
            inventory.RemoveItem(inventoryEntry.item, inventoryEntry.amount);
        }

        inventory.AddItem(recipe.resultItem);
    }
}

