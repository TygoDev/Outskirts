using UnityEngine;

[CreateAssetMenu(
    fileName = "NewPlayerStats",
    menuName = "Player/Stats",
    order = 1)]
public class PlayerStats : ScriptableObject
{
    public int money = 0;
}
