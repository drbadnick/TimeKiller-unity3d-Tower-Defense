using UnityEngine;
using UnityEngine.UI;


public class LevelButton : MonoBehaviour
{
    // Reference to the LevelManager
    public LevelManager levelManager;

    // The name of the level associated with this button
    public string levelName;

    // The Image component of this button
    Image image;

    void Start()
    {
        // Set the levelManager variable to reference the LevelManager component
        levelManager = FindObjectOfType<LevelManager>();

        // If the level is unlocked
        if (levelManager.IsLevelUnlocked(levelName))
        {
            // Enable the button
            GetComponent<Button>().interactable = true;
        }
        else
        {
            // Disable the button and paint it black
            GetComponent<Button>().interactable = false;
            GetComponent<Image>().color = Color.black;
        }
    }

    // Function called when the button is clicked
    public void OnClick()
    {
        // Load the level using the LevelManager's LoadLevel method
        levelManager.LoadLevel(levelName);
    }
}