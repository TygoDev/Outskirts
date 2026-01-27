using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays a single inventory item.
/// </summary>
public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text amountText;
    private Inventory attatchedInventory;

    private Item item;

    public void Initialize(Item item, Inventory inventory)
    {
        this.attatchedInventory = inventory;
        this.item = item;

        icon.sprite = item.icon;
        icon.enabled = item.icon != null;

        var button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        if (GameManager.Instance.CurrentState != GameState.Shopping)
            return;

        Shop shop = (Shop)GameManager.Instance.shippedComponent;
        shop.MoveItem(item, attatchedInventory);
    }


    public void SetAmount(long amount)
    {
        amountText.text = FormatAmount(amount);
    }

    public void HideAmount()
    {
        amountText.gameObject.SetActive(false);
    }

    private string FormatAmount(long value)
    {
        if (value >= 1_000_000_000)
            return (value / 1_000_000_000f).ToString("0.#") + "B";
        if (value >= 1_000_000)
            return (value / 1_000_000f).ToString("0.#") + "M";
        if (value >= 1_000)
            return (value / 1_000f).ToString("0.#") + "K";

        return value.ToString();
    }
}
