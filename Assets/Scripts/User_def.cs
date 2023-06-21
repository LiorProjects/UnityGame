using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
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
    public List<Score> scores { set; get; }



    //Possible Methods ...
}
