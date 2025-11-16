using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    [SerializeField]
    private string gunName;
    
    [SerializeField]
    private LayerMask targetLayerMask;
    [SerializeField]
    private bool isPickupable = true;
    
    [Header("Gun Stats")]
    [SerializeField]
    private float damage;
    [SerializeField]
    private float range;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private bool isAutomatic;
    
    [Header("Ammunition")]
    [SerializeField]
    private int magazineSize;
    [SerializeField]
    private int totalAmmo;
    [SerializeField]
    private float reloadTime;
    
    [Header("Sounds")]
    [SerializeField]
    private AudioClip shootSound;
    [SerializeField]
    private AudioClip reloadSound;
    
    
    
    public string GunName => gunName;
    public LayerMask TargetLayerMask => targetLayerMask;
    public float Damage => damage;
    public float Range => range;
    public float FireRate => fireRate;
    public int MagazineSize => magazineSize;
    public int TotalAmmo => totalAmmo;
    public float ReloadTime => reloadTime; 
    public bool IsAutomatic => isAutomatic;
    public AudioClip ShootSound => shootSound;
    public AudioClip ReloadSound => reloadSound;
    public bool IsPickupable => isPickupable;
}
