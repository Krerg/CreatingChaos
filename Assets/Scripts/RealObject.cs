using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealObject : MonoBehaviour
{

    //Другая версия обьекта
    public GameObject ghostObject;


    GhostObject ghost;
    BoxCollider2D coll;

    //Создаём призрачный обьект дабы не добавлять его вручную
    private void Awake()
    {

        coll = this.GetComponent<BoxCollider2D>();
        ghost = ghostObject.AddComponent<GhostObject>();
        ghost.coll = ghostObject.AddComponent<BoxCollider2D>();
        ghost.coll.isTrigger = true;
        ghost.tag = "Ghost";
        ghost.coll.bounds.SetMinMax(coll.bounds.min, coll.bounds.max);
        ghost = ghostObject.GetComponent<GhostObject>();
        ghostObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        ghostObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

}
