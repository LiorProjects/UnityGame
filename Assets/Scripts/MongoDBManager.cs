using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;

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
    public void insertNewUserToDB(User_def newUser)
    {
        IMongoCollection<User_def> userCollection = this._database.GetCollection<User_def>("users");
        userCollection.InsertOne(newUser);
    }
}
