using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
	public float timer;

	void Start()
	{
		Invoke("DestroyPrefab", timer);
	}

	void DestroyPrefab()
	{
		Destroy(gameObject);
	}

	void OnDestroy()
	{
		Destroy(gameObject);
	}
}