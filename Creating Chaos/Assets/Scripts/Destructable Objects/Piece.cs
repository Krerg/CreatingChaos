using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Piece : MonoBehaviour
{
    public Vector3 finalPos;
    public void Jump(Vector3 targetPos, float jumpForce, float maxHeight)
    {
        float gravity;
        rb = GetComponent<Rigidbody2D>();

        //rb.mass = 1;

        Vector3 velocity;
        fts.solve_ballistic_arc_lateral(this.transform.position, jumpForce, targetPos, maxHeight, out velocity, out gravity);
        Vector2 velocity2 = velocity.ToVector2();

        rb.velocity += velocity2 * Time.deltaTime;
    }


    public bool exploded;
    Rigidbody2D rb;
    Vector3 randomPos;
    public void Jump<T>(float radius, float jumpForce, float maxHeight) where T : Pieces.IRandom
    {
        float gravity;

        float randomXPos = Random.Range(this.transform.position.x - radius, this.transform.position.x + radius);
        float randomYPos = Random.Range(this.transform.position.y - radius, this.transform.position.y + radius);

        randomPos = new Vector3(randomXPos, randomYPos, 0f);
        rb = this.gameObject.GetComponent<Rigidbody2D>();

        //rb.mass = 1;

        Vector3 velocity;
        fts.solve_ballistic_arc_lateral(this.transform.position, jumpForce, randomPos, maxHeight, out velocity, out gravity);
        Vector2 velocity2 = velocity.ToVector2();
        rb.velocity += velocity2;
        this.finalPos = randomPos;
    }

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if ((rb.velocity.y < -5f) && (transform.position.y  < randomPos.y))
        {
            rb.simulated = false;
        }
    }
}