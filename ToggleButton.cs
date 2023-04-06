using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
	public GameObject targetObject;

	public void Toggle()
	{
		targetObject.SetActive(!targetObject.activeSelf);
	}
}