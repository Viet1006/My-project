using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [SerializeField] protected List<GameObject> bulletPiece;
    [SerializeField] protected float speed;
    void Update()
    {
        // Move the bullet to the left over time
        transform.position += -transform.right * Time.deltaTime * speed;
    }
    void HandleCollision()
    {
        foreach(var piece in bulletPiece)
        {
            Instantiate(piece, transform.position,transform.localRotation);
        }
        Destroy(this.gameObject);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        HandleCollision();
    }
}
