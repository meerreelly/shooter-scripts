using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "Scriptable Objects/HealthData")]
public class HealthData : ScriptableObject
{
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float regenRate = 0f;
    [SerializeField]
    private float regenDelay = 0;
    
    public float MaxHealth => maxHealth;
    public float RegenRate => regenRate;
    public float RegenDelay => regenDelay;
}
