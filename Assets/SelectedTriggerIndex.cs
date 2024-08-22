using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTriggerIndexSetter : MonoBehaviour
{
    public static SelectedTriggerIndexSetter Instance;
    public int selectedTriggerIndex;

    private void Awake()
    {
        // Check if there is already an instance of this class
        if (Instance == null)
        {
            Instance = this;  // Assign this object as the singleton instance
            DontDestroyOnLoad(gameObject);  // Ensure this object is not destroyed between scene transitions
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void DestroySelectedTriggerIndex()
    {
        Destroy(gameObject);
        Instance = null;
    }
}
