using UnityEngine;

/// Tutorial dot that detects the player entering its trigger.
/// Notifies the <see cref="TutorialManager"/> which step was completed and removes itself.
public class TutorialMove : MonoBehaviour
{
    /// 1-based identifier of this tutorial dot used to validate touch order.
    [SerializeField] private int dotNum = 1;

    /// Reference to the tutorial manager coordinating tutorial progression.
    [SerializeField] private TutorialManager tutorialManager;

    /// Ensures a reference to <see cref="TutorialManager"/> is available.
    void Start()
    {
        if (tutorialManager == null)
        {
            tutorialManager = FindObjectOfType<TutorialManager>();
        }
    }

    /// Trigger callback invoked when another collider enters this dot's trigger.
    /// If the collider belongs to the player, reports the dot touch and destroys this dot.
    /// <param name="other">The collider that entered the trigger.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialManager.OnDotTouched(dotNum);
            Destroy(gameObject);
        }
    }
}