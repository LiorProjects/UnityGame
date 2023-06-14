using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RegisterAndLogin : MonoBehaviour
{
    // Start is called before the first frame update
    private TextField usernameField;
    private TextField passwordField;
    private TextField ageField;
    private Button submitBtn;
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        usernameField = root.Q<TextField>("username-field");
        passwordField = root.Q<TextField>("password-field");
        ageField = root.Q<TextField>("age-field");
        submitBtn = root.Q<Button>("submit");

        submitBtn.clicked += enterGame;
    }
    
    private void enterGame()
    {
        //bool isNumeric = ageField.text.All(char.IsDigit);

        //if (usernameField.text.Length < 2 || usernameField.text.Length > 32)
        //    Debug.Log("Username must be between 2 and 32 characters long");
        //else if (passwordField.text.Length < 8 || passwordField.text.Length > 32)
        //    Debug.Log("Password must be between 8 and 32 characters long");
        //else if(!isNumeric)
        //    Debug.Log("Age must be between 4 and 99 and only numbers");
        //else
        //    SceneManager.LoadScene("MainMenu");

    }
    //private void enterGame()
    //{
    //    string ageText = ageField.text;

    //    if (passwordField.text.Length < 8 || passwordField.text.Length > 20)
    //        Debug.Log("Password must be between 8 and 20 characters long");
    //    else if (int.TryParse(ageText, out int age))
    //        if(age < 4 || age > 99)
    //            Debug.Log("Age must be between 4 and 99 and only numbers");
    //    else
    //        SceneManager.LoadScene("MainMenu");
    //}
}
