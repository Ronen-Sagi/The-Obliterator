using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    // HashMap: weaponId -> WeaponData
    private Dictionary<string, GameObject> weapons =
        new Dictionary<string, GameObject>();

    [Header("Initial Weapons")]
    [SerializeField] private List<GameObject> startingWeapons;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        RegisterStartingWeapons();
    }

    private void RegisterStartingWeapons()
    {
        foreach (var weapon in startingWeapons)
        {
            RegisterWeapon(weapon.name, weapon);
        }
    }

    public void RegisterWeapon(string weaponName, GameObject weaponPrefab)
    {
        if (string.IsNullOrEmpty(weaponName))
        {
            Debug.LogWarning("Weapon name is invalid");
            return;
        }

        if (weaponPrefab.CompareTag("Weapon"))
        {
            Debug.LogWarning("Prefab is not tagged as Weapon");
        }

        if (!weapons.ContainsKey(weaponName))
        {
            weapons.Add(weaponName, weaponPrefab);
        }
    }

    public GameObject GetWeapon(string weaponName)
    {
        if (weapons.TryGetValue(weaponName, out GameObject weapon))
            return weapon;

        Debug.LogWarning($"Weapon '{weaponName}' not found");
        return null;
    }

    public bool HasWeapon(string weaponName)
    {
        return weapons.ContainsKey(weaponName);
    }
}