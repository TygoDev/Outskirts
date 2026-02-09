using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] private CharacterStats playerCharacter;
    [SerializeField] private Inventory playerInventory = null;

    [SerializeField] private EquipmentSlot weaponSlot;

    public void UnequipItem(EquipmentSlot equipmentSlot)
    {
        playerInventory.AddItem(equipmentSlot.item);
        equipmentSlot.item = null;
        equipmentSlot.SetVisuals();
    }

    public void EquipItem(Item item)
    {
        if(item.itemType == ItemType.Weapon)
        {
            weaponSlot.item = item;
            weaponSlot.SetVisuals();
        }
    }
}
