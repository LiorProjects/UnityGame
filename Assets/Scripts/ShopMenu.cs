using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ShopMenu : MonoBehaviour
{
    private Button backBtn;
    private Button use;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        backBtn = root.Q<Button>("back-btn");
        use = root.Q<Button>("blue-bird-buy-btn");
        backBtn.clicked += backToMenu;
        //use.clicked += load;
        use.clicked += blueBird;
    }

    private void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    //void load()
    //{
    //    player.LoadCharacter();
    //}
    void blueBird()
    {
        PlayerPrefs.SetString("Blue_Bird", "Blue");
    }
}
