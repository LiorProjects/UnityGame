using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using System;

public class Notifications : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //if(NotificationManager.hasShownNotification == false)
        //    sendNotification("Welcome", "Welcome the user to the game", "BirdJumper", "Welcome Back!", "icon_small", "icon_large", DateTime.Now.AddSeconds(2));
        //NotificationManager.hasShownNotification = true;
    }

    //Send a custom notification
    private void sendNotification(string id, string name, string title, string text, string smallIcon, string largeIcon, DateTime fireTime)
    {
        //Create the profile for the notification.
        var notificationChannel = new AndroidNotificationChannel()
        {
            Id = id,
            Name = name,
            Importance = Importance.High,
            Description = "Game notification",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        //Create the notification.
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.SmallIcon = smallIcon;
        notification.LargeIcon = largeIcon;
        notification.ShowTimestamp = true;
        notification.FireTime = fireTime;

        //Send the notification to user.
        AndroidNotificationCenter.SendNotification(notification, id);
        NotificationManager.hasShownNotification = true;
    }
}
