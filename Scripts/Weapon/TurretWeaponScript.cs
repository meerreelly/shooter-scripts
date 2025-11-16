using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TurretWeaponScript:Gun
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField] private float shootForce;
    [SerializeField]
    private Transform projectileSpawnPoint;
    [SerializeField]
    private TurretScript turretScript;

    private bool isShooting = false;

    protected override void Update()
    {
        if(GetCurrentAmmo()<=0 && !IsReloading() && GetTotalAmmo()>0)
        {
            TryToReload();
        }
        
        if (turretScript.IsFollowingPlayer() && !isShooting && !IsReloading())
        {
            StartCoroutine(ShootCycle());
        }
    }

    private IEnumerator ShootCycle()
    {
        isShooting = true;

        float shootTime = turretScript.GetShootTime();
        float shootCooldown = turretScript.GetShootCooldown();
        
        float timer = 0f;
        while (timer < shootTime && turretScript.IsFollowingPlayer())
        {
            TryShoot();
            timer += Time.deltaTime;
            yield return null; 
        }
        
        yield return new WaitForSeconds(shootCooldown);
        isShooting = false;
    }

    protected override void Shoot()
    {
        Vector3 direction = projectileSpawnPoint.forward;
        var projectile = Instantiate(
            projectilePrefab, 
            projectileSpawnPoint.position, 
            Quaternion.LookRotation(direction)
        );
        
        if (projectile.TryGetComponent(out ProjectileScript damageProjectile))
        {
            damageProjectile.SetDamage(gunData.Damage);
        }
        
        if (projectile.TryGetComponent(out Rigidbody rb))
        {
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rb.AddForce(direction * shootForce, ForceMode.Impulse);
        }
    }
}
