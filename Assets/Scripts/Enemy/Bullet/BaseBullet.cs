using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] protected List<GameObject> bulletPiece;
    [SerializeField] protected float speed;
    public abstract void HandleCollision();
}
