using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // The path that the enemy will follow
    public Transform[] path;

    // The current waypoint on the path that the enemy is heading towards
    private int currentWaypoint = 0;

    // The speed at which the enemy moves
    public float speed = 1.0f;

    void Update()
    {
        // Get the enemy's current position
        Vector3 currentPosition = transform.position;

        // Check if the enemy has reached the end of the path
        if (currentWaypoint >= path.Length)
        {
            // If the enemy has reached the end of the path, destroy it
            Destroy(gameObject);
            return;
        }

        // Get the position of the next waypoint on the path
        Vector3 targetPosition = path[currentWaypoint].position;

        // Move the enemy towards the next waypoint on the path
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

        // Check if the enemy has reached the next waypoint on the path
        if (currentPosition == targetPosition)
        {
            // If the enemy has reached the next waypoint, increment the current waypoint
            currentWaypoint++;
        }
    }
}