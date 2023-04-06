
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerBuilder : MonoBehaviour
{
	// The tower prefab that will be instantiated when the player clicks on a tower button
	public GameObject towerPrefab;
	public MeshRenderer towerMeshRenderer;

	// The building menu UI element
	public GameObject buildingMenu;

	// The tower buttons in the building menu
	public Button[] towerButtons;

	// The cost of each tower in silver coins
	public int[] towerCosts;

	// The minimum distance that towers must be placed apart from each other in meters
	public float minTowerDistance = 2f;

	// The UI element that displays the player's current silver coin balance
	public Text silverCoinText;

	// The distance from the ground at which the tower prefab will be instantiated
	public float towerInstantiateDistance = 0.5f;

	// The current tower button that the player has selected
	private int currentTowerButton = -1;

	// The tower prefabs for each type of tower
	public GameObject[] towerPrefabs;

	// The camera used to project the mouse position to the ground
	public Camera camera;

	public void SetCurrentTowerButton(int towerButton)
	{
		currentTowerButton = towerButton;
		towerPrefab = towerPrefabs[currentTowerButton];
		towerPrefab.SetActive(true);
		buildingMenu.SetActive(true);
	}

	void Update()
	{
		// If the player has clicked on a tower button
		if (currentTowerButton != -1)
		{
			// Get the position of the mouse in world space
			Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
			// Set the position of the tower prefab to the mouse position
			towerPrefab.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);


			// Raycast from the mouse position to the ground
			RaycastHit hit;
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, -5000, LayerMask.GetMask("Ground", "Tower")))
			{
				// Get the position of the mouse in world space
				Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
				// Set the position of the tower prefab to the point where the raycast hits the ground
				Vector3 hitPosition = new Vector3(hit.point.x, towerInstantiateDistance, hit.point.z);
				// Adjust the hit position to account for the camera's isometric view
				hitPosition = Quaternion.Euler(camera.transform.eulerAngles.x, camera.transform.eulerAngles.y, 0) * (hitPosition - mousePos) + mousePos;
				// Set the position of the tower prefab to the adjusted hit position
				towerPrefab.transform.position = hitPosition;
				towerPrefab.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
			}
			// If the player has clicked the mouse button
			if (Input.GetMouseButtonDown(0))
			{
				// Check if the player has enough silver coins to build the tower
				if (GameManager.instance.silverCoins >= towerCosts[currentTowerButton])
				{
					// Check if there are any towers within the minimum distance
					bool tooClose = false;
					Collider[] colliders = Physics.OverlapSphere(towerPrefab.transform.position, minTowerDistance);
					foreach (Collider collider in colliders)
					{
						if (collider.tag == "Tower")
						{
							tooClose = true;
							break;
						}
					}

					// If there are no towers within the minimum distance
					if (!tooClose)
					{
						// Instantiate a new tower at the mouse position
						GameObject tower = Instantiate(towerPrefab, towerPrefab.transform.position, towerPrefab.transform.rotation);
						Vector3 mouseCameraDifference = mousePosition - camera.transform.position;

						// Snap the tower to the ground
						Vector3 towerPosition = new Vector3(mousePosition.x, mousePosition.y, mousePosition.z) - mouseCameraDifference;
						towerPosition.y = 0;
						tower.transform.position = towerPosition;

						// Subtract the cost of the tower from the player's silver coin balance
						GameManager.instance.silverCoins -= towerCosts[currentTowerButton];

						// Update the UI to show the player's updated silver coin balance
						GameManager.instance.UpdateSilverCoinsText();

						// Reset the current tower button
						currentTowerButton = -1;
						towerPrefab.SetActive(false);
						buildingMenu.SetActive(false);

						// Set the color of the tower prefab back to white
					}
				}
				// If the player does not have enough silver coins to build the tower
				else
				{
					// Reset the current tower button
					currentTowerButton = -1;
					towerPrefab.SetActive(false);
					buildingMenu.SetActive(false);

					// Set the color of the tower prefab back to white
				}
			}
		}

		// If the player right-clicks
		if (Input.GetMouseButtonDown(1))
		{
			// Reset the current tower button
			currentTowerButton = -1;
			towerPrefab.SetActive(false);
			buildingMenu.SetActive(false);

			// Set the color of the tower prefab back to white
		}
	}
}