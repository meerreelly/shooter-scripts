using UnityEngine;

public class ak47Script: Gun
{
    protected override void Shoot()
    {
        var screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        var ray = cam.ScreenPointToRay(screenCenter);
        
        if(Physics.Raycast(ray, out RaycastHit hit, gunData.Range, gunData.TargetLayerMask))
        {
            if (hit.transform.TryGetComponent<HealthSystem>(out var health))
            {
                health.OnDamage(gunData.Damage);
            }
        }
    }
}
