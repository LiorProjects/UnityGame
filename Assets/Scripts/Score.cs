using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using System;

public class Score : MonoBehaviour
{
    public ObjectId _id { set; get; }
    public int score { set; get; }
    public DateTime date { set; get; }
    
}
