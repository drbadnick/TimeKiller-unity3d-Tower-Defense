using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.AI;
using static Enemy;
using System.Collections;

public class GameManager : MonoBehaviour
{
	// The number of lives that the player starts the game with
	public int startingLives = 10;

	// The amount of silver coins that the player has
	public int silverCoins;

	// The UI element that displays the player's current silver coin balance
	public Text silverCoinText;

	// The UI element that displays the player's current number of lives
	public Text livesText;

	// The UI element that displays the "Defeat" screen
	public GameObject defeatScreen;

	// The number of seconds to wait before returning to the main menu after losing
	public float defeatScreenDuration = 5f;

	// The static instance of the GameManager class
	public static GameManager instance;

	// The number of lives that the player currently has
	public int lives;

	// Audio source for playing sound effects
	public AudioSource audioSource;

	// Sound effect for when an enemy is killed
	public AudioClip enemyDeathSound;

	// Sound effect for when silver coins are earned
	public AudioClip coinEarnedSound;

	void Awake()
	{
		// Initialize the static instance of the GameManager class
		instance = this;
	}

	void Start()
	{
		// Initialize the player's lives
		lives = startingLives;

		// Update the UI to show the player's current number of lives
		UpdateLivesText();
	}

	// Function to be called when an enemy is killed
	public void OnEnemyKilled(int worth)
	{
		// Add the enemy's worth to the player's balance
		silverCoins += worth;

		// Update the UI to show the player's current silver coin balance
		UpdateSilverCoinsText();

		// Play the enemy death sound
		audioSource.PlayOneShot(enemyDeathSound);
	}

	// Function to be called when an enemy reaches the castle
	public void OnEnemyReachesCastle()
	{
		// Decrement the player's lives
		lives--;

		// Update the UI to show the player's current number of lives
		UpdateLivesText();

		// IfIf the player has no more lives
		if (lives <= 0)
		{
			// Show the defeat screen
			defeatScreen.SetActive(true);

		        // After the specified amount of time, return to the main menu
		StartCoroutine(ReturnToMenu());
		}
	}

	// Coroutine to return to the main menu after the defeat screen has been shown
	IEnumerator ReturnToMenu()
	{
		// Wait for the specified amount of time
		yield return new WaitForSeconds(defeatScreenDuration);

		// Return to the main menu
		// Code to return to the main menu goes here
	}
	// Function to update the UI to show the player's current number of lives
	void UpdateLivesText()
	{
		// Set the text of the livesText UI element to show the player's current number of lives

		if (livesText != null)
			livesText.text = "Lives: " + lives;
	}

	// Function to update the UI to show the player's current silver coin balance
	public void UpdateSilverCoinsText()
	{
		// Set the text of the silverCoinText UI element to show the player's current silver coin balance
		if (silverCoinText != null)
			silverCoinText.text = "Silver Coins: " + silverCoins;
		audioSource.pitch = Random.Range(1.7f, 1.8f);
		audioSource.PlayOneShot(coinEarnedSound);
	}
}