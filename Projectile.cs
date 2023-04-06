using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.AI;
using static Enemy;

public class Projectile : MonoBehaviour
{
    // The amount of damage that the projectile deals
    public int damage = 10;

    // The radius of the area of effect (AOE)
    public float aoeRadius = 0;

    // The status effects that the projectile applies
    public bool freeze = false;
    public bool poison = false;
    public bool fire = false;
    public bool aoe = false;
    public Enemy targetEnemy;
    public float speed;

    // The duration of the status effects
    public float effectDuration = 5;

    // The amount of damage that the poison and fire status effects deal over time
    public int damageOverTime = 5;


    // The percentage by which the freeze effect slows the enemy
    public float freezeAmount = 0.5f;
    // Function called when the projectile hits an enemy

    // Function called every frame
    void Update()
    {
        // Find all enemies in the scene
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        if (enemies.Length > 0)
        {
            // Set the default nearest enemy to be the first enemy in the array
            Enemy nearestEnemy = enemies[0];

            // Loop through all enemies in the scene
            foreach (Enemy enemy in enemies)
            {
                // If the current enemy is closer to the projectile than the nearest enemy
                if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, nearestEnemy.transform.position))
                {
                    // Set the current enemy as the nearest enemy
                    nearestEnemy = enemy;
                }
            }

            // Set the nearest enemy as the target enemy
            targetEnemy = nearestEnemy;

            // If the target enemy is not null
            if (targetEnemy != null)
            {
                // Move the projectile towards the target enemy
                transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, speed * Time.deltaTime);

                // If the projectile has reached the target enemy
                if (transform.position == targetEnemy.transform.position)
                {
                    // Apply the damage and effects to the target enemy
                    OnHit(targetEnemy);
                }
            }
            else
            {
                // If the target enemy is null, destroy the projectile
                Destroy(gameObject);
            }
        }
    }

	public void OnEnemyMissing()
	{
		Destroy(gameObject);
	}

	public void OnHit(Enemy enemy)
    {

        // Reduce the enemy's health by the amount of damage taken
        enemy.health -= damage;

        // If the projectile has the freeze effect
        if (freeze)
        {
            // Freeze the enemy
            enemy.Freeze(effectDuration, freezeAmount);
        }

        // If the projectile has the poison effect
        if (poison)
        {
            // Poison the enemy
            enemy.Poison(effectDuration, damageOverTime);
        }

        // If the projectile has the fire effect
        if (fire)
        {
            // Ignite the enemy
            enemy.Ignite(effectDuration, damageOverTime);
        }

        // If the projectile has the AOE effect
        if (aoe)
        {
            // Get all enemies within the AOE radius
            Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, aoeRadius);

            // Loop through all enemies in range
            foreach (Collider enemyCollider in enemiesInRange)
            {
                // Get the enemy component from the collider
                Enemy enemyInRange = enemyCollider.GetComponent<Enemy>();

                // If the enemy is not null
                if (enemyInRange != null)
                {
                    // Reduce the enemy's health by the amount of damage taken
                    enemyInRange.health -= damage;

                    // If the projectile has the freeze effect
                    if (freeze)
                    {
                        // Freeze the enemy
                        enemyInRange.Freeze(effectDuration, freezeAmount);
                    }

                    // If the projectile has the poison effect
                    if (poison)
                    {
                        // Poison the enemy
                        enemyInRange.Poison(effectDuration, damageOverTime);
                    }

                    // If the projectile has the fire effect
                    if (fire)
                    {
                        // Ignite the enemy
                        enemyInRange.Ignite(effectDuration, damageOverTime);
                    }
                }
            }
        }

        // Destroy the projectile
        Destroy(gameObject);
    }
}