using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameTextUI : MonoBehaviour
{
    public Label playerScore;
    public Label playerCoins;
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        playerScore = root.Q<Label>("player-score");
        playerCoins = root.Q<Label>("player-coins");
    }
    //Function to display score on screen
    public void scoreText(int score)
    {
        playerScore.text = "Score: " + score;
    }
    //Function to display coins on screen
    public void coinsText(int coins)
    {
        playerCoins.text = "Coins: " + coins;
    }
}