using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T Instance;

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log($"Repeated instance of {typeof(T)}");
            Destroy(gameObject);
            return;
        }
        Instance = GetComponent<T>();
    }

    public static T GetInstance()
    {
        if (Instance == null)
        {
            Debug.Log($"Instance of {typeof(T)} is null, searching...");
            Instance = FindObjectOfType<T>();
        }
        return Instance;
    }
}
