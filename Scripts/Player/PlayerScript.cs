using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField]
    HealthSystem healthSystem;
    [SerializeField]
    HealthBarScript healthBar;
    [SerializeField]
    AmmoScript ammoScript;
    
    [SerializeField]
    Gun gun;

    [Header("Pickup / Drop")]
    [SerializeField]
    float dropRadius = 2f;
    [SerializeField]
    KeyCode pickupKey = KeyCode.E;
    [SerializeField]
    KeyCode dropKey = KeyCode.G;
    [SerializeField]
    Transform gunHolder;
    
    [SerializeField]
    protected Camera cameraTransform;
    
    private Gun _nearbyWeapon;

    void Start()
    {
        healthBar.SetMaxHealth(healthSystem.GetMaxHealth());
        if (gun)
        {
            gun.SetAsEquipped();
            ammoScript.SetAmmo(gun.GetCurrentAmmo(), gun.GetTotalAmmo());
        }
    }

    void Update()
    {
        healthBar.SetHealth(healthSystem.GetCurrentHealth());
        if (gun)
        {
            ammoScript.SetAmmo(gun.GetCurrentAmmo(), gun.GetTotalAmmo());
        }

        if (Input.GetKeyDown(pickupKey) && _nearbyWeapon && !_nearbyWeapon.isEquipped)
        {
            PickupGun(_nearbyWeapon);
        }

        if (Input.GetKeyDown(dropKey) && gun)
        {
            DropCurrentGun();
            ammoScript.SetAmmo(0, 0);
        }
    }
    
    public void NotifyWeaponNearby(Gun weapon)
    {
        if (!weapon.isEquipped)
        {
            _nearbyWeapon = weapon;
        }
    }
    
    
    public void NotifyWeaponLeft(Gun weapon)
    {
        if (_nearbyWeapon == weapon)
        {
            _nearbyWeapon = null;
        }
    }

    void PickupGun(Gun found)
    {
        if (gun)
        {
            DropCurrentGun();
        }

        gun = found;
        _nearbyWeapon = null;
        
        Transform parent = gunHolder ? gunHolder : transform;
        gun.transform.SetParent(parent);
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = new Quaternion(gunHolder.localRotation.x, 0, 0, gunHolder.localRotation.w);
        
        gun.SetCamera(cameraTransform);
        gun.SetAsEquipped();
    }

    void DropCurrentGun()
    {
        if (!gun) return;
        
        if (_nearbyWeapon == gun)
        {
            _nearbyWeapon = null;
        }
        gun.SetAsDropped();
        gun.transform.position = transform.position + transform.forward * dropRadius;
        gun = null;
    }
    
}
