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
    private TextField ageField;
    private Button submitBtn;
    private Button registerLoginBtn;
    private Button loginBtn;
    private Button backBtn;
    private User_def user_def;

    //mongo
    //private const string MONGO_URI = "mongodb://localhost:27017/";
    //private const string MONGO_URI = "mongodb+srv://liorbuddha:liors1234@cluster0.leplnhi.mongodb.net/";

    private const string MONGO_URI = "mongodb+srv://liorbuddha:liors1234@cluster0.leplnhi.mongodb.net/?retryWrites=true&w=majority";
    private const string DATABASE_NAME = "birdDB";
    private MongoClient client;
    private IMongoDatabase db;


    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        register = root.Q<VisualElement>("register");
        login = root.Q<VisualElement>("login");

        usernameRegisterField = root.Q<TextField>("username-register-field");
        passwordRegisterField = root.Q<TextField>("password-register-field");
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
        //insert new user to DB
        //client = new MongoClient(MONGO_URI);
        //db = client.GetDatabase(DATABASE_NAME);
        //IMongoCollection<User_def> userCollection = db.GetCollection<User_def>("users");
        //User_def e = new();
        //e.name = "lior";
        //e.password = "12345678";
        //e.age = 29;
        //userCollection.InsertOne(e);
        //SceneManager.LoadScene("MainMenu");
        if (usernameRegisterField.text.Length < 2 || usernameRegisterField.text.Length > 32)
        {
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
            client = new MongoClient(MONGO_URI);
            db = client.GetDatabase(DATABASE_NAME);
            IMongoCollection<User_def> userCollection = db.GetCollection<User_def>("users");
            User_def e = new();
            e.name = usernameRegisterField.text;
            e.password = passwordRegisterField.text;
            e.age = int.Parse(ageField.text);
            userCollection.InsertOne(e);
            SceneManager.LoadScene("MainMenu");
        }

    }
    private void enterGameAfterLogin()
    {
        //Need DB first
        client = new MongoClient(MONGO_URI);
        db = client.GetDatabase(DATABASE_NAME);
        IMongoCollection<User_def> mc = db.GetCollection<User_def>("users", null);
        List<User_def> ul = mc.FindSync(user=>true).ToList();
        User_def[] ud = ul.ToArray();
        foreach(User_def u in ud)
        {
            Debug.Log($"{u.name}");
        }
    }
}
