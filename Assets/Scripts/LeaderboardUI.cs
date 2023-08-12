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
    int scoreSpace = 70;
    int numbersOfScoresToDisplay = 0;
    private Label noUserLogin;
    string[] playerName = { "player1", "player2", "player3", "player4", "player5", "player6", "player7", "player8" };
    string[] playerScore = { "score1", "score2", "score3", "score4", "score5", "score6", "score7", "score8" };
    void Start()
    {
        mongoManager = gameObject.AddComponent<MongoDBManager>();
        var root = GetComponent<UIDocument>().rootVisualElement;
        var parentElement = root.Q<VisualElement>("parent-element");
        noUserLogin = root.Q<Label>("no-user-login");
        backBtn = root.Q<Button>("back-btn");
        backBtn.clicked += backToMenu;
        //top 10 scores from all users
        Score[] leaderScores = mongoManager.getTopLeaderScores();
        if (PlayerPrefs.GetString("user_name") != "")
        {
            //display score list on screen
            foreach (Score score in leaderScores)
            {
                if(numbersOfScoresToDisplay != 8)
                {
                    Label newScore = new Label();
                    Label newName = new Label();
                    newScore = root.Q<Label>(playerScore[numbersOfScoresToDisplay]);
                    newName = root.Q<Label>(playerName[numbersOfScoresToDisplay]);
                    newScore.text = "" + score.score;
                    newName.text = "" + score.user_name;
                    parentElement.Add(newScore);
                    parentElement.Add(newName);
                    newScore.style.display = DisplayStyle.Flex;
                    newName.style.display = DisplayStyle.Flex;
                    numbersOfScoresToDisplay++;
                }
            }
        }
        else
        {
            noUserLogin.style.display = DisplayStyle.Flex;
        }

    }
    //Back to main menu
    private void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
