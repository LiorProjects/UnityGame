using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //variables of a player
    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 7f;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;
    private int score = 0;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
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
            transform.position = new Vector3(0, -5.8f, -1);
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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.name == "PipeDown" || collision.gameObject.name == "PipeUp")
    //    {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //        score = 0;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "GainScore")
        {
            Debug.Log("Score: " + ++score);
        }
    }
}
