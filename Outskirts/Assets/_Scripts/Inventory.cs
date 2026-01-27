using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores items and their total amounts.
/// Stackable items share a slot.
/// Unstackable items are counted internally but shown separately in UI.
/// </summary>
public class Inventory : MonoBehaviour
{
    [Serializable]
    public class InventoryEntry
    {
        public Item item;
        public long amount;
    }


    [SerializeField] private int inventoryBalance = 0;
    public int InventoryBalance => inventoryBalance;

    [Header("Starting Inventory (Editor Only)")]
    [SerializeField] private List<InventoryEntry> startingItems = new();

    private readonly Dictionary<Item, InventoryEntry> inventory = new();
    public event Action OnInventoryChanged;

    private void Awake()
    {
        inventory.Clear();

        // Initialize from inspector list (merge duplicates safely)
        foreach (var entry in startingItems)
        {
            if (entry.item == null || entry.amount <= 0)
                continue;

            if (inventory.TryGetValue(entry.item, out var existing))
            {
                existing.amount += entry.amount;
            }
            else
            {
                inventory[entry.item] = new InventoryEntry
                {
                    item = entry.item,
                    amount = entry.amount
                };
            }
        }

        OnInventoryChanged?.Invoke();
    }

    public void ModifyBalance(int amount)
    {
        inventoryBalance += amount;
        OnInventoryChanged?.Invoke();
    }

    public void SetBalance(int amount)
    {
        inventoryBalance = amount;
        OnInventoryChanged?.Invoke();
    }

    public void AddItem(Item item, long amount = 1)
    {
        if (item == null || amount <= 0)
            return;

        if (inventory.TryGetValue(item, out var entry))
        {
            entry.amount += amount;
        }
        else
        {
            inventory[item] = new InventoryEntry
            {
                item = item,
                amount = amount
            };
        }

        OnInventoryChanged?.Invoke();
    }

    public bool RemoveItem(Item item, long amount = 1)
    {
        if (!inventory.TryGetValue(item, out var entry))
            return false;

        if (entry.amount < amount)
            return false;

        entry.amount -= amount;

        if (entry.amount <= 0)
            inventory.Remove(item);

        OnInventoryChanged?.Invoke();
        return true;
    }

    public bool HasItem(Item item, long amount = 1)
    {
        return inventory.TryGetValue(item, out var entry) && entry.amount >= amount;
    }

    public IReadOnlyCollection<InventoryEntry> GetAllItems()
    {
        return inventory.Values;
    }
}
