using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    Rigidbody2D rb;
    public void ChangeVelocity(ref float initialVelocity)
    {
        rb = GetComponent<Rigidbody2D>();
        initialVelocity /= rb.mass * (rb.drag + 1);
        initialVelocity *= Player.movingObjectsSlowdown;
    }
}
