using UnityEngine;

public class RPGScript: Gun
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField] private float shootForce;
    [SerializeField]
    private Transform projectileSpawnPoint;
    
    protected override void Shoot()
    {
        var screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        var ray = cam.ScreenPointToRay(screenCenter);

        var targetPoint = Physics.Raycast(ray, out RaycastHit hit, gunData.Range, gunData.TargetLayerMask) ? hit.point : ray.GetPoint(gunData.Range);
        
        var direction = targetPoint - projectileSpawnPoint.position;
        
        var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        if(projectile.TryGetComponent(out ProjectileScript damageProjectile))
        {
            damageProjectile.SetDamage(gunData.Damage);
        }
        projectile.transform.forward = direction;
        if (projectile.TryGetComponent(out Rigidbody rb))
        {
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rb.AddForce(direction * shootForce, ForceMode.Impulse);
        }
    }
}
