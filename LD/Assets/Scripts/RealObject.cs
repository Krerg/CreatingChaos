using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealObject : MonoBehaviour
{

    //Другая версия обьекта
    public GameObject ghostObject;


    GhostObject ghost;
    BoxCollider2D coll;

    
    private void Awake()
    {
        CreateGhost();
    }

    private void Update()
    {
        ghostObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    //Создаём призрачный обьект дабы не добавлять его вручную
    public void CreateGhost()
    {
        if (ghostObject == null)
        {
            ghostObject = new GameObject(this.gameObject.name + "'s Ghost");
            if (this.gameObject.transform.parent.name == "Map1")
            {
                ghostObject.gameObject.transform.SetParent(GameObject.Find("Map2").transform);
            }else if (this.gameObject.transform.parent.name == "Map2")
            {
                ghostObject.gameObject.transform.SetParent(GameObject.Find("Map1").transform);
            }
        }

        if (!this.TryGetComponent<BoxCollider2D>(out coll))
        {
            coll = this.gameObject.AddComponent<BoxCollider2D>();
        }

        ghost = ghostObject.AddComponent<GhostObject>();
        ghost.coll = ghostObject.AddComponent<BoxCollider2D>();
        ghost.coll.isTrigger = true;
        ghost.tag = "Ghost";
        ghost.coll.bounds.SetMinMax(coll.bounds.min, coll.bounds.max);
        ghost = ghostObject.GetComponent<GhostObject>();
        ghostObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    }
}
