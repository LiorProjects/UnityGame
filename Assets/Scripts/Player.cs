using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //variables of a player
    public float gravity = -9.8f;
    public float strength = 5f;
    private int spriteIndex;
    private int playerScore = 0;
    private int playerCoins = 0;
    private Vector3 direction;
    private SpriteRenderer spriteRenderer;
    public InGameTextUI inGameTextUI;
    public EndGameTextUI endGameTextUI;
    private Sounds playSound;
    private Camera gameCamera;
    public Sprite[] sprites;

    [SerializeField] private Sprite[] blueBird;
    [SerializeField] private Sprite[] greenBird;
    [SerializeField] private Sprite[] redBird;
    private string birdColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Application.targetFrameRate = 120;
        Time.timeScale = 1f;
        gameCamera = Camera.main;
        playSound = FindObjectOfType<Sounds>();
        birdColor = PlayerPrefs.GetString("birdColor");
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
        LoadFirstFrameOfTheImage();
    }
    // Update is called once per frame
    private void Update()
    {
        // Check if the game has not started yet and the player presses a key or mouse button
        
        //getting input from keyboard or mouse
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0))
        {
            //set new values to the vector
            direction = Vector3.up * strength;
            if (Time.timeScale != 0f)
                playSound.jumpSound();
        }
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
        
        //rotate on Z
        if(direction.y > 0)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 15f);
        }
        else
        { 
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        // Check if the player has touched the top or the bottom of the screen
        Vector3 screenPosition = gameCamera.WorldToScreenPoint(transform.position);
        if(screenPosition.y < 0 || screenPosition.y > Screen.height)
        {
            pauseAndEndGame();
        }
    }
    //used in Start()
    private void AnimateSprite()
    {
        if(birdColor == "Blue")
        {
            LoadCharacter(blueBird);
        }
        else if(birdColor == "Green")
        {
            LoadCharacter(greenBird);
        }
        else if(birdColor == "Red")
        {
            LoadCharacter(redBird);
        }
        else
        {
            LoadCharacter(sprites);
        }
    }

    private void LoadCharacter(Sprite[] name)
    {
        spriteIndex++;
        if (spriteIndex >= name.Length)
        {
            spriteIndex = 0;
        }
        spriteRenderer.sprite = name[spriteIndex];
    }
    private void LoadFirstFrameOfTheImage()
    {
        if(birdColor == "Blue") spriteRenderer.sprite = blueBird[0];
        else if(birdColor == "Green") spriteRenderer.sprite = greenBird[0];
        else if(birdColor == "Red") spriteRenderer.sprite = redBird[0];
    }

    //Collision a Pole
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RemovePipe")
        {
            playSound.hitSound();
            pauseAndEndGame();
        }
    }

    //If the player hit an obstacle
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "GainScore")
        {
            inGameTextUI.scoreText(++playerScore);
            Time.timeScale += 0.05f;
        }
        if(collision.gameObject.tag == "RemoveCoin")
        {
            playSound.coinSound();
            inGameTextUI.coinsText(++playerCoins);
            Destroy(collision.gameObject);
        }
    }
    //End the current game
    private void pauseAndEndGame()
    {
        Time.timeScale = 0f;
        endGameTextUI.displayEndGameScreen();
        PlayerPrefs.SetInt("Coins",playerCoins);//test
        //add DB query
    }
}