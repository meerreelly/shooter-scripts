using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField]
    protected GunData gunData;
    protected Camera cam;
    
    [Header("Keybinds")]
    [SerializeField]
    protected KeyCode reloadKey = KeyCode.R;
    [SerializeField]
    protected KeyCode shootKey = KeyCode.Mouse0;
    
    [HideInInspector]
    public bool isEquipped = false;
    
    [Header("Pickup")]
    [SerializeField]
    private Collider pickupTrigger;
    
    private int currentAmmo = 0;
    private float nextTimeToFire = 0;
    private bool isReloading = false;
    private int totalAmmo = 0;
    
    
    public int GetCurrentAmmo() => currentAmmo;
    public int GetTotalAmmo() => totalAmmo;
    public bool IsReloading() => isReloading;
    
    public void SetCamera(Camera newCam)
    {
        cam = newCam;
    }
    
    void Start()
    {
        currentAmmo = gunData.MagazineSize;
        totalAmmo = gunData.TotalAmmo;
    }
    
    
    void OnTriggerEnter(Collider other)
    {
        if (gunData.IsPickupable)
        {
            if (isEquipped) return;
            if (other.TryGetComponent(out PlayerScript player))
            {
                player.NotifyWeaponNearby(this);
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (gunData.IsPickupable)
        {
            if (isEquipped) return;
            if (other.TryGetComponent(out PlayerScript player))
            {
                player.NotifyWeaponLeft(this);
            }
        }
    }
    
    protected virtual void Update()
    {
        if (!isEquipped)
        {
            return;
        }
        
        if (gunData.IsAutomatic && !isReloading)
        {
            if (Input.GetKey(shootKey))
            {
                TryShoot();
            }
        }
        else if(!isReloading)
        {
            if (Input.GetKeyDown(shootKey))
            {
                TryShoot();
            }
        }
        if (Input.GetKeyDown(reloadKey))
        {
            TryToReload();
        }
    }

    protected void TryToReload()
    {
        if(!isReloading && currentAmmo < gunData.MagazineSize && gunData.TotalAmmo > 0)
        {
            StartCoroutine(Reload());
        }
    }
    
    private IEnumerator Reload()
    {
        isReloading = true;
        if (gunData.ReloadSound)
        {
            AudioSource.PlayClipAtPoint(gunData.ReloadSound, transform.position);
        }
        yield return new WaitForSeconds(gunData.ReloadTime);
        
        int ammoNeeded = gunData.MagazineSize - currentAmmo;
        if(totalAmmo >= ammoNeeded)
        {
            currentAmmo += ammoNeeded;
            totalAmmo -= ammoNeeded;
        }
        else
        {
            currentAmmo += totalAmmo;
            totalAmmo = 0;
        }
        isReloading = false;
    }

    protected void TryShoot()
    {
        if (isReloading)
        {
            return;
        }
        if(currentAmmo<=0)
        {
            return;
        }

        if (Time.time >= nextTimeToFire)
        {
            if (gunData.FireRate <= 0f || float.IsNaN(gunData.FireRate))
            {
                nextTimeToFire = Time.time + 0.1f; 
            }
            else
            {
                nextTimeToFire = Time.time + (1f / gunData.FireRate);
            }
            HandleShoot();
        }
        
    }

    private void HandleShoot()
    {
        currentAmmo--;
        if(gunData.ShootSound)
        {
            AudioSource.PlayClipAtPoint(gunData.ShootSound, transform.position);
        }
        Shoot();
    }

    protected abstract void Shoot();
    
    
    public void SetAsEquipped()
    {
        isEquipped = true;
        
        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = true; 
            rb.detectCollisions = false;
        }
        if (TryGetComponent(out Collider col))
        {
            col.enabled = false;
        }
        if (pickupTrigger && gunData.IsPickupable)
        {
            pickupTrigger.enabled = false;
        }
    }
    
    public void SetAsDropped()
    {
        isEquipped = false;
        cam = null;
        transform.SetParent(null);
        
        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = false; 
            rb.detectCollisions = true;
        }

        if (TryGetComponent(out Collider col))
        {
            col.enabled = true;
        }
        if (pickupTrigger && gunData.IsPickupable)
        {
            pickupTrigger.enabled = true;
        }
    }
}
