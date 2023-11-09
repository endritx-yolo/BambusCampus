using UnityEngine;

public class _RandomMovement : MonoBehaviour
{
    public float minX = -10f; // Minimum X value for random position
    public float maxX = 10f; // Maximum X value for random position
    public float minZ = -10f; // Minimum Z value for random position
    public float maxZ = 10f; // Maximum Z value for random position
    public float moveSpeed = 5f; // Speed at which the object moves
    public float rotationSpeed = 5f; // Speed at which the object rotates

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Start()
    {
        // Set the initial random target position while keeping the current Y position
        SetRandomTargetPosition();
        RotateTowardsTargetPosition();
    }

    void Update()
    {
        // Move the object towards the target XY position while keeping the Y position unchanged
        Vector3 currentPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);

        // Smoothly rotate the object to face the target position
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Check if the object has reached the target XY position
        if (Vector3.Distance(currentPosition, targetPosition) < 0.01f)
        {
            // Set a new random target XY position while keeping the Y position unchanged
            SetRandomTargetPosition();
            RotateTowardsTargetPosition();
        }
    }

    void SetRandomTargetPosition()
    {
        // Generate random X and Z coordinates within the specified minX, maxX, minZ, and maxZ values
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }

    void RotateTowardsTargetPosition()
    {
        // Calculate the rotation to face the target position
        Vector3 direction = targetPosition - transform.position;
        targetRotation = Quaternion.LookRotation(direction);
    }
}
