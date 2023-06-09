using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ShopMenu : MonoBehaviour
{
    private Button backBtn;
    private Button equipBlueBird;
    private Button equipGreenBird;
    private Button equipRedBird;
    private Sounds playSound;
    private Label coins;
    // Start is called before the first frame update
    void Start()
    {
        playSound = FindObjectOfType<Sounds>();
        var root = GetComponent<UIDocument>().rootVisualElement;
        coins = root.Q<Label>("coins");
        coins.text = "Coins: " + PlayerPrefs.GetInt("Coins");
        backBtn = root.Q<Button>("back-btn");
        equipBlueBird = root.Q<Button>("blue-bird-buy-btn");
        equipGreenBird = root.Q<Button>("green-bird-buy-btn");
        equipRedBird = root.Q<Button>("red-bird-buy-btn");
        backBtn.clicked += backToMenu;
        equipBlueBird.clicked += blueBird;
        equipGreenBird.clicked += greenBird;
        equipRedBird.clicked += redBird;
    }
    private void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void blueBird()
    {
        PlayerPrefs.SetString("birdColor", "Blue");
        playSound.clickSound();
    }
    void greenBird()
    {
        PlayerPrefs.SetString("birdColor", "Green");
        playSound.clickSound();
    }
    void redBird()
    {
        PlayerPrefs.SetString("birdColor", "Red");
        playSound.clickSound();
    }
}
