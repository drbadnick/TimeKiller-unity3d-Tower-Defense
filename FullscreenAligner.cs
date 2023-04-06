using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.AI;
using static Enemy;
using System.Collections;

public class FullscreenAligner : MonoBehaviour
{
    private RectTransform rectTransform;

    // Variables to specify the anchored position of the interface element in fullscreen and windowed mode
    [SerializeField] private Vector2 fullscreenAnchoredPosition = new Vector2(0, 0);
    [SerializeField] private Vector2 windowedAnchoredPosition = new Vector2(0, 0);

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Screen.fullScreen)
        {
            // Use the specified anchored position for fullscreen mode
            rectTransform.anchoredPosition = fullscreenAnchoredPosition;
        }
        else
        {
            // Use the specified anchored position for windowed mode
            rectTransform.anchoredPosition = windowedAnchoredPosition;
        }
    }
}