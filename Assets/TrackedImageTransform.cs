using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedImageTransform : MonoBehaviour
{
    // The singleton instance
    public static TrackedImageTransform Instance;

    public Vector3 position;
    public Quaternion rotation;
    public string imageName;

    // Start is called before the first frame update
    private void Awake()
    {
        // Check if there is already an instance of this class
        if (Instance == null)
        {
            Instance = this;  // Assign this object as the singleton instance
            DontDestroyOnLoad(gameObject);  // Ensure this object is not destroyed between scene transitions
        }
    }
}

