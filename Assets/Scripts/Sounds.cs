using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    //Array of sounds
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
    //Shop sounds
    public void clickSound()
    {
        sound[0].Play();
    }
}
