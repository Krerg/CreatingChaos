using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSwitcher : MonoBehaviour
{

    int state = 0;

    bool isSwitchPossible = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Меняем время по W
        if (Input.GetKeyDown(KeyCode.W) && isSwitchPossible)
        {

            Fadable map1 = GameObject.Find("Map1").GetComponent<Fadable>();
            Fadable map2 = GameObject.Find("Map2").GetComponent<Fadable>();
            if (state == 0)
            {
                map1.EnableCollision();
                map2.DisableCollision();
                StartCoroutine(map1.FadeImage(false));
                StartCoroutine(map2.FadeImage(true));
                state = 1;
            }
            else
            {
                map2.EnableCollision();
                map1.DisableCollision();
                StartCoroutine(map1.FadeImage(true));
                StartCoroutine(map2.FadeImage(false));
                state = 0;
            }

        }
    }

    public void DisableSwitch()
    {
        isSwitchPossible = false;
        print("Нельзя переместиться");
    }

    public void EnableSwitch()
    {
        isSwitchPossible = true;
        print("Можно переместиться");
    }
}
