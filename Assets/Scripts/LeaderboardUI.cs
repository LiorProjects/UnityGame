using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LeaderboardUI : MonoBehaviour
{
    private Button backBtn;
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        backBtn = root.Q<Button>("back-btn");
        backBtn.clicked += backToMenu;
    }
    //Back to main menu
    private void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
