using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource[] sound;

    public void jumpSound()
    {
        sound[0].Play();
    }
    public void hitSound()
    {
        sound[1].Play();
    }
    public void coinSound()
    {
        sound[2].Play();
    }
}
