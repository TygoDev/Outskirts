using UnityEngine;

public class Equipment : MonoBehaviour
{
    public static Equipment Instance { get; private set; }

    [SerializeField] private CharacterStats playerCharacter;
    [SerializeField] private Inventory playerInventory = null;

    [SerializeField] private EquipmentSlot weaponSlot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void UnequipItem(EquipmentSlot equipmentSlot)
    {
        if (equipmentSlot.item == null)
            return;

        playerInventory.AddItem(equipmentSlot.item);
        equipmentSlot.item = null;
        equipmentSlot.SetVisuals();
    }

    public void EquipItem(Item item)
    {
        if (item == null)
            return;

        if (item.itemType == ItemType.Weapon)
        {
            if(weaponSlot.item != null)
                playerInventory.AddItem(weaponSlot.item);

            weaponSlot.item = item;
            weaponSlot.SetVisuals();
            playerInventory.RemoveItem(item);
        }
    }
}
