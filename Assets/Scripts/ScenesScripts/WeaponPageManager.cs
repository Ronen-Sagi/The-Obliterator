using UnityEngine;

public class WeaponPageManager : MonoBehaviour
{
    private WeaponManager wp;
    [SerializeField] GameObject shopPage;
    [SerializeField] GameObject weaponPage;
    
    private void Awake()
    {
        wp = WeaponManager.Instance;
        if (wp == null)
        {
            Debug.LogError("WeaponManager.Instance is NULL", this);
        }
    }
    
    public void EquipWeaponButton(string weaponName)
    {
        GameObject weaponPrefab = wp.GetWeapon(weaponName);
        if (weaponPrefab != null)
        {
            wp.CurrentWeapon = weaponPrefab;
            Debug.Log($"Equipped weapon: {weaponName}");
        }
        else
        {
            Debug.LogWarning($"Weapon '{weaponName}' not found in WeaponManager.");
        }
    }

    public void BackToShopButton()
    {
        shopPage.SetActive(true);
        weaponPage.SetActive(false);
    }
    
    public void OpenWeaponPage()
    {
        shopPage.SetActive(false);
        weaponPage.SetActive(true);
    }
}
