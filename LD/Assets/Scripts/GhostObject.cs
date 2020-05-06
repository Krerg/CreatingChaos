using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostObject : MonoBehaviour
{

    private DimensionSwitcher ds;

    [HideInInspector]
    public Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        ds = GameObject.Find("DimensionSwitcher").GetComponent<DimensionSwitcher>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            ds.DisableSwitch();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ds.EnableSwitch();
        }
    }

}
