using UnityEngine;

[CreateAssetMenu(fileName = "NewWheelType", menuName = "Equipment/Wheel Type")]
public class WheelType : ScriptableObject
{
    public string wheelName;
    public float speedMultiplier = 1f;
    public float friction = 1f; // High friction = low sliding
}
