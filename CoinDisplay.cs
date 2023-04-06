using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    // Reference to the text component that will display the coin balance
    public Text coinText;

    // Reference to the LevelManager component in the scene
    public LevelManager levelManager;

    void Start()
    {
        // Set the initial text of the coinText component
        UpdateCoinText();
    }

    // Function to update the text of the coinText component
    void UpdateCoinText()
    {
        // Set the text of the coinText component to the player's coin balance
        coinText.text = "Coins: " + levelManager.playerData.coins.ToString();
    }
}