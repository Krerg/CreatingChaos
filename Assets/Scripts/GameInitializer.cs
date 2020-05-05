using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //При нажатии ESC выходим из приложения
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void Awake()
    {
        
        Fadable map1 = GameObject.Find("Map1").GetComponent<Fadable>();
        Fadable map2 = GameObject.Find("Map2").GetComponent<Fadable>();

        
        StartCoroutine(map1.FadeImage(true));
        map1.DisableCollision();

        Transform tmap1 = GameObject.Find("Map1").GetComponent<Transform>();
        Transform tmap2 = GameObject.Find("Map2").GetComponent<Transform>();

        tmap2.position = new Vector3(0, 0, 0);
        tmap1.position = new Vector3(0, 0, 0);
    }

    public void SwitchDimension()
    {

    }
}
