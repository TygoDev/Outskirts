using TMPro;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    [SerializeField] private TMP_Text itemNameText = null;
    [SerializeField] private TMP_Text itemDescriptionText = null;
    [SerializeField] private TMP_Text itemValueText = null;

    public void SetValues(string name, string description, int value)
    {
        itemNameText.text = name;
        itemDescriptionText.text = description;

        if (value < 0)
        {
            itemValueText.gameObject.SetActive(true);
            itemValueText.text = $"$: {value}";
        }
        else
        itemValueText.gameObject.SetActive(false);
    }
}
