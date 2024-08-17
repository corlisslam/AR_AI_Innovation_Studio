using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class ModelSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs;

    void Awake()
    {
        if (TrackedImageTransform.Instance != null)
        {
            for (int i = 0; i < prefabs.Length; i++)
            {
                if (prefabs[i] != null)
                {
                    GameObject instantiatedPrefab = Instantiate(prefabs[i]); //, TrackedImageTransform.Instance.position, TrackedImageTransform.Instance.rotation);
                    instantiatedPrefab.transform.SetParent(null, true);
                }
            }

        }
        
    }

}
