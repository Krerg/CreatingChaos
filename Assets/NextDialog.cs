using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDialog : MonoBehaviour
{
    public bool showed;
    public int newIndex;
    public GameObject dialogManager;
    public bool I_need_tom;
    public bool tom;
    // Start is called before the first frame update
    void Start()
    {
        tom = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (tom == true)
                tom = false;
            else
                tom = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (showed == false)
        {
            if (col.gameObject.tag == "Player")
            {
                if (I_need_tom == tom)
                {
                    dialogManager.GetComponent<Dialog>().NextSentence(newIndex);
                    showed = true;
                }
            }
        }

    }
}

