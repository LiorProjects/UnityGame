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
    string[] playerScore = { "player1", "player2", "player3", "player4", "player5", "player6", "player7", "player8" };
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
                    //Label playerScore = new Label();
                    //Label playerName = new Label();
                    //playerName.text = "Name: " + score.user_name;
                    //playerName.style.top = 230 + scoreSpace;
                    //playerName.AddToClassList("Scores");
                    //parentElement.Add(playerName);
                    //playerScore.text = "Score: " + score.score;
                    //playerScore.style.top = 230 + scoreSpace;
                    //playerScore.style.left = 1050;
                    //playerScore.AddToClassList("Scores");
                    //parentElement.Add(playerScore);
                    //scoreSpace += 70;
                    Label newScore = new Label();
                    newScore = root.Q<Label>(playerScore[numbersOfScoresToDisplay]);
                    newScore.text = "Score: " + score.score + "\tName: " + score.user_name;
                    parentElement.Add(newScore);
                    newScore.style.display = DisplayStyle.Flex;
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
