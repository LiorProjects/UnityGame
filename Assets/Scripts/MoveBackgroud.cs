using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MoveBackgroud : MonoBehaviour
{
    private MeshRenderer mesh;
    private float speed = 0.1f;
    //Gets the mesh component
    void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    //Update is called once per frame
    void Update()
    {
        mesh.material.mainTextureOffset += new Vector2(speed *  Time.deltaTime, 0);
    }
}
