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
using System.Security.Cryptography;

public class RegisterAndLogin : MonoBehaviour
{
    //variables
    private VisualElement register;
    private VisualElement login;
    private VisualElement loginError;
    private VisualElement takenUsername;
    private VisualElement usernameLength;
    private VisualElement passwordLength;
    private VisualElement ageError;
    private VisualElement wrongPasswordOrUsername;
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
        loginError = root.Q<VisualElement>("error-login");
        takenUsername = root.Q<VisualElement>("taken-username-message");
        usernameLength = root.Q<VisualElement>("username-length-message");
        passwordLength = root.Q<VisualElement>("password-length-message");
        ageError = root.Q<VisualElement>("age-error-message");
        wrongPasswordOrUsername = root.Q<VisualElement>("wrong-password-or-username-login");
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

    //Turns the password into a hash for user security
    private string hashedPassword(string password)
    {
        var hash = SHA256.Create();
        byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(bytes);
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
        loginError.style.display = DisplayStyle.None;
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
                {
                    clearMessageOnScreen();
                    takenUsername.style.display = DisplayStyle.Flex;
                }
                else
                {
                    clearMessageOnScreen();
                    usernameLength.style.display = DisplayStyle.Flex;
                }
            }
            else if (passwordRegisterField.text.Length < 8 || passwordRegisterField.text.Length > 32)
            {
                clearMessageOnScreen();
                passwordLength.style.display = DisplayStyle.Flex;
            }
            else if (ageField.text.Length < 1 || ageField.text.Length > 2 || !ageField.text.All(char.IsDigit))
            {
                clearMessageOnScreen();
                ageError.style.display = DisplayStyle.Flex;
            }
            else
            {
                //insert new user to DB
                User_def newUser = new();
                newUser.name = usernameRegisterField.text;
                newUser.password = hashedPassword(passwordRegisterField.text);
                newUser.age = int.Parse(ageField.text);
                newUser.coins_count = 0;
                newUser.max_score = 0;
                newUser.status = "Online";
                newUser.scores = new Score[0];
                newUser.birds = new string[0];
                newUser.birdColor = "Default";
                mongoManager.insertNewUserToDB(newUser);
                PlayerPrefs.SetString("user_name", usernameRegisterField.text);
                PlayerPrefs.SetInt("user_coins", newUser.coins_count);
                PlayerPrefs.SetInt("user_max_score", newUser.max_score);
                PlayerPrefs.SetString("birdColor", "Default");
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
                if (userName.name == usernameLoginField.text && userName.password == hashedPassword(passwordLoginField.text))
                {
                    //Checks if the user already in game
                    var filter1 = Builders<User_def>.Filter.Eq("name", usernameLoginField.text) & Builders<User_def>.Filter.Eq("status", "Online");
                    var user = mongoCollection.Find(filter1).FirstOrDefault();
                    if (user != null)
                    {
                        loginError.style.display = DisplayStyle.Flex;
                        break;
                    }
                    PlayerPrefs.SetString("user_name", usernameLoginField.text);
                    PlayerPrefs.SetInt("user_coins", userName.coins_count);
                    PlayerPrefs.SetInt("user_max_score", userName.max_score);
                    PlayerPrefs.SetString("birdColor", userName.birdColor);
                    SceneManager.LoadScene("MainMenu");
                    Debug.Log(usernameLoginField.text + " Login");
                    var filter = Builders<User_def>.Filter.Eq("name", PlayerPrefs.GetString("user_name"));
                    var update = Builders<User_def>.Update.Set("status", "Online");
                    mongoCollection.UpdateOne(filter, update);
                    Notifications.sendNotification("Welcome", "Welcome the user to the game", "BirdJumper", "Welcome Back " + usernameLoginField.text + "!", "icon_small", "icon_large", DateTime.Now.AddSeconds(2));
                    loginError.style.display = DisplayStyle.None;
                    wrongPasswordOrUsername.style.display = DisplayStyle.None;
                    break;
                }
                else
                {
                    wrongPasswordOrUsername.style.display = DisplayStyle.Flex;
                }
                
            }
        }
    }
    void toMainMenu()
    {
        //2
        PlayerPrefs.SetString("user_name", "");
        PlayerPrefs.SetInt("user_max_score", 0);
        PlayerPrefs.SetInt("user_coins", 0);
        SceneManager.LoadScene("MainMenu");
    }

    //Clears the message that on the screen
    void clearMessageOnScreen()
    {
        takenUsername.style.display = DisplayStyle.None;
        usernameLength.style.display = DisplayStyle.None;
        passwordLength.style.display = DisplayStyle.None;
        ageError.style.display = DisplayStyle.None;
    }
}
