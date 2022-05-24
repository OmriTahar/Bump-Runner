using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance => _instance;

    public virtual void Awake()
    {
        if (isActiveAndEnabled)
        {
            if (Instance == null)
                _instance = this as T;

            else if (Instance != this as T)
                Destroy(this);
        }
    }
}