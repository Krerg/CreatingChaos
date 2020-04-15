using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class DestructableObject : MonoBehaviour
{
    //Тип разрушения    
    public enum DestructionType
    {
        Explosion,
        Scatter
    }

    public DestructionType destructionType = DestructionType.Explosion;

    /*Кастомный package, который я нашёл на гитхабе, этот аттрибут
     * показывает поле в инспекторе только при определённои условии */
    [ConditionalField("destructionType", compareValues:DestructionType.Explosion)]
    public float minRadius, maxRadius;

    [ConditionalField("destructionType", compareValues:DestructionType.Scatter)]
    public float minRange, maxRange;

    //Cпрайты кусков объекта
    public Sprite[] pieces;

    Vector3 s0;
    Vector3 s1;
    Rigidbody2D rb;

    Vector3 velocity;
    float gravity;
    public float height;

    public float destructionSpeed;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Vector3 target = rb.transform.position;
        target.x += 5f;
        fts.solve_ballistic_arc_lateral(rb.transform.position, destructionSpeed, target, height, out velocity, out gravity);
        Vector2 velocity2 = velocity.ToVector2();
        rb.velocity += velocity2;
    }
}

            


