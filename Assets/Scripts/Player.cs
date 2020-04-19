using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;

    public float velocity, maxSpeed;

    //Сила прыжка
    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Прыжок по пробелу
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("jump");
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        //Получаем пользовательский ввод 
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Движение        
        Vector2 movement = new Vector2(moveHorizontal, 0f);

        if (rb.velocity.x < maxSpeed)
        {
            rb.AddForce(movement * velocity);

        }

           // rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ghost")
        {
            print("Столкновение");
        }
    }

}
