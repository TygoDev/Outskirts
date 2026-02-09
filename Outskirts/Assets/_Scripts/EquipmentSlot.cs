using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private Image equipmentSlotImage = null;
    public Item item = null;

    private void Start()
    {
        SetVisuals();
    }

    public void SetVisuals()
    {
        if (item != null)
            equipmentSlotImage.sprite = item.icon;
        else
            equipmentSlotImage.sprite = null;
    }
}
