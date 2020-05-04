using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    float speed = 3f;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target_position = new Vector3(target.position.x, target.position.y+1.7f, target.position.z);
        target_position.z = transform.position.z - 1;
        transform.position = Vector3.Lerp(transform.position, target_position, speed * Time.deltaTime);
    }
}
