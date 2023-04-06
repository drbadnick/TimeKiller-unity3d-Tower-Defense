using UnityEngine;

public class Rotator : MonoBehaviour
{
	// The detection range of the tower in meters
	public float detectionRange = 10;

	// The rotation speed of the tower in degrees per second
	public float rotationSpeed = 10;

	// The tag of the objects to detect
	public string detectionTag = "Enemy";

	// Function to find the nearest object with the specified tag within the detection range of the tower
	GameObject GetNearestObject()
	{
		// The nearest object found so far
		GameObject nearestObject = null;

		// The distance to the nearest object found so far
		float nearestDistance = float.MaxValue;

		// Find all objects with the specified tag
		GameObject[] objects = GameObject.FindGameObjectsWithTag(detectionTag);

		// Iterate through each of the found objects
		foreach (GameObject obj in objects)
		{
			// Calculate the distance between the tower and the current object
			float distance = Vector3.Distance(transform.position, obj.transform.position);

			// If the distance is within the detection range and is less than the nearest distance found so far
			if (distance <= detectionRange && distance < nearestDistance)
			{
				// Set the nearest object to the current object
				nearestObject = obj;

				// Set the nearest distance to the distance between the tower and the current object
				nearestDistance = distance;
			}
		}

		// Return the nearest object
		return nearestObject;
	}



	void Update()
	{
		// Find the nearest object within the detection range of the tower
		GameObject nearestObject = GetNearestObject();

		// If the nearest object is not null
		if (nearestObject != null)
		{
			// Rotate the tower around its y-axis by 180 degrees
			transform.RotateAround(transform.position, transform.up, 180);

			// Rotate the tower towards the nearest object
			transform.LookAt(nearestObject.transform, transform.up);
		}
	}
}