using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int totalCoins = 0;

    void Start()
    {
        UpdateCoinText();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            // Increment the total coin count
            totalCoins++;

            // Update the TextMeshPro text
            UpdateCoinText();

            // Destroy the coin GameObject
            Destroy(other.gameObject);
        }
    }

    void UpdateCoinText()
    {
        // Update the TextMeshPro text with the total coin count
        coinText.text = "Coins: " + totalCoins.ToString();
    }
}
