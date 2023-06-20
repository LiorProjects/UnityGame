using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using UnityEditor.PackageManager;

public class MongoDBManager : MonoBehaviour
{
    private const string MONGO_URI = "mongodb+srv://liorbuddha:liors1234@cluster0.leplnhi.mongodb.net/?retryWrites=true&w=majority";
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
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    //Create the connection for database
    private void Start()
    {
        //MongoDB connection
        _client = new MongoClient(MONGO_URI);
        _database = _client.GetDatabase(DATABASE_NAME);
        IMongoCollection<User_def> mongoCollection = _database.GetCollection<User_def>("users", null);
        List<User_def> usersList = mongoCollection.Find(user => true).ToList();
        User_def[] ud = usersList.ToArray();
    }
    //Get the database
    public IMongoDatabase GetDatabase()
    {
        return _database;
    }
}
