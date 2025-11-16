using UnityEngine;

public class RotateScript : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 10f;
    
    [Header("Random Rotation Settings")]
    [SerializeField]
    private float randomRotationSpeed = 5f;
    [SerializeField]
    private float changeDirectionTime = 2f;
    
    private Transform target;
    private bool follow = false;
    private Vector3 randomRotation;
    private float nextChangeTime = 0;


    private void LateUpdate()
    {
        if (follow && target)
        {
            RotateToTarget();
        }else
        {
            RandomRotation();
        }
    }
    
    public void FollowTarget(Transform targetTransform)
    {
        target = targetTransform;
        follow = true;
    }
    
    public void StopFollowing()
    {
        follow = false;
    }
    
    private void RotateToTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            desiredRotation,
            rotationSpeed * Time.deltaTime
        );
    }
    
    
    private void RandomRotation()
    {
        if (Time.time >= nextChangeTime)
        {
            SetRandomRotation();
            nextChangeTime = Time.time + changeDirectionTime;   
        }
        transform.Rotate(randomRotation * (randomRotationSpeed * Time.deltaTime));
    }
    
    
    private void SetRandomRotation()
    {
        randomRotation = new Vector3(0f, Random.Range(-1f, 1f), 0f).normalized;
    }
}
