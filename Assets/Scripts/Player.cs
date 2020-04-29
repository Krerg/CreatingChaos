using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    public float acceleration, maxWalkingSpeed;

    //Сила прыжка
    public float jumpWalkingVerticalAcceleration;

    public float maxRunningSpeed;

    public float runningAcceleration;

    public float airAcceleration;

    public float jumpRunningHorizontalAcceleration;

    public float jumpRunningVerticalAcceleration;

    private int isRunning = 0;

    private bool isRunningJump = false;

    private BoxCollider2D playerCollider;

    public LayerMask platformLayer;

    //Замедление при движении обьекта (static чтобы можно было получать доступ из других скриптов)
    [Min(1)]
    public float MovingObjectsSlowdown;
    public static float movingObjectsSlowdown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Получаем пользовательский ввод 
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Движение        
        Vector2 movement = new Vector2(moveHorizontal, 0f);
        //Прыжок по пробелу
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isRunning == 1)
            {
                rb.AddForce(movement * jumpRunningHorizontalAcceleration);
                rb.AddForce(transform.up * jumpRunningVerticalAcceleration, ForceMode2D.Impulse);
                isRunningJump = true;

            }
            else
            {
                rb.AddForce(transform.up * jumpWalkingVerticalAcceleration, ForceMode2D.Impulse);
                isRunningJump = false;
            }
            print("jump");

        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            
            isRunning = 1;
        } else
        {
           
            isRunning = 0;
        }


    }

    private void FixedUpdate()
    {
        

        //Получаем пользовательский ввод 
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Движение        
        Vector2 movement = new Vector2(moveHorizontal, 0f);

        

        

        

        bool inAir = !isGrounded();
        print(inAir);

        if(!inAir)
        {
            isRunningJump = false;
        }

        float maxSpeed;
        if (inAir && isRunningJump)
        {
            maxSpeed = 0;
        } else if(inAir)
        {
            maxSpeed = airAcceleration;
        }
        else
        {
            maxSpeed = isRunning == 1 ? maxRunningSpeed : maxWalkingSpeed;
        }

        print(maxSpeed);
        if (rb.velocity.x < maxSpeed)
        {
            rb.AddForce(movement * (acceleration + (isRunning * runningAcceleration)));
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
                tempVelocity = acceleration;
                movable.ChangeVelocity(ref acceleration);
            }
        }
    }

    private bool isGrounded()
    {
        float rayHeight = .2f;
        RaycastHit2D raycastHit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y + rayHeight, platformLayer);
        Color rayColor;
        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
        } else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(playerCollider.bounds.center, Vector2.down * (playerCollider.bounds.extents.y + rayHeight), rayColor);
        return raycastHit.collider != null;
    }

    //Меняем скорость на изначальную
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<MovableObject>(out movable))
        {
            acceleration = tempVelocity;
        }
    }
}
