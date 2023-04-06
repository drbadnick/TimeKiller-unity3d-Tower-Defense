using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    // The cost of the turret in silver coins
    public int cost = 10;

    public GameObject projectilePrefab;

    // The rate of fire of the turret in shots per second
    public float rateOfFire = 1;

    // The amount of damage that the turret will deal with each shot
    public int damage = 1;

    // The range of the turret in meters
    public float range = 5;

    // The target that the turret is currently attacking
    public Enemy target;

    // The time at which the turret last fired
    private float lastFired = 0;

    void Start()
    {

        // Initialize the lastFired variable to 0
        lastFired = -300;
    }

    void Update()
    {
        // If the turret doesn't have a target
        if (target == null)
        {
            // Find all the enemies within the range of the turret
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");

            // Iterate through each of the found enemies
            foreach (var enemy in enemies)
            {
                // If the enemy is within range of the turret
                if (Vector3.Distance(transform.position, enemy.transform.position) <= range)
                {
                    // Set the target of the turret to the enemy
                    target = enemy.GetComponent<Enemy>();
                    break;
                }
            }
        }

        // If the turret has a target
        if (target != null)
        {
            // Activate the projectile prefab
            projectilePrefab.SetActive(true);

            // If the turret is ready to fire again
            if (Time.time > lastFired + 1 / rateOfFire)
            {
                // Create a new projectile at the position of the turret
                var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                // Set the damage of the projectile to the damage of the turret
                projectile.GetComponent<Projectile>().damage = damage;

                // Set the target of the projectile to the target of the turret
                projectile.GetComponent<Projectile>().targetEnemy = target;

                // Set the time at which the turret last fired to the current time
                lastFired = Time.time;
            }

            // Deactivate the projectile prefab
            projectilePrefab.SetActive(false);
        }
    }
}