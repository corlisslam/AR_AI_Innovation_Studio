using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingNotificationHandler : MonoBehaviour
{
    void Start()
    {
        if (NotificationSystemHandler.Instance != null)
        {
            Debug.Log("NotificationSystemHandler found and is working.");
        }
        else
        {
            Debug.LogError("NotificationSystemHandler is null!");
        }
    }

}
