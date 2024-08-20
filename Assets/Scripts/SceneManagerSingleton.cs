using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerSingleton : MonoBehaviour
{
    public static SceneManagerSingleton Instance;

    public GameObject npc;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicates if they exist
        }
    }

    public void SetNPC(GameObject npcObject)
    {
        npc = npcObject;
    }
}

