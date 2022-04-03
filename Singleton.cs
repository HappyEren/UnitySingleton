using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static bool appIsQuit = false;

    public static T Instance
    {
        get
        {
            if (appIsQuit)
                return null;

            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    var singletonObj = new GameObject();
                    singletonObj.name = typeof(T).Name;
                    DontDestroyOnLoad(singletonObj);

                    instance = singletonObj.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (Instance != this)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationQuit()
    {
        if (instance == this)
            instance = null;

        appIsQuit = true;
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}