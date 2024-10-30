using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            T tempComp = GameObject.FindObjectOfType<T>();

            if(tempComp == null)
            {
                GameObject tempObj = new GameObject(typeof(T).Name);
                tempComp = tempObj.AddComponent<T>();
            }
            
            instance = tempComp;
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
            instance = this as T;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(transform.root.gameObject);
    }
}
