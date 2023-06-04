using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    private Button playBtn;
    private Button shopBtn;
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        playBtn = root.Q<Button>("play-btn");
        shopBtn = root.Q<Button>("shop-btn");

        playBtn.clicked += StartGame;
        shopBtn.clicked += shop;
    }

    void StartGame()
    {
        SceneManager.LoadScene("BirdJumper");
    }
    void shop()
    {
        SceneManager.LoadScene("Shop");
    }
}
