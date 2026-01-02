using UnityEngine;

/// <summary>
/// Abstract base class for all power-ups in the game.
/// Defines the common interface and behavior that all power-ups must implement.
/// </summary>
public abstract class PowerUpBase : ScriptableObject
{
    /// Display name of the power-up (e.g., "Speed Boost", "Shield")
    [SerializeField] protected string powerUpName;
    
    /// Description shown in the shop
    [SerializeField] protected string description;
    
    /// Duration of the power-up effect in seconds
    [SerializeField] protected float duration = 5f;
    
    /// Price in the shop
    [SerializeField] protected int price = 50;
    
    /// Icon for UI display
    [SerializeField] protected Sprite icon;

    /// Gets the display name of this power-up
    public string PowerUpName => powerUpName;
    
    /// Gets the description of this power-up
    public string Description => description;
    
    /// Gets the duration of this power-up
    public float Duration => duration;
    
    /// Gets the price of this power-up
    public int Price => price;
    
    /// Gets the icon of this power-up
    public Sprite Icon => icon;

    /// <summary>
    /// Activates the power-up effect on the specified player GameObject.
    /// This method must be implemented by each specific power-up type.
    /// </summary>
    /// <param name="player">The player GameObject to apply the effect to</param>
    public abstract void Activate(GameObject player);

    /// <summary>
    /// Deactivates the power-up effect on the specified player GameObject.
    /// This method must be implemented by each specific power-up type.
    /// </summary>
    /// <param name="player">The player GameObject to remove the effect from</param>
    public abstract void Deactivate(GameObject player);
}