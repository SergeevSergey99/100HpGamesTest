using System;
using System.Reflection;
using UnityEngine;

public class MonoInstance<T> : MonoBehaviour where T : MonoInstance<T>
{
    protected static T _instance = null;
    public static bool IsInstanceExist => _instance != null;
    private static bool isSelfCreating = false;
    public static T Instance {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    isSelfCreating = true;
                    var method = typeof(T).GetMethod(nameof(SelfCreate), BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                    _instance = (T)method?.Invoke(null, null) ?? SelfCreate();
                    isSelfCreating = false;
                }

                _instance.Init();
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    protected virtual void Init() {}
    protected virtual void DeInit() {}

    protected static T SelfCreate()
    {
        var go = new GameObject(typeof(T).Name);
        return go.AddComponent<T>();
    }
    public virtual void Awake()
    {
        if (!isSelfCreating)
        {
            if (_instance == null)
            {
                _instance = (T) this;
                Init();
            }
            else if (_instance != this)
                Destroy(gameObject);
        }
    }
    public virtual void OnDestroy()
    {
        if (_instance == this)
        {
            DeInit();
            _instance = null;
        }
    }
}