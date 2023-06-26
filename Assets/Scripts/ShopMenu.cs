using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Diagnostics.Tracing;

public class ShopMenu : MonoBehaviour
{
    private Button backBtn;
    private Button buyBlueBird;
    private Button equipBlueBird;
    private Button buyRedBird;
    private Button equipRedBird;
    private Button buyGreenBird;
    private Button equipGreenBird;

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
        backBtn.clicked += backToMenu;
        //Buttons for Blue bird
        this.equipBlueBird = root.Q<Button>("blue-bird-equip-btn");
        this.buyBlueBird = root.Q<Button>("blue-bird-buy-btn");
        buyBlueBird.clicked += () => buyBlueBirdFromShop(100);
        birdShop(buyBlueBird, equipBlueBird, "Blue");
        //Buttons for Red bird
        this.equipRedBird = root.Q<Button>("red-bird-equip-btn");
        this.buyRedBird = root.Q<Button>("red-bird-buy-btn");
        buyRedBird.clicked += () => buyRedBirdFromShop(200);
        birdShop(buyRedBird, equipRedBird, "Red");
        //Buttons for Green bird
        this.equipGreenBird = root.Q<Button>("green-bird-equip-btn");
        this.buyGreenBird = root.Q<Button>("green-bird-buy-btn");
        buyGreenBird.clicked += () => buyGreenBirdFromShop(300);
        birdShop(buyGreenBird, equipGreenBird, "Green");

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

    void birdShop(Button buyBird, Button equipBird, string birdName)
    {
        if (mongoManager1.checkIfUserHaveBird(PlayerPrefs.GetString("user_name"), birdName))
        {
            buyBird.style.display = DisplayStyle.None;
            equipBird.style.display = DisplayStyle.Flex;
            if(birdName == "Blue")
                equipBird.clicked += setBlueBird;
            else if(birdName == "Red")
                equipBird.clicked += setRedBird;
            else
                equipBird.clicked += setGreenBird;
        }
    }

    //Buy Blue bird
    void buyBlueBirdFromShop(int value)
    {
        if (PlayerPrefs.GetInt("user_coins") >= value)
        {
            Debug.Log("player coins \n" + PlayerPrefs.GetInt("user_coins"));
            PlayerPrefs.SetString("birdColor", "Blue");
            playSound.clickSound();
            PlayerPrefs.SetInt("user_coins", PlayerPrefs.GetInt("user_coins") - value);
            mongoManager1.updateUserCoins(PlayerPrefs.GetString("user_name"));
            //Add selected bird to DB
            mongoManager1.addNewBirdToUser(PlayerPrefs.GetString("user_name"), "Blue");
            this.buyBlueBird.style.display = DisplayStyle.None;
            equipBlueBird.clicked += setBlueBird;
        }
    }
    //Set Blue bird
    void setBlueBird()
    {
        PlayerPrefs.SetString("birdColor", "Blue");
        playSound.clickSound();
    }
    //Buy Red bird
    void buyRedBirdFromShop(int value)
    {
        if (PlayerPrefs.GetInt("user_coins") >= value)
        {
            Debug.Log("player coins \n" + PlayerPrefs.GetInt("user_coins"));
            PlayerPrefs.SetString("birdColor", "Red");
            playSound.clickSound();
            PlayerPrefs.SetInt("user_coins", PlayerPrefs.GetInt("user_coins") - value);
            mongoManager1.updateUserCoins(PlayerPrefs.GetString("user_name"));
            //Add selected bird to DB
            mongoManager1.addNewBirdToUser(PlayerPrefs.GetString("user_name"), "Red");
            this.buyRedBird.style.display = DisplayStyle.None;
            equipRedBird.clicked += setRedBird;
        }
    }
    //Set Red bird
    void setRedBird()
    {
        PlayerPrefs.SetString("birdColor", "Red");
        playSound.clickSound();
    }
    //Buy Green bird
    void buyGreenBirdFromShop(int value)
    {
        if (PlayerPrefs.GetInt("user_coins") >= value)
        {
            Debug.Log("player coins \n" + PlayerPrefs.GetInt("user_coins"));
            PlayerPrefs.SetString("birdColor", "Green");
            playSound.clickSound();
            PlayerPrefs.SetInt("user_coins", PlayerPrefs.GetInt("user_coins") - value);
            mongoManager1.updateUserCoins(PlayerPrefs.GetString("user_name"));
            //Add selected bird to DB
            mongoManager1.addNewBirdToUser(PlayerPrefs.GetString("user_name"), "Green");
            this.buyGreenBird.style.display = DisplayStyle.None;
            equipGreenBird.clicked += setGreenBird;
        }
    }
    //Set Green bird
    void setGreenBird()
    {
        PlayerPrefs.SetString("birdColor", "Green");
        playSound.clickSound();
    }
}
