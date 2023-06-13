using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LeaderboardUI : MonoBehaviour
{
    private Button backBtn;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        backBtn = root.Q<Button>("back-btn");
        backBtn.clicked += backToMenu;
    }
    private void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
