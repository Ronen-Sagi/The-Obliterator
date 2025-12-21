using UnityEngine;

/// <summary>
/// XP pickup that the player can collect to gain experience points.
/// </summary>
public class XPDrop : PickupDrop
{
    [SerializeField] private int xpAmount = 1;

    protected override void OnCollected(GameObject player)
    {
        // Find the XP manager or level system and add XP
        // Adjust this based on your actual XP system
        //var xpManager = player.GetComponent<XPManager>(); // or however you manage XP
        //if (xpManager != null)
        //{
        //    xpManager. AddXP(xpAmount);
        //}
    }
}