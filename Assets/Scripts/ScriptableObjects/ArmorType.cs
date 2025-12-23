using UnityEngine;

[CreateAssetMenu(fileName = "NewArmorType", menuName = "Equipment/Armor Type")]
public class ArmorType : ScriptableObject
{
    public string armorName;
    public float healthMultiplier = 1f; // 1.5 = +50%
    public float speedMultiplier = 1f; // 0.85 = -15%
    public float reflectionChance = 0f; // 0 to 1
}
