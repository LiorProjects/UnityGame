using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpeed : MonoBehaviour
{
    private float speed = 3f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        //if(Player.myScore % 2 == 0 && Player.myScore > 1)
        //{
        //    speed += 0.2f;
        //}
    }
}
