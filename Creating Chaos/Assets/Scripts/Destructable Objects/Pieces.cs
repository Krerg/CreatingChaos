using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Pieces : MonoBehaviour
{
    //Классы, чтобы можно было создать рандомную перегрузку метода Explode (да, это костыль)
    public interface IRandom { }
    public class RandomExplosionRange : IRandom { }

    public void Explode(Vector3 targetPos, float jumpForce, float maxHeight)
    {
        foreach(Piece p in GetComponentsInChildren<Piece>())
        {
            p.Jump(targetPos, jumpForce, maxHeight);
            p.finalPos = targetPos;
        }
    }

    public void Explode<T> (float radius, float jumpForce, float maxHeight) where T : IRandom
    {
        foreach (Piece p in GetComponentsInChildren<Piece>())
        {
            p.Jump<RandomExplosionRange>(radius, jumpForce, maxHeight);
        }
    }
}
