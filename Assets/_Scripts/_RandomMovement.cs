using UnityEngine;

public class _RandomMovement : MonoBehaviour
{
    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Start()
    {
        SetRandomTargetPosition();
        RotateTowardsTargetPosition();
    }

    void Update()
    {
        Vector3 currentPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(currentPosition, targetPosition) < 0.01f)
        {
            SetRandomTargetPosition();
            RotateTowardsTargetPosition();
        }
    }

    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }

    void RotateTowardsTargetPosition()
    {
        Vector3 direction = targetPosition - transform.position;
        targetRotation = Quaternion.LookRotation(direction);
    }
}