using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    //variables of a player
    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 7f;
    private int spriteIndex;
    private int playerScore = 0;
    private int playerCoins = 0;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private Sounds playSound;
    public InGameTextUI inGameTextUI;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        playSound = GameObject.FindObjectOfType<Sounds>();
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }
    // Update is called once per frame
    private void Update()
    {
        //getting input from keyboard or mouse
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0))
        {
            //set new values to the vector
            direction = Vector3.up * strength;
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
        if (transform.position.y >= 5.8f)
        {
            // transform the player to bottom
            transform.position = new Vector3(0, -5.8f, -5);
        }
        
    }
    //private void Die()
    //{
    //    // Add your code here to handle the player's death
    //    // For example, you can show a game over screen, play a sound effect, etc.

    //    // Disable the player object to prevent further movement
    //    transform.position = new Vector3(0, -4.9f, 0);

    //    gameObject.SetActive(true);
    //}
    
    
    //used in Start()
    private void AnimateSprite()
    {
        spriteIndex++;
        if(spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }
        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RemovePipe")
        {
            playSound.hitSound();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "GainScore")
        {
            inGameTextUI.scoreText(++playerScore);
        }
        if(collision.gameObject.tag == "RemoveCoin")
        {
            playSound.coinSound();
            inGameTextUI.coinsText(++playerCoins);
            Destroy(collision.gameObject);
        }
    }
}
