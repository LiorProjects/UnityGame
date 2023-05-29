using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class SpawnPipes : MonoBehaviour
{
    public GameObject fab;
    private float FirstDelay = 0f; //How much time it take to be shown on the screen for the first time
    private float NextDelay = 2.4f; //How much time needed to be waited before called the function again
    private float pipeMax = -2f;
    private float pipeMin = 2f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), FirstDelay, NextDelay);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }
    private void Spawn()
    {
        //Creates new pipe in a random position
        GameObject myPipes = Instantiate(fab, transform.position, Quaternion.identity);
        myPipes.transform.position +=  Vector3.up * Random.Range(pipeMin, pipeMax);
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
