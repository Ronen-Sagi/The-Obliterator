using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] Health playerHealth;
    [SerializeField] Slider slider;
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0, 1.2f, 0);

    void Start()
    {
        slider.maxValue = playerHealth.GetMaxHealth();
        slider.value = playerHealth.GetCurrentHealth();
    }

    void Update()
    {
        slider.value = playerHealth.GetCurrentHealth();
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
        transform.position = target.position + offset;
    }
}