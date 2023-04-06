using UnityEngine;
using UnityEngine.UI;

public class TowerButtonScript : MonoBehaviour
{
	public int towerButton;

	private TowerBuilder towerBuilder;

	private void Start()
	{
		towerBuilder = FindObjectOfType<TowerBuilder>();
	}

	public void OnClick()
	{
		towerBuilder.SetCurrentTowerButton(towerButton);
	}
}
