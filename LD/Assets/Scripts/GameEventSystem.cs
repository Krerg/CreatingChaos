using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSystem : MonoBehaviour
{
    //Event для наезжания камеры
    public static GameEventSystem current;

    private void Awake()
    {
        current = this;
    }

    public event Action<Transform> onObjectPickedUp;
    public void PickUpObject(Transform obj)
    {
        if(onObjectPickedUp != null)
        {
            onObjectPickedUp(obj);
        }
    }
}

