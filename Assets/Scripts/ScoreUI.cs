using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using MongoDB.Driver;
using MongoDB.Bson;
using System;

public class ScoreUI : MonoBehaviour
{
    private Button backBtn;
    private IMongoDatabase database;
    int scoreSpace = 70;
    int numbersOfScoresToDisplay = 8;
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var parentElement = root.Q<VisualElement>("parent-element");
        backBtn = root.Q<Button>("back-btn");
        backBtn.clicked += backToMenu;
        if (PlayerPrefs.GetString("user_name") != null)
        {
            // Connect to MongoDB
            this.database = MongoDBManager.Instance.GetDatabase();
            IMongoCollection<User_def> mongoCollection = database.GetCollection<User_def>("users", null);
            var filter = Builders<User_def>.Filter.Eq("name", PlayerPrefs.GetString("user_name"));
            List<User_def> usersList = mongoCollection.FindSync(filter).ToList();
            var ausersList = mongoCollection.Find(FilterDefinition<User_def>.Empty).ToList();
            User_def[] userData = usersList.ToArray();

            User_def user_Def = userData[0];
            Score[] userScoreaFromDB = user_Def.scores;

            //display score list on screen
            for (int i = 0; i < userScoreaFromDB.Length && i < numbersOfScoresToDisplay; i++)
            {
                //int nextPosition sets the last 8 scores the user has
                int nextPosition = Math.Max(userScoreaFromDB.Length - numbersOfScoresToDisplay, 0);
                Label newScore = new Label();
                newScore.text = "Score: " + userScoreaFromDB[i + nextPosition].score + "\tDate: " + userScoreaFromDB[i + nextPosition].date;
                newScore.style.top = 230 + scoreSpace;
                newScore.AddToClassList("Scores");
                parentElement.Add(newScore);
                scoreSpace += 70;
            }

        }
        else
        {
            Label newScore = new Label();
            newScore.text = "You must sign up to see this page!";
            newScore.style.top = 230 + scoreSpace;
            newScore.AddToClassList("Scores");
            parentElement.Add(newScore);
            scoreSpace += 70;
        }
    }
    private void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
