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
        if(NotificationManager.hasShownNotification == false)
        {
            //Create the profile for the notification.
            AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel()
            {
                Id = "first_test",
                Name = "default",
                Importance = Importance.High,
                Description = "myTest",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

            //Create custom notification.
            AndroidNotification notification = new AndroidNotification();
            notification.Title = "BirdJumper";
            notification.Text = "Welcome back!";
            notification.SmallIcon = "icon_small";
            notification.LargeIcon = "icon_large";
            notification.ShowTimestamp = true;
            notification.FireTime = System.DateTime.Now.AddSeconds(2);
        
            //Send the notification to user.
            var identifier = AndroidNotificationCenter.SendNotification(notification, "first_test");
            //Check if there any notifications if yes cancel them and put new notification to prevent duplication.
            if(AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Scheduled)
            {
                AndroidNotificationCenter.CancelAllNotifications();
                AndroidNotificationCenter.SendNotification(notification, "first_test");
            }
            NotificationManager.hasShownNotification = true;
        }
    }
}
