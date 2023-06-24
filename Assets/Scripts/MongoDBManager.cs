using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using System.Data.Common;
using MongoDB.Bson;
using System;
using System.Linq;

public class MongoDBManager : MonoBehaviour
{
    //private const string MONGO_URI = "mongodb+srv://liorbuddha:liors1234@cluster0.leplnhi.mongodb.net/?retryWrites=true&w=majority";
    private const string MONGO_URI = "mongodb://liorbuddha:liors1234@ac-ojekpeb-shard-00-00.leplnhi.mongodb.net:27017,ac-ojekpeb-shard-00-01.leplnhi.mongodb.net:27017,ac-ojekpeb-shard-00-02.leplnhi.mongodb.net:27017/?ssl=true&replicaSet=atlas-3zex7x-shard-0&authSource=admin&retryWrites=true&w=majority";
    private const string DATABASE_NAME = "birdDB";
    private static MongoDBManager instance;
    private IMongoClient _client;
    private IMongoDatabase _database;

    public static MongoDBManager Instance
    {
        get { return instance; }
    }
    //Don't close the database on scene change
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //Create the connection for database
        _client = new MongoClient(MONGO_URI);
        if(_database == null)
            _database = _client.GetDatabase(DATABASE_NAME);
    }
    //Connect to MongoDB
    private void Start()
    {
       
    }
    //Get the database
    public IMongoDatabase GetDatabase()
    {
        return _database;
    }
    //Set the player to Offline status when the game is closed
    public void OnApplicationQuit()
    {
        IMongoCollection<User_def> mongoCollection = this._database.GetCollection<User_def>("users", null);
        List<User_def> usersList = mongoCollection.FindSync(user => true).ToList();
        var filter = Builders<User_def>.Filter.Eq("name", PlayerPrefs.GetString("user_name"));
        var update = Builders<User_def>.Update.Set("status", "Offline");
        mongoCollection.UpdateOne(filter, update);
    }
    public User_def[] getAllUsers()
    {
        IMongoCollection<User_def> mongoCollection = this._database.GetCollection<User_def>("users", null);
        List<User_def> usersList = mongoCollection.Find(user => true).ToList();
        User_def[] ud = usersList.ToArray();
        return ud;
    }
    public User_def getUserByUserName(string user_name)
    {
        IMongoCollection<User_def> mongoCollection = this._database.GetCollection<User_def>("users", null); 
        var filter = Builders<User_def>.Filter.Eq("name", PlayerPrefs.GetString("user_name"));
        User_def user = mongoCollection.Find(filter).FirstOrDefault();
        return user;
    }
    //add new user to DB
    public void insertNewUserToDB(User_def newUser)
    {
        IMongoCollection<User_def> userCollection = this._database.GetCollection<User_def>("users");
        userCollection.InsertOne(newUser);
    }
    //Update user coins
    public void updateUserCoins(string user_name, int numOfCoins)
    {
        IMongoCollection<User_def> mongoCollection = _database.GetCollection<User_def>("users");
        var filter = Builders<User_def>.Filter.Eq("name", user_name);
        var update = Builders<User_def>.Update.Set("coins_count", (PlayerPrefs.GetInt("user_coins")-numOfCoins));
        mongoCollection.UpdateOne(filter, update);
    }
    //Add bird to user in DB
    public void addNewBirdToUser(string user_name, string birdName)
    {
        // Find and update the user's birds
        IMongoCollection<User_def> mongoCollection = _database.GetCollection<User_def>("users");
        User_def user = getUserByUserName(user_name);
        var filter = Builders<User_def>.Filter.Eq("name", user.name);
        string[] user_birds = user.birds ?? (new string[0]);
        Debug.Log("birds array:");
        Debug.Log("is bird found in user's birds array?: "+user_birds.Contains(birdName));
        if(!user_birds.Contains(birdName))
        {
            user_birds.Append(birdName);
            var update = Builders<User_def>.Update.Push("birds", birdName);
            mongoCollection.UpdateOne(filter, update);
        }
        
        
    }
    //get top scores
    public Score[] getTopLeaderScores()
    {
        User_def[] users = getAllUsers();
        Score[] scores = new Score[0];
        for(int i = 0; i < users.Length; i++)
        {
            for(int j=0; j < users[i].scores.Length; j++)
            {
                scores.Append(users[i].scores[j]);
            }
        }
        for(int i =0; i < scores.Length; i++)
        {
            for(int j = 0;j<scores.Length-1; j++)
            {
                if (scores[j].score > scores[j + 1].score)
                {
                    Score tmp = scores[j];
                    scores[j] = scores[j + 1];
                    scores[j+1] = tmp;
                }
            }
        }
        Score[] orderedByScore = new Score[10];
        for (int i = 0; i < orderedByScore.Length; i++)
        {
            orderedByScore[i] = scores[i + 1 * -1];
        }
        return orderedByScore;
    }
}
