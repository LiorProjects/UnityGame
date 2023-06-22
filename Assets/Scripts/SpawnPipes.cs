using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;

public class SpawnPipes : MonoBehaviour
{
    public GameObject fab;
    public GameObject coin;
    private float FirstDelay = 0f; //How much time it take to be shown on the screen for the first time
    private float NextDelay = 2.1f; //How much time needed to be waited before called the function again
    private float pipeMax = 1f;
    private float pipeMin = 5f;
    private float coinMax = 0.6f;
    private float coinMin = -0.6f;
    private float coinChance = 0.6f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), FirstDelay, NextDelay);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }
    //Spawn the pipes
    private void Spawn()
    {
        //Creates new pipe in a random position
        GameObject myPipes = Instantiate(fab, transform.position, Quaternion.identity);
        myPipes.transform.position += Vector3.up * Random.Range(pipeMin, pipeMax);

        float randomCoin = Random.value;
        if(randomCoin <= coinChance)
        {
            GameObject myCoin = Instantiate(coin, transform.position, Quaternion.identity);
            myCoin.transform.position += Vector3.up * Random.Range(coinMin, coinMax);
            myCoin.transform.SetParent(myPipes.transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroys the pipe after leaving the screen
        if (collision.gameObject.tag == "RemovePipe")
        {
            Destroy(collision.gameObject);
        }
    }
}
