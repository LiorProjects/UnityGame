using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameTextUI : MonoBehaviour
{
    public Label playerScore;
    public Label playerCoins;
    public Label playerMaxScore;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        playerScore = root.Q<Label>("player_score");
        playerCoins = root.Q<Label>("player_coins");
        playerMaxScore = root.Q<Label>("player_max_score");

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
    //Function to display max_score on screen
    public void maxScoreText(int maxScore)
    {
        playerMaxScore.text = "Highest Score: " + maxScore;
    }
}