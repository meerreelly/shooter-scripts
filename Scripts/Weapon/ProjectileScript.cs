using System.Collections;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField]
    private bool damageable = false;
    private float damage;
    [SerializeField]
    private GameObject expossionEffect;
    [SerializeField]
    private float effectDuration = 2f;
    private void OnCollisionEnter(Collision collision)
    {
        if (damageable)
        {
            if (collision.gameObject.TryGetComponent(out HealthSystem health))
            {
                health.OnDamage(damage);
            }
        }
        Destroy(gameObject);
        if (expossionEffect)
        {
            CreateExpose();
        }
    }
    
    public void SetDamage(float damageAmount)
    {
        damage = damageAmount;
    }

    private void CreateExpose()
    {
        var effect = Instantiate(expossionEffect, transform.position, Quaternion.identity);
        StartCoroutine(DestroyEffect(effect));
    }
    
    private IEnumerator DestroyEffect(GameObject effect)
    {
        yield return new WaitForSeconds(effectDuration);
        Destroy(effect);
    }
}
