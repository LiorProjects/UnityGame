using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackGround : MonoBehaviour
{
    Vector3 position;
    float repeatX;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        repeatX = GetComponent<BoxCollider2D>().size.x * 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < position.x - repeatX)
        {
            transform.position = position;
        }
    }
}
