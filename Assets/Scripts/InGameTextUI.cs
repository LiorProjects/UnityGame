using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameTextUI : MonoBehaviour
{
    public Label playerScore;
    public Label playerCoins;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        playerScore = root.Q<Label>("player-score");
        playerCoins = root.Q<Label>("player-coins");
    }

    public void scoreText(int score)
    {
        playerScore.text = "Score: " + score;
    }
    public void coinsText(int coins)
    {
        playerCoins.text = "Coins: " + coins;
    }
}