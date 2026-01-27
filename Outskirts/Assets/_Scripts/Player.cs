using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    [SerializeField] private Inventory inventory;

    private void OnEnable()
    {
        if (inventory != null)
            inventory.OnInventoryChanged += UpdatePlayer;

        Initialize();
    }

    private void OnDisable()
    {
        if (inventory != null)
            inventory.OnInventoryChanged -= UpdatePlayer;
    }

    private void Initialize()
    {
        inventory.SetBalance(stats.money);
    }

    private void UpdatePlayer()
    {
        stats.money = inventory.InventoryBalance;
    }
}
