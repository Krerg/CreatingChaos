using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Destruction : MonoBehaviour
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
    public float radius;

    [ConditionalField("destructionType", compareValues:DestructionType.Scatter)]
    public float minRange, maxRange;

    //Cпрайты кусков объекта
    public Sprite[] pieces;

    public float maxHeight;
    public Vector3 targetPosition;
    public float destructionSpeed;

    public void Explode()
    {
        GameObject piecesObj = new GameObject(this.gameObject.name + " Pieces");
        piecesObj.AddComponent<Pieces>();

        for (int x = 0; x < pieces.Length; x++)
        {
            GameObject piece = new GameObject(this.gameObject.name + " Piece");
            piece.AddComponent<Piece>();
            piece.AddComponent<SpriteRenderer>();
            piece.AddComponent<Rigidbody2D>();
            SpriteRenderer renderer = piece.GetComponent<SpriteRenderer>();
            renderer.sprite = pieces[x];
            piece.transform.SetParent(piecesObj.transform);
        }

        Pieces explosion = piecesObj.GetComponent<Pieces>();
        explosion.Explode<Pieces.RandomExplosionRange>(radius, destructionSpeed, maxHeight);
    }

    private void Start()
    {
        Explode();
        Destroy(this.gameObject);
    }
}

            


