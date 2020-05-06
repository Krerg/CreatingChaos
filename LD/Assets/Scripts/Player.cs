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
    [Range(0, 1)]
    public float MovingObjectsAcceleration;
    public static float movingObjectsAcceleration;

    public Transform obj;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUp(this.transform);
        }

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

        //Бег (Shift)
        if (Input.GetKey(KeyCode.LeftShift))
        {

            isRunning = 1;
        }
        else
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

        bool inAir = !IsGrounded();
        print(inAir);

        if (!inAir)
        {
            isRunningJump = false;
        }

        float maxSpeed;
        if (inAir && isRunningJump)
        {
            maxSpeed = 0;
        }
        else if (inAir)
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

        movingObjectsAcceleration = MovingObjectsAcceleration;
    }

    float tempVelocity;
    MovableObject movable;
    public BoxCollider2D ground;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ghost")
        {
            print("Столкновение");
        }

        //Изменяем скорость при столкновении с обьектом
        if (collision.gameObject.TryGetComponent<MovableObject>(out movable))
        {
            if (!ground.IsTouching(collision.gameObject.GetComponent<BoxCollider2D>()))
            {
                tempVelocity = acceleration;
                movable.ChangeVelocity(ref acceleration);
            }
        }
    }

    private bool IsGrounded()
    {
        float rayHeight = .2f;
        RaycastHit2D raycastHit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y + rayHeight, platformLayer);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
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
            if (tempVelocity != 0)
            {
                acceleration = tempVelocity;
            }
        }
    }

    public void PickUp(Transform obj)
    {
        GameEventSystem.current.PickUpObject(obj);
    }
}
