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
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        //Buttons
        playAgain = root.Q<Button>("play-again-btn");
        mainMenu = root.Q<Button>("main-menu-btn");
        endGameScreen = root.Q<VisualElement>("display-end-screen");

        playAgain.clicked += play_again;
        mainMenu.clicked += main_menu;
    }

    void play_again()
    {
        SceneManager.LoadScene("BirdJumper");
    }
    void main_menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    //Display a box with options
    public void displayEndGameScreen()
    {
        endGameScreen.style.display = DisplayStyle.Flex;
    }
}
