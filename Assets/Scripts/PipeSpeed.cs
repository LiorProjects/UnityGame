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
        //if (player.getScore() % 2 == 0 && player.getScore() > 1)
        //{
        //    speed += 1.0f;
        //}
    }
}
