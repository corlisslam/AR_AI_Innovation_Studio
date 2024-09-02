using UnityEngine;



public class ModelSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs;

    void Awake()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            if (prefabs[i] != null)
            {
                GameObject instantiatedPrefab = Instantiate(prefabs[i]); //, TrackedImageTransform.Instance.position, TrackedImageTransform.Instance.rotation);
                Debug.Log("Prefab instantiated: " + instantiatedPrefab.name);
                instantiatedPrefab.transform.SetParent(null, true);
            }
        }
    }

    void Start()
    {
        NotificationSystemHandler.Instance.NotificationRequest(NotificationType.ModelsHaveSpawned);
    }

}
