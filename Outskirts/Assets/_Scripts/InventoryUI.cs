using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays inventory contents.
/// Stackable items use one slot.
/// Unstackable items use one slot per item.
/// </summary>
public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private TMP_Text inventoryBalance;

    private readonly List<InventorySlotUI> slots = new();

    private void OnEnable()
    {
        if (inventory != null)
            inventory.OnInventoryChanged += Refresh;

        Refresh();
    }

    private void OnDisable()
    {
        if (inventory != null)
            inventory.OnInventoryChanged -= Refresh;
    }

    private void Refresh()
    {
        ClearSlots();

        if (inventory == null)
            return;

        foreach (var entry in inventory.GetAllItems())
        {
            if (entry.item == null || entry.amount <= 0)
                continue;

            if (entry.item.stackable)
            {
                CreateSlot(entry.item, entry.amount);
            }
            else
            {
                for (int i = 0; i < entry.amount; i++)
                {
                    CreateSlot(entry.item, 1);
                }
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(
            slotParent as RectTransform
        );

        if (inventoryBalance != null)
            inventoryBalance.text = $"$: {inventory.InventoryBalance.ToString()}";
    }

    private void CreateSlot(Item item, long amount)
    {
        GameObject obj = Instantiate(slotPrefab, slotParent);
        InventorySlotUI slot = obj.GetComponent<InventorySlotUI>();

        slot.Initialize(item, inventory);

        if (item.stackable)
            slot.SetAmount(amount);
        else
            slot.HideAmount();

        slots.Add(slot);
    }

    private void ClearSlots()
    {
        foreach (var slot in slots)
        {
            if (slot != null)
                Destroy(slot.gameObject);
        }

        slots.Clear();
    }
}
