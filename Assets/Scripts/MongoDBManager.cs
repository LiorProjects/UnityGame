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

    public void OnApplicationQuit()
    {
        IMongoCollection<User_def> mongoCollection = this._database.GetCollection<User_def>("users", null);
        List<User_def> usersList = mongoCollection.FindSync(user => true).ToList();
        var filter = Builders<User_def>.Filter.Eq("name", PlayerPrefs.GetString("user_name"));
        var update = Builders<User_def>.Update.Set("status", "Offline");
        mongoCollection.UpdateOne(filter, update);
    }
}
