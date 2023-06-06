using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
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
    public Sprite[] sprites;
    private Sounds playSound;
    public InGameTextUI inGameTextUI;
    public EndGameTextUI endGameTextUI;
    private Camera gameCamera;

    [SerializeField] private Sprite[] testCharacter;
    private String birdChar;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        gameCamera = Camera.main;
        playSound = FindObjectOfType<Sounds>();
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
        birdChar = PlayerPrefs.GetString("Blue_Bird");
        LoadFirstCharacterImageFrame();
        //if (myVal == "Blue")
        //{
        //    LoadCharacter();//YOU ARE HERE you can use myval2 = myval
        //}
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
        // Check if the player has touched the top of the screen
        Vector3 screenPosition = gameCamera.WorldToScreenPoint(transform.position);
        if(screenPosition.y < 0 || screenPosition.y > Screen.height)
        {
            pauseAndEndGame();
        }
    }
    //used in Start()
    private void AnimateSprite()
    {
        if(birdChar == "Blue")
        {
            LoadCharacter(testCharacter);
        }
        else
        {
            spriteIndex++;
            if(spriteIndex >= sprites.Length)
            {
                spriteIndex = 0;
            }
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    public void LoadCharacter(Sprite[] name)
    {
        spriteIndex++;
        if (spriteIndex >= name.Length)
        {
            spriteIndex = 0;
        }
        spriteRenderer.sprite = name[spriteIndex];
    }
    public void LoadFirstCharacterImageFrame()
    {
        if(birdChar == "Blue") spriteRenderer.sprite = testCharacter[0];
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
    void pauseAndEndGame()
    {
        Time.timeScale = 0f;
        endGameTextUI.displayEndGameScreen();
    }
}