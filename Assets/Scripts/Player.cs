using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //variables of a player
    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 7f;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;


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
            Debug.Log(direction.y);
            Debug.Log("test     ");
            
            
        }
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
        if(direction.y == -1*Screen.height)
        {
            direction.y = 0;
            transform.position = direction;
        }
    }

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
}
