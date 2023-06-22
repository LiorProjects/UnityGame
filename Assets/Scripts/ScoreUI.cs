using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using MongoDB.Driver;
using MongoDB.Bson;

public class ScoreUI : MonoBehaviour
{
    private Score[] userScores;
    private Button backBtn;
    private IMongoDatabase database;
    private Label playerTextScore;
    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        backBtn = root.Q<Button>("back-btn");
        playerTextScore = root.Q<Label>("playe-text-score");
        this.database = MongoDBManager.Instance.GetDatabase();
        backBtn.clicked += backToMenu;
        // Connect to MongoDB
        IMongoCollection<User_def> mongoCollection = database.GetCollection<User_def>("users", null);
        var filter = Builders<User_def>.Filter.Eq("name", PlayerPrefs.GetString("user_name"));

        List<User_def> usersList = mongoCollection.FindSync(filter).ToList();
        var ausersList = mongoCollection.Find(FilterDefinition<User_def>.Empty).ToList();
        User_def[] userData = usersList.ToArray();
        User_def user_Def = userData[0];
        Score[] userScoreaFromDB = user_Def.scores;
        for(int i = 0; i < userScoreaFromDB.Length; i++)
        {
            Debug.Log(userScoreaFromDB[i].date + "   " + userScoreaFromDB[i].score);
        }
    }
    private void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
