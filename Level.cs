using System.Collections.Generic;
using UnityEngine;

// The Level class inherits from the MonoBehaviour class
public class Level : MonoBehaviour
{
    // The name of the level
    public new string name;

    // A flag that indicates whether the level is unlocked
    public bool unlocked;

    public string sceneName;

    // The number of waves in the level
    public int numWaves;

    // A list of enemy data, with one entry for each enemy in the level
    public List<EnemyData> enemies;

    // Other level properties, such as amount of coins earned, etc.

    void Start()
    {
        // Create a new list of enemy data
        enemies = new List<EnemyData>();
    }
}


