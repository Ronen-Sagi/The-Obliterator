using UnityEngine;

/// <summary>
/// XP pickup that the player can collect to gain experience points.
/// </summary>
public class XPDrop : PickupDrop
{
    /// The number of XP got when collected.
    [SerializeField] private int xpAmount = 1;

    protected override void OnCollected(GameObject player)
    {
        // Add to persistent TotalXP
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddXP(xpAmount);
        }

        // Add to current run ExperienceManager
        if (ExperienceManager.Instance != null)
        {
            ExperienceManager.Instance.AddXP(xpAmount);
        }
    }
}
