using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
	// The prefab of the enemy to spawn
	public GameObject enemyPrefab;

	// The number of enemies to spawn
	public int enemyCount = 10;

	// The interval between each enemy spawn
	public float spawnInterval = 1;

	// The duration of each wave
	public float waveDuration = 10;

	// The UI element that displays the current wave
	public Text waveText;

	// The UI element that displays the enemies left in the wave
	public Text enemiesLeftText;

	// The UI element that displays the countdown to the next wave
	public Text waveCountdownText;

	// The list of prefabs for each wave
	public List<GameObject> wavePrefabs;

	// The current wave number
	private int wave = 1;

	// The number of enemies to spawn per wave
	public int enemiesPerWave = 10;

	// The audio source to play the sound when an enemy is spawned
	public AudioSource audioSource;

	// Function called when the spawner is created
	void Start()
	{
		// Assign a value to the projectilePrefab variable
		enemyPrefab = Resources.Load("Enemy1") as GameObject;
		// Start the first wave
		StartCoroutine(SpawnWave());
	}

	IEnumerator SpawnWave()
	{
		// Update the UI to show the current wave number
		UpdateWaveText();

		// Reset the enemy count
		ResetEnemyCount();

		// Loop through the prefabs for the current wave
		foreach (GameObject prefab in wavePrefabs)
		{
			// Loop for the number of enemies to spawn
			for (int i = 0; i < enemiesPerWave; i++)
			{
				// Check if the prefab still exists
				if (prefab != null)
				{
					// Activate the prefab
					prefab.SetActive(true);
					// Spawn the enemy at the spawner's position
					Instantiate(prefab, transform.position, Quaternion.identity);
					// Deactivate the prefab
					prefab.SetActive(false);

					// Update the UI to show the enemies left in the wave
					UpdateEnemiesLeftText();

					// Play the sound from the audio source
					audioSource.Play();
				}

				// Wait for the specified amount of time before spawning the next enemy
				yield return new WaitForSeconds(spawnInterval);

				// Decrement the enemy count
				enemyCount--;
			}

			// Activate the wave countdown text
			waveCountdownText.gameObject.SetActive(true);

			// Start the countdown
			float countdown = waveDuration;
			while (countdown > 0)
			{
				// Update the UI to show the countdown
				waveCountdownText.text = "Next Wave in: " + countdown.ToString("F0");

				// Wait for one second
				yield return new WaitForSeconds(1);

				// Decrement the countdown
				countdown--;
			}

			// Deactivate the wave countdown text
			waveCountdownText.gameObject.SetActive(false);

			// Increment the wave number
			wave++;
		}

		// If all waves have been completed, do something here
		// Code goes here
	}


	// Function to update the UI to show the current wave number
	void UpdateWaveText()
	{
		// Set the text of the waveText UI element to show the current wave number
		waveText.text = "Wave: " + wave;
	}

	// Function to update the UI to show the enemies left in the wave
	void UpdateEnemiesLeftText()
	{
		// Set the text of the enemiesLeftText UI element to show the enemies left in the wave
		enemiesLeftText.text = "Enemies Left: " + enemyCount;
	}

	// Function to reset the enemy count
	void ResetEnemyCount()
	{
		// Set the enemy count to the enemies per wave value
		enemyCount = enemiesPerWave;
	}
}