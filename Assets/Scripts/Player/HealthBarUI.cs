using UnityEngine;
using UnityEngine.UI;

/// UI controller that displays a world\-/screen\-/space health bar for the player.
public class HealthBarUI : MonoBehaviour
{
    /// Reference to the player's <see cref="Health"/> component used as the data source.
    [SerializeField] Health playerHealth;

    /// The UI <see cref="Slider"/> that represents health.
    /// Its <see cref="Slider.maxValue"/> is set from <see cref="Health.GetMaxHealth"/>
    /// and its <see cref="Slider.value"/> is set from <see cref="Health.GetCurrentHealth"/>.
    [SerializeField] Slider slider;

    /// The transform the health bar should follow (typically the player).
    [SerializeField] Transform target;

    /// Positional offset applied to <see cref="target"/> when placing the health bar.
    [SerializeField] Vector3 offset = new Vector3(0, 1.2f, 0);


    /// Initializes the slider range and initial value based on the player's health.
    void Start()
    {
        slider.maxValue = playerHealth.GetMaxHealth();
        slider.value = playerHealth.GetCurrentHealth();
    }


    /// Refreshes the slider value to track the player's current health.
    void Update()
    {
        slider.value = playerHealth.GetCurrentHealth();
    }

    /// Positions the health bar at <c>target.position + offset</c> and resets rotation
    /// to <see cref="Quaternion.identity"/> so it does not inherit/accumulate unwanted rotation.
    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
        transform.position = target.position + offset;
    }
}