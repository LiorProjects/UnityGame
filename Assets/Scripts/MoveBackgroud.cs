using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackgroud : MonoBehaviour
{
    // Start is called before the first frame update
    private MeshRenderer mesh;
    private float speed = 0.4f;
    void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mesh.material.mainTextureOffset += new Vector2(speed *  Time.deltaTime, 0);
    }
}
