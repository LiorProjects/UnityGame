using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    private Button playBtn;
    private Button shopBtn;
    private Button scoreBtn;
    private Button leaderboardBtn;
    //Display buttons on screen
    void Start()
    {
        MongoDBManager.SaveStatus();
        var root = GetComponent<UIDocument>().rootVisualElement;
        playBtn = root.Q<Button>("play-btn");
        shopBtn = root.Q<Button>("shop-btn");
        scoreBtn = root.Q<Button>("personal-score-btn");
        leaderboardBtn = root.Q<Button>("leaderboard-btn");

        playBtn.clicked += StartGame;
        shopBtn.clicked += Shop;
        scoreBtn.clicked += Score;
        leaderboardBtn.clicked += Leaderboard;
    }

    void StartGame()
    {
        SceneManager.LoadScene("BirdJumper");
    }
    void Shop()
    {
        SceneManager.LoadScene("Shop");
    }
    void Score()
    {
        SceneManager.LoadScene("Score");
    }
    void Leaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
}
