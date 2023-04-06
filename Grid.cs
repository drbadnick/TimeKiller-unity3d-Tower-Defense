using UnityEngine;

public class Grid : MonoBehaviour
{
    // The grid cell prefab that will be used to create the grid
    public GameObject gridCellPrefab;

    // The width and height of the grid
    public int width = 10;
    public int height = 10;

    // The main camera in the scene
    public Camera mainCamera;

    void Start()
    {
        // Loop through the width and height of the grid
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Convert the grid cell position to a world position
                Vector3 worldPosition = new Vector3(x, 0, y);

                // Convert the world position to a screen position
                Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

                // Check if the screen position is within the viewport
                if (screenPosition.x >= 0 && screenPosition.x <= Screen.width &&
                    screenPosition.y >= 0 && screenPosition.y <= Screen.height)
                {
                    // Create a new grid cell at the current position
                    GameObject gridCell = Instantiate(gridCellPrefab, worldPosition, Quaternion.identity);

                    // Set the parent of the grid cell to the grid object
                    gridCell.transform.SetParent(transform);
                }
            }
        }
    }
}