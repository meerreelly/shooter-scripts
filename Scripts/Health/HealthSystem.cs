using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private HealthData healthData;
    
    private float currentHealth;
    
    void Start()
    {
        currentHealth = healthData.MaxHealth;
    }
    
    public void OnDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
    
    public float GetMaxHealth() => healthData.MaxHealth;
    
    public float GetCurrentHealth() => currentHealth;
}
