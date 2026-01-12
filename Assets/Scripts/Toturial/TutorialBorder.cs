using UnityEngine;

/// <summary>
/// Border component that blocks the player but allows enemies to pass through. 
/// Uses Unity's layer-based collision system.
/// </summary>
public class TutorialBorder : MonoBehaviour
{
    void Start()
    {
        // Ensure this border has a collider
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogWarning("TutorialBorder needs a Collider2D component!", this);
        }
    }
}