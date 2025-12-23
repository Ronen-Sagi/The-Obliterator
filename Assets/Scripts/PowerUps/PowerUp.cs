using UnityEngine;

public enum PowerUpType
{
    CoinMagnet,
    TimeFreeze,
    DoubleTap,
    ShieldBubble,
    XPSurge
}

[CreateAssetMenu(fileName = "NewPowerUp", menuName = "PowerUps/PowerUp")]
public class PowerUp : ScriptableObject
{
    public PowerUpType type;
    public float duration = 5f;
    public float multiplier = 2f; // For XP Surge
}
