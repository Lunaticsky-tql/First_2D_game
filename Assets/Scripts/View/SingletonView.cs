using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonView<T> : ViewBase where T : SingletonView<T>
{

    private static T _instance;

    public static T Instance {
        get {
            if ( _instance == null )
            {
                Debug.Log( typeof(T).Name );
                GameObject.Instantiate( Resources.Load<GameObject>("Prefabs/View/" + typeof(T).Name));
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        _instance = this as T ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
