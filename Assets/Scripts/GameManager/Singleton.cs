using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }
    public static bool IsInitialized => Instance != null;

    
    protected virtual void Awake()
    {
        if (Instance != null)
        {
            if (Debug.isDebugBuild)
                Debug.LogError("[Singleton] trying to instantiate a second instance of a singleton class.");
            Destroy(gameObject);
        }
        else
            Instance = (T)this;
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
