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
    
   

    //Possible Methods ...
}
