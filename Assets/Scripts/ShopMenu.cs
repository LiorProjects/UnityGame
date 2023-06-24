using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using MongoDB.Driver;
using MongoDB.Bson;


public class ShopMenu : MonoBehaviour
{
    private Button backBtn;
    private Button equipBlueBird;
    private Button equipGreenBird;
    private Button equipRedBird;
    private Sounds playSound;
    private Label playerCoins;
    private MongoDBManager mongoManager1;

    //Display all birds in shop
    void Start()
    {
        mongoManager1 = gameObject.AddComponent<MongoDBManager>();
        Debug.Log(PlayerPrefs.GetInt("user_coins"));
        var root = GetComponent<UIDocument>().rootVisualElement;
        this.playSound = FindObjectOfType<Sounds>();
        this.playerCoins = root.Q<Label>("coins");
        this.playerCoins.text = "Coins: " + PlayerPrefs.GetInt("user_coins");

        this.backBtn = root.Q<Button>("back-btn");
        this.equipBlueBird = root.Q<Button>("blue-bird-buy-btn");
        this.equipGreenBird = root.Q<Button>("green-bird-buy-btn");
        this.equipRedBird = root.Q<Button>("red-bird-buy-btn");

        backBtn.clicked += backToMenu;
        equipBlueBird.clicked += blueBird;
        equipGreenBird.clicked += greenBird;
        equipRedBird.clicked += redBird;
        Debug.Log("player coins at start() " + PlayerPrefs.GetInt("user_coins"));

    }
    private void Update()
    {
        playerCoins.text = "Coins: " + PlayerPrefs.GetInt("user_coins");
    }
    //exit button
    private void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    //SELECT BIRD
    void blueBird()
    {
        int value = 100;
        if (PlayerPrefs.GetInt("user_coins") >= value)
        {
            Debug.Log("player coins \n" + PlayerPrefs.GetInt("user_coins"));
            PlayerPrefs.SetString("birdColor", "Blue");
            playSound.clickSound();
            PlayerPrefs.SetInt("user_coins", PlayerPrefs.GetInt("user_coins") - value);
            mongoManager1.updateUserCoins(PlayerPrefs.GetString("user_name"), value);
            //Add selected bird to DB
            mongoManager1.addNewBirdToUser(PlayerPrefs.GetString("user_name"), "Blue");
        }
        
    }
    void redBird()
    {
        int value = 200;
        if (PlayerPrefs.GetInt("user_coins") >= value)
        {
            PlayerPrefs.SetString("birdColor", "Red");
            playSound.clickSound();
            PlayerPrefs.SetInt("user_coins", PlayerPrefs.GetInt("user_coins") - value);
            this.mongoManager1.updateUserCoins(PlayerPrefs.GetString("user_name"), value);
            //Add selected bird to DB
            mongoManager1.addNewBirdToUser(PlayerPrefs.GetString("user_name"), "Red");
        }
    }
    void greenBird()
    {
        int value = 300;
        if (PlayerPrefs.GetInt("user_coins") >= value)
        {
            PlayerPrefs.SetString("birdColor", "Green");
            playSound.clickSound();
            PlayerPrefs.SetInt("user_coins", PlayerPrefs.GetInt("user_coins") - value);
            this.mongoManager1.updateUserCoins(PlayerPrefs.GetString("user_name"), value);
            //Add selected bird to DB
            mongoManager1.addNewBirdToUser(PlayerPrefs.GetString("user_name"), "Green");
        }
        
    }
    
}
