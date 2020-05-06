using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_platforms : MonoBehaviour
{
    public GameObject platform1;
    public GameObject platform2;
    public GameObject platform3;
    public GameObject platform4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {

            if (col.gameObject.tag == "box")
            {
                Destroy(platform1);
                Destroy(platform2);
                Destroy(platform3);
                Destroy(platform4);
            }


    }
}
