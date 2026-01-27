using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "NewRecipe",
    menuName = "Recipes/Recipe",
    order = 2)]
public class Recipe : ScriptableObject
{
    public Item resultItem;
    public List<InventoryEntry> itemsNeeded = new List<InventoryEntry>();
}

[Serializable]
public class InventoryEntry
{
    public Item item;
    public long amount;
}
