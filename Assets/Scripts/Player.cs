using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    public float velocity, maxSpeed;

    //Сила прыжка
    public float jumpForce;

    //Замедление при движении обьекта (static чтобы можно было получать доступ из других скриптов)
    [Min(1)]
    public float MovingObjectsSlowdown;
    public static float movingObjectsSlowdown;

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

        movingObjectsSlowdown = MovingObjectsSlowdown;
    }

    BoxCollider2D coll;
    float tempVelocity;
    MovableObject movable;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ghost")
        {
            print("Столкновение");
        }

        //Изменяем скорость при столкновении с обьектом
        if (collision.gameObject.TryGetComponent<MovableObject>(out movable))
        {
            coll = GetComponent<BoxCollider2D>();
            if (coll.bounds.min.y < collision.gameObject.GetComponent<BoxCollider2D>().bounds.center.y)
            {
                tempVelocity = velocity;
                movable.ChangeVelocity(ref velocity);
            }
        }
    }

    //Меняем скорость на изначальную
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<MovableObject>(out movable))
        {
            velocity = tempVelocity;
        }
    }
}
