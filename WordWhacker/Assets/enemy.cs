using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 50.0f; // Speed at which the enemy moves
    private Vector3 targetPosition = new Vector3(400, -430, 0); // Target position to move towards

    void Update()
    {
        // Calculate the direction vector from the current position to the target position
        Vector3 direction = targetPosition - transform.position;

        // Calculate the angle towards the target position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation about the Z axis to point towards the target
        transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Subtracting 90 degrees because the default forward for sprites is to the right (x-positive)

        // Move the enemy towards the target
        direction.Normalize(); // Normalize the direction vector after using it for rotation
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}
