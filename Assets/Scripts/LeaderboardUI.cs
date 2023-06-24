using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using MongoDB.Driver;

public class LeaderboardUI : MonoBehaviour
{
    private Button backBtn;
    private MongoDBManager mongoManager;
    private IMongoDatabase database;
    private User_def[] user_array;
    void Start()
    {
        mongoManager = gameObject.AddComponent<MongoDBManager>();
        var root = GetComponent<UIDocument>().rootVisualElement;
        backBtn = root.Q<Button>("back-btn");
        backBtn.clicked += backToMenu;
        //top 10 scores from all users
        Score[] leaderScores = mongoManager.getTopLeaderScores();

    }
    //Back to main menu
    private void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
