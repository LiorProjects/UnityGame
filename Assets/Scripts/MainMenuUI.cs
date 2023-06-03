using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    private Button playBtn;
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        playBtn = root.Q<Button>("play-btn");

        playBtn.clicked += StartGame;
    }

    void StartGame()
    {
        SceneManager.LoadScene("BirdJumper");
    }
}
