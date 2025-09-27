using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    protected static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();
                if (_instance == null)
                    _instance = new GameObject(typeof(T).Name + "Singleton Auto-Generated").AddComponent<T>();
            }
            return _instance;
        }
    }
}
