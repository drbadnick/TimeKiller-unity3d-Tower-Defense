using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.AI;
using System.Collections;
using static Projectile;

public class Enemy : MonoBehaviour
{
    // The amount of health that the enemy has
    public int health = 100;

    // The amount of coins that the enemy is worth
    public int worth = 10;

    // The speed at which the enemy moves
    public float speed = 10;

    // The waypoints that the enemy follows
    public Transform[] waypoints;

    // The index of the current waypoint
    private int waypointIndex = 0;

    public new Renderer renderer;   

    public Color startColor = Color.green;
    public Color endColor = Color.red;



    // The projectile component of the collider that the enemy is colliding with
    public Projectile projectile;

    // Function called when the enemy is created
    void Start()
    {

        renderer = GetComponent<Renderer>();

        // Set the enemy's initial position to the position of the first waypoint
        transform.position = waypoints[waypointIndex].position;
    }
    // Function called when the enemy reaches the final waypoint
    void OnReachFinalWaypoint()
    {
        // Decrement the player's lives by one
        GameManager.instance.OnEnemyReachesCastle();

        // Destroy the enemy
        Destroy(gameObject);
    }
    // Function called every frame
    void Update()
    {

        // Calculate the enemy's current health percentage
        float healthPercentage = (float)health / 100;


        // Set the enemy's color to a gradient between green and yellow based on its health percentage
        renderer.material.color = Color.Lerp(Color.red, Color.green, healthPercentage);

        // Rotate the enemy towards the direction where it is moving
        Vector3 direction = (waypoints[waypointIndex].position - transform.position).normalized;
		if (direction != Vector3.zero)
		{
			Quaternion lookRotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
		}

		// If the enemy's health is less than or equal to zero
		if (health <= 0)
        {
            // Destroy the enemy
            Destroy(gameObject);
            return;
        }
        if (waypointIndex == waypoints.Length)
        {
            // Decrement the player's lives by one
            GameManager.instance.OnEnemyReachesCastle();

            // Destroy the enemy
            Destroy(gameObject);
            return;
        }

        // Move the enemy towards the current waypoint
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].position, speed * Time.deltaTime);

        // If the enemy has reached the current waypoint
        if (transform.position == waypoints[waypointIndex].position)
        {
            // Go to the next waypoint
            waypointIndex++;

            // If the enemy has reached the final waypoint
            if (waypointIndex == waypoints.Length)
            {
                // Decrement the player's lives by one
                GameManager.instance.OnEnemyReachesCastle();

                // Destroy the enemy
                Destroy(gameObject);
                return;
            }
        }
    }

    // Function called when the enemy collides with another object
    void OnCollisionEnter(Collision collision)
    {
        // Get the projectile component from the collider that the enemy is colliding with
        Projectile projectile = collision.collider.GetComponent<Projectile>();

        // If the projectile is not null
        if (projectile != null)
        {
            // Restore the enemy's speed by dividing it by the freeze amount of the projectile
            speed /= projectile.freezeAmount;
        }
    }


    // Function called when the enemy is destroyed
    void OnDestroy()
    {
        // Add the worth of the enemy to the player's coin balance
        GameManager.instance.OnEnemyKilled(worth);
    }

    // Enumeration of the possible enemy statuses
    public enum Status
    {
        None,
        Frozen,
        Poisoned,
        Ignited
    }

    // The current status of the enemy
    public Status status = Status.None;

    // Function called when the enemy is frozen
    public void Freeze(float duration, float slowAmount)
    {
        // Set the enemy's status to frozen
        status = Status.Frozen;

        // Set the enemy's speed to the slow amount
        speed *= slowAmount;

        // Wait for the duration of the freeze effect
        StartCoroutine(Unfreeze(duration));
    }

    // Function called to unfreeze the enemy after the duration of the freeze effect
    IEnumerator Unfreeze(float duration)
    {
        // Wait for the duration of the freeze effect
        yield return new WaitForSeconds(duration);

        // Set the enemy's status to none
        status = Status.None;

		if (projectile != null)
		{
			speed /= projectile.freezeAmount;
		}
	}

    // Function called when the enemy is poisoned
    public void Poison(float duration, int damageOverTime)
    {
        // Set the enemy's status to poisoned
        status = Status.Poisoned;

        // Wait for the duration of the poison effect
        StartCoroutine(Unpoison(duration, damageOverTime));
    }

    // Function called to unpoison the enemy after the duration of the poison effect
    IEnumerator Unpoison(float duration, int damageOverTime)
    {
        // Wait for the duration of the poison effect
        yield return new WaitForSeconds(duration);

        // Set the enemy's status to none
        status = Status.None;

        // Stop the poison damage coroutine
        StopCoroutine(PoisonDamage(damageOverTime));
    }

    // Function called every second while the enemy is poisoned
    IEnumerator PoisonDamage(int damageOverTime)
    {
        // Reduce the enemy's health by the amount of poison damage taken
        health -= damageOverTime;

        // Wait for one second
        yield return new WaitForSeconds(1);

        // If the enemy is still poisoned
        if (status == Status.Poisoned)
        {
            // Repeat the poison damage coroutine
            StartCoroutine(PoisonDamage(damageOverTime));
        }
    }

    // Function called when the enemy is ignited
    public void Ignite(float duration, int damageOverTime)
    {
        // Set the enemy's status to ignited
        status = Status.Ignited;

        // Wait for the duration of the ignite effect
        StartCoroutine(Unignite(duration, damageOverTime));
    }

    // Function called to unignite the enemy after the duration of the ignite effect
    IEnumerator Unignite(float duration, int damageOverTime)
    {
        // Wait for the duration of the ignite effect
        yield return new WaitForSeconds(duration);

        // Set the enemy's status to none
        status = Status.None;

        // Stop the ignite damage coroutine
        StopCoroutine(IgniteDamage(damageOverTime));
    }

    // Function called every second while the enemy is ignited
    IEnumerator IgniteDamage(int damageOverTime)
    {
        // Reduce the enemy's health by the amount of ignite damage taken
        health -= damageOverTime;

        // Wait for one second
        yield return new WaitForSeconds(1);

        // If the enemy is still ignited
        if (status == Status.Ignited)
        {
            // Repeat the ignite damage coroutine
            StartCoroutine(IgniteDamage(damageOverTime));
        }
    }
}