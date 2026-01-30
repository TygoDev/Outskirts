using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays a single inventory item.
/// </summary>
public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private GameObject descriptionBackground;
    private Inventory attatchedInventory;
    private ItemDescription itemDescription;

    private Item item;

    public void Initialize(Item item, Inventory inventory, GameObject descriptionObject)
    {
        attatchedInventory = inventory;
        descriptionBackground = descriptionObject;
        itemDescription = descriptionBackground.GetComponent<ItemDescription>();
        this.item = item;

        icon.sprite = item.icon;
        icon.enabled = item.icon != null;

        var button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        if (GameManager.Instance.CurrentState == GameState.Shopping)
        {
            if (descriptionBackground.activeSelf)
                descriptionBackground.SetActive(false);

            Shop shop = (Shop)GameManager.Instance.shippedComponent;
            shop.MoveItem(item, attatchedInventory);
        }

        if (GameManager.Instance.CurrentState == GameState.Idle || GameManager.Instance.CurrentState == GameState.Crafting)
        {
            descriptionBackground.SetActive(!descriptionBackground.activeSelf);

            if (descriptionBackground.activeSelf)
            {
                itemDescription.SetValues(item.itemName, item.description, item.sellPrice);
            }
        }
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
