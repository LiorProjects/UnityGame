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
    private Label noUserLogin;
    int numbersOfScoresToDisplay = 8;
    string[] playerScore = { "score1", "score2", "score3", "score4", "score5", "score6", "score7", "score8"};
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var parentElement = root.Q<VisualElement>("parent-element");
        noUserLogin = root.Q<Label>("no-user-login");

        backBtn = root.Q<Button>("back-btn");
        backBtn.clicked += backToMenu;
        //no user loged in
        if (PlayerPrefs.GetString("user_name") != "")
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
                newScore = root.Q<Label>(playerScore[i]);
                newScore.text = "Score: " + userScoreaFromDB[i + nextPosition].score + "\tDate: " + userScoreaFromDB[i + nextPosition].date;
                parentElement.Add(newScore);
                newScore.style.display = DisplayStyle.Flex;
            }
        }
        else
        {
            noUserLogin.style.display = DisplayStyle.Flex;
        }
    }
    private void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
