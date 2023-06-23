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
using System;

public class RegisterAndLogin : MonoBehaviour
{
    //variables
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
    private Button playWithoutLogin;
    private IMongoDatabase database;
    private User_def[] usersListArray;
    private MongoDBManager mongoManager;

    void Start()
    {
        database = MongoDBManager.Instance.GetDatabase();
        var root = GetComponent<UIDocument>().rootVisualElement;
        register = root.Q<VisualElement>("register");
        login = root.Q<VisualElement>("login");
        mongoManager = gameObject.AddComponent<MongoDBManager>();
        usernameRegisterField = root.Q<TextField>("username-register-field");
        passwordRegisterField = root.Q<TextField>("password-register-field");
        usernameLoginField = root.Q<TextField>("username-login-field");
        passwordLoginField = root.Q<TextField>("password-login-field");
        ageField = root.Q<TextField>("age-field");

        registerLoginBtn = root.Q<Button>("register-login-btn");
        loginBtn = root.Q<Button>("login-btn");
        backBtn = root.Q<Button>("back-login-btn");
        submitBtn = root.Q<Button>("submit-btn");
        playWithoutLogin = root.Q<Button>("play-without-login");

        submitBtn.clicked += enterGameAfterRegister;
        loginBtn.clicked += enterGameAfterLogin;
        registerLoginBtn.clicked += toLoginMenu;
        backBtn.clicked += backToRegister;
        playWithoutLogin.clicked += toMainMenu;
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
        //Checks if the user has internet
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("You can't play without internet");
        }
        else
        {
            bool isTaken = false;
            usersListArray = mongoManager.getAllUsers();
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
                User_def newUser = new();
                newUser.name = usernameRegisterField.text;
                newUser.password = passwordRegisterField.text;
                newUser.age = int.Parse(ageField.text);
                newUser.coins_count = 0;
                newUser.max_score = 0;
                newUser.status = "Online";
                newUser.scores = new Score[0];
                mongoManager.insertNewUserToDB(newUser);
                PlayerPrefs.SetString("user_name", usernameRegisterField.text);
                PlayerPrefs.SetInt("user_coins", newUser.coins_count);
                PlayerPrefs.SetInt("user_max_score", newUser.max_score);
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
    //Login
    //check if password matches the one in the DB
    private void enterGameAfterLogin()
    {
        //Checks if the user has internet
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("You can't play without internet");
        }
        else
        {
            //MOVE TO DB_MANAGER
            //db client
            IMongoCollection<User_def> mongoCollection = database.GetCollection<User_def>("users", null);
            User_def[] userData = mongoManager.getAllUsers();
            foreach (User_def userName in userData)
            {
                //if username matches password, continue to main menu
                if (userName.name == usernameLoginField.text && userName.password == passwordLoginField.text)
                {
                    //Checks if the user already in game
                    var filter1 = Builders<User_def>.Filter.Eq("name", usernameLoginField.text) & Builders<User_def>.Filter.Eq("status", "Online");
                    var user = mongoCollection.Find(filter1).FirstOrDefault();
                    if (user != null)
                    {
                        Debug.Log("This user already in game");
                        break;
                    }
                    PlayerPrefs.SetString("user_name", usernameLoginField.text);
                    PlayerPrefs.SetInt("user_coins", userName.coins_count);
                    PlayerPrefs.SetInt("user_max_score", userName.max_score);
                    SceneManager.LoadScene("MainMenu");
                    Debug.Log(usernameLoginField.text + "Login");
                    var filter = Builders<User_def>.Filter.Eq("name", PlayerPrefs.GetString("user_name"));
                    var update = Builders<User_def>.Update.Set("status", "Online");
                    mongoCollection.UpdateOne(filter, update);
                    Notifications.sendNotification("Welcome", "Welcome the user to the game", "BirdJumper", "Welcome Back " + usernameLoginField + "!", "icon_small", "icon_large", DateTime.Now.AddSeconds(2));
                    break;
                }
                else
                {
                    Debug.Log(usernameLoginField.text + "faild to login");
                }
            }
        }
    }
    void toMainMenu()
    {
        PlayerPrefs.SetString("user_name", null);
        SceneManager.LoadScene("MainMenu");
    }
}
