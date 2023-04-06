using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;



public class LevelManager : MonoBehaviour
{
    // Array of levels, each level should have a unique name and unlock status
    public Level[] levels;

    // The current level that the player is playing
    public int currentLevel = -1;

    // The player's data
    public PlayerData playerData; 
    
    public bool IsLevelUnlocked(string levelName)
    {
        // Loop through the levels array
        for (int i = 0; i < levels.Length; i++)
        {
            // If the name of the level matches the given level name
            if (levels[i].name == levelName)
            {
                // Return whether the level is unlocked or not
                return levels[i].unlocked;
            }
        }

        // Return false if the level with the given name was not found in the array
        return false;
    }




    void Start()
    {


        // Save the player's data
        SavePlayerData(playerData);
    }



    public void LoadLevelIfUnlocked(string levelName)
    {
        // Loop through the levels array
        for (int i = 0; i < levels.Length; i++)
        {
            // If the name of the level matches the given level name
            if (levels[i].name == levelName)
            {
                // If the level is unlocked
                if (levels[i].unlocked)
                {
                    // Deactivate the menu
                    // Code to deactivate the menu goes here

                    // Load the level with the name of the scene associated with the level
                    SceneManager.LoadScene(levels[i].sceneName);
                }
            }
        }
    }

    public void LoadLevel(string levelName)
    {
        // Loop through the levels array
        for (int i = 0; i < levels.Length; i++)
        {
            // If the name of the level matches the given level name
            if (levels[i].name == levelName)
            {
                // If the level is unlocked
                if (levels[i].unlocked)
                {
                    // Load the level by its scene name
                    SceneManager.LoadScene(levels[i].sceneName);
                }
            }
        }
    }

    // Function to unlock a level by its name
    public void UnlockLevel(string levelName)
    {
        // Loop through the levels array
        for (int i = 0; i < levels.Length; i++)
        {
            // If the name of the level matches the given level name
            if (levels[i].name == levelName)
            {
                // Unlock the level
                levels[i].unlocked = true;
                // Save the player's data
                SavePlayerData(playerData);
                return;
            }
        }
    }

    // Function to add coins to the player's coin balance
    public void AddCoins(int amount)
    {
        playerData.coins += amount;
        // Save the player's data
        SavePlayerData(playerData);
    }



    // Function to remove coins from the player's coin balance
    public void RemoveCoins(int amount)
    {
        playerData.coins -= amount;
        // Save the player's data
        SavePlayerData(playerData);
    }
    // Function to save the player's data
    void SavePlayerData(PlayerData data)
    {
        // Serialize the player's data
        string json = JsonUtility.ToJson(data);
        // Save the serialized data to a file
        File.WriteAllText(Application.persistentDataPath + "/player.json", json);
    }

    // Function to load the player's saved data
    PlayerData LoadPlayerData()
    {
        // Check if the save file exists
        if (File.Exists(Application.persistentDataPath + "/player.json"))
        {
            // Read the contents of the save file
            string json = File.ReadAllText(Application.persistentDataPath + "/player.json");
            // Deserialize the player's data
            return JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            // If the save file doesn't exist, create a new PlayerData object
            return new PlayerData();
        }
    }

}