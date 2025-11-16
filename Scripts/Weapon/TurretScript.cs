using Unity.VisualScripting;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField]
    private RotateScript rotateScript;
    private bool isFollowingPlayer = false;

    [SerializeField] 
    private float shootTime = 2f;
    [SerializeField]
    private float shootCooldown = 1f;
    
    public bool IsFollowingPlayer()=> isFollowingPlayer;
    public float GetShootCooldown()=> shootCooldown;
    public float GetShootTime()=> shootTime;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rotateScript.FollowTarget(other.transform);
            isFollowingPlayer = true;
        }  
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rotateScript.StopFollowing();
            isFollowingPlayer = false;
        }
    }
    
    
}
