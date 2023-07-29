using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
// Model_User Sample
public class User_def
{
    public ObjectId _id { set; get; }
    public string name { set; get; }
    public string password { set; get; }
    public int age { set; get; }
    public int coins_count {  set; get; }
    public int max_score { set; get; }
    public string status { set; get; }
    public Score[] scores { set; get; }
    public string[] birds { set; get; }
    public string birdColor { set; get; }



    //Possible Methods ...
}
