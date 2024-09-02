using UnityEngine;

public class SelectedTriggerIndexSetter : MonoBehaviour
{
    public static SelectedTriggerIndexSetter Instance { get; private set; }
    public int selectedTriggerIndex;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void DestroySelectedTriggerIndex()
    {
        Destroy(gameObject);
        Instance = null;
    }
}
