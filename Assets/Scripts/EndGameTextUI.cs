using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EndGameTextUI : MonoBehaviour
{
    private Button playAgain;
    private Button mainMenu;
    private VisualElement endGameScreen;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        playAgain = root.Q<Button>("play-again-btn");
        mainMenu = root.Q<Button>("main-menu-btn");
        endGameScreen = root.Q<VisualElement>("display-end-screen");

        playAgain.clicked += play_again;
        mainMenu.clicked += main_menu;
    }

    void play_again()
    {
        SceneManager.LoadScene("BirdJumper");
        Time.timeScale = 1.0f;
    }
    void main_menu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }
    public void displayEndGameScreen()
    {
        endGameScreen.style.display = DisplayStyle.Flex;
    }
}
