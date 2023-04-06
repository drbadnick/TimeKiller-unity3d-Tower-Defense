using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    // Reference to the levels game object
    public GameObject levels;

    // Reference to the store game object
    public GameObject store;

    // Reference to the info game object
    public GameObject info;

    // Function called when the "Start" button is clicked
    public void OpenLevels()
    {
        // Enable the levels game object
        levels.SetActive(true);
    }

    // Function called when the "Shop" button is clicked
    public void OpenStore()
    {
        // Enable the store game object
        store.SetActive(true);
    }

    // Function called when the "Info" button is clicked
    public void OpenInfo()
    {
        // Enable the info game object
        info.SetActive(true);
    }

}