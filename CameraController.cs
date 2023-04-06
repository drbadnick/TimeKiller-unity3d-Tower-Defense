using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float speed;
	public float edgeThreshold;
	public Bounds boundingBox; // Add a public Bounds variable to set the bounding box in the inspector
	public float panSpeed; // Add a public variable to control the panning speed

	private Vector3 mousePosition;
	private Vector3 touchPosition;
	private Vector3 moveDirection;

	void Update()
	{
		if (Input.touchCount > 0)
		{
			touchPosition = Input.GetTouch(0).position;

			if (touchPosition.x > Screen.width - edgeThreshold)
			{
				moveDirection = Vector3.right;
			}
			else if (touchPosition.x < edgeThreshold)
			{
				moveDirection = Vector3.left;
			}
			else if (touchPosition.y > Screen.height - edgeThreshold)
			{
				moveDirection = Vector3.forward;
			}
			else if (touchPosition.y < edgeThreshold)
			{
				moveDirection = Vector3.back;
			}
			else
			{
				moveDirection = Vector3.zero;
			}

			transform.parent.Translate(moveDirection * speed * Time.deltaTime);
		}
		else
		{
			mousePosition = Input.mousePosition;

			if (mousePosition.x > Screen.width - edgeThreshold)
			{
				moveDirection = Vector3.right;
			}
			else if (mousePosition.x < edgeThreshold)
			{
				moveDirection = Vector3.left;
			}
			else if (mousePosition.y > Screen.height - edgeThreshold)
			{
				moveDirection = Vector3.forward;
			}
			else if (mousePosition.y < edgeThreshold)
			{
				moveDirection = Vector3.back;
			}
			else
			{
				moveDirection = Vector3.zero;
			}

			if (Input.GetMouseButton(0))
			{
				transform.parent.Translate(moveDirection * speed * Time.deltaTime);
			}

			// Add middle mouse panning control
			if (Input.GetMouseButton(2))
			{
				float panX = Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
				float panZ = Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;
				transform.parent.Translate(panX, 0, panZ);
			}

			// Keep the camera inside the bounding box
			Vector3 clampedPosition = transform.parent.position;
			clampedPosition.x = Mathf.Clamp(transform.parent.position.x, boundingBox.min.x, boundingBox.max.x);
			clampedPosition.z = Mathf.Clamp(transform.parent.position.z, boundingBox.min.z, boundingBox.max.z);
			transform.parent.position = clampedPosition;
		}
	}
}