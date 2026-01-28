using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character Stats")]
public class CharacterStats : ScriptableObject
{
    public List<Item> lootTable = new List<Item>();
    public int moneyDropValue = 1;
    public string characterName;
    public float maxHealth = 100f;
    public float damage = 10f;
    public float speed = 2f;
    public float knockbackForce = 2f;
    public int size = 1;
}
