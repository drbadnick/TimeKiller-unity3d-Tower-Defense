using UnityEngine;
using System.Collections;

public class CloseButton : MonoBehaviour
{

    // Reference to the game object that should be closed
    public GameObject targetObject;

    // Function called when the button is clicked
    public void OnClick()
    {
        // Disable the target object and its children
        targetObject.SetActive(false);
    }

}