using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using System;

public class Score
{
    public Score(ObjectId id, int score, int coins, DateTime date)
    {
        _id = id;
        this.score = score;
        this.coins = coins;
        this.date = date;
    }

    public ObjectId _id { set; get; }
    public int score { set; get; }
    public int coins { set; get; }
    public DateTime date { set; get; }
    
}
