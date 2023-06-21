using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using MongoDB.Driver;
using UnityEngine.Networking;
using MongoDB.Bson;
using System.Text;



public class RegisterAndLogin : MonoBehaviour
{
    // Start is called before the first frame update
    private VisualElement register;
    private VisualElement login;
    private TextField usernameRegisterField;
    private TextField passwordRegisterField;
    private TextField usernameLoginField;
    private TextField passwordLoginField;
    private TextField ageField;
    private Button submitBtn;
    private Button registerLoginBtn;
    private Button loginBtn;
    private Button backBtn;
    private IMongoDatabase database;
    private User_def[] usersListArray;

    void Start()
    {
        //openDB();
        database = MongoDBManager.Instance.GetDatabase();
        //Debug.Log(database);
        var root = GetComponent<UIDocument>().rootVisualElement;
        register = root.Q<VisualElement>("register");
        login = root.Q<VisualElement>("login");

        usernameRegisterField = root.Q<TextField>("username-register-field");
        passwordRegisterField = root.Q<TextField>("password-register-field");
        usernameLoginField = root.Q<TextField>("username-login-field");
        passwordLoginField = root.Q<TextField>("password-login-field");
        ageField = root.Q<TextField>("age-field");

        registerLoginBtn = root.Q<Button>("register-login-btn");
        loginBtn = root.Q<Button>("login-btn");
        backBtn = root.Q<Button>("back-login-btn");
        submitBtn = root.Q<Button>("submit-btn");

        submitBtn.clicked += enterGameAfterRegister;
        loginBtn.clicked += enterGameAfterLogin;
        registerLoginBtn.clicked += toLoginMenu;
        backBtn.clicked += backToRegister;
    }
    private void toLoginMenu()
    {
        register.style.display = DisplayStyle.None;
        login.style.display = DisplayStyle.Flex;
    }
    private void backToRegister()
    {
        register.style.display = DisplayStyle.Flex;
        login.style.display = DisplayStyle.None;
    }
    private void enterGameAfterRegister()
    {
        bool isTaken = false;
        IMongoCollection<User_def> mongoCollection = database.GetCollection<User_def>("users", null);
        List<User_def> usersList = mongoCollection.Find(user => true).ToList();
        usersListArray = usersList.ToArray();
        //A loop that checks if the username is taken or not
        foreach (User_def userName in usersListArray)
        {
            if (userName.name == usernameRegisterField.text)
            { 
                isTaken = true;
                break;
            }
        }
        if (usernameRegisterField.text.Length < 2 || usernameRegisterField.text.Length > 32 || isTaken)
        {
            if (isTaken)
                Debug.Log("Username is take try another one");
            else
                Debug.Log("Username must be between 2 and 32 characters long");

            //Need to check if the user is taken in DB
        }
        else if (passwordRegisterField.text.Length < 8 || passwordRegisterField.text.Length > 32)
        {
            Debug.Log("Password must be between 8 and 32 characters long");
        }
        else if (ageField.text.Length < 1 || ageField.text.Length > 2 || !ageField.text.All(char.IsDigit))
        {
            Debug.Log("Age must be between 4 and 99 and only numbers");
        }
        else
        {
            //insert new user to DB
            IMongoCollection<User_def> userCollection = database.GetCollection<User_def>("users");
            User_def newUser = new();
            newUser.name = usernameRegisterField.text;
            newUser.password = passwordRegisterField.text;
            newUser.age = int.Parse(ageField.text);
            newUser.coins_count = 0;
            newUser.max_score = 0;
            newUser.status = "Online";
            newUser.scores = new();
            userCollection.InsertOne(newUser);
            PlayerPrefs.SetString("user_name", usernameRegisterField.text);
            PlayerPrefs.SetInt("user_coins", newUser.coins_count);
            PlayerPrefs.SetInt("user_max_score", newUser.max_score);
            SceneManager.LoadScene("MainMenu");
        }

    }
    //Login
    //checks if user exists
    //check if password matches the one in the DB
    private void enterGameAfterLogin()
    {
        //db client
        IMongoCollection<User_def> mongoCollection = database.GetCollection<User_def>("users", null);
        List<User_def> usersList = mongoCollection.FindSync(user => true).ToList();
        var ausersList = mongoCollection.Find(FilterDefinition<User_def>.Empty).ToList();
        User_def[] userData = usersList.ToArray();
        foreach (User_def userName in userData)
        {
            //if username matches password, continue to main menu
            if (userName.name == usernameLoginField.text && userName.password == passwordLoginField.text)
            {
                PlayerPrefs.SetString("user_name", usernameLoginField.text);
                PlayerPrefs.SetInt("user_coins", userName.coins_count);
                PlayerPrefs.SetInt("user_max_score", userName.max_score);
                SceneManager.LoadScene("MainMenu");
                Debug.Log(usernameLoginField.text + "Login");
                var filter = Builders<User_def>.Filter.Eq("name", PlayerPrefs.GetString("user_name"));
                var update = Builders<User_def>.Update.Set("status", "Online");
                mongoCollection.UpdateOne(filter, update);
                break;
            }
            else
            {
                Debug.Log(usernameLoginField.text + "faild to login");
            }
        }
    }
}
