using UnityEngine;
public class DeathFall : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0.03f; // Tốc độ quay die effect
    Rigidbody2D rb;
    [SerializeField] Vector2 force = new Vector2 (3,4); // Lực đẩy die effect
    void Awake()
    {
        enabled = false; // Đặt bằng false chờ kích hoạt
    }
    void Start()
    {
        if(GetComponent<Rigidbody2D>() == null)
        {
            gameObject.AddComponent<Rigidbody2D>();
        }
        rb = GetComponent<Rigidbody2D>();
        DestroyAllColliders();
        rb.bodyType = RigidbodyType2D.Dynamic;
        if(GetComponent<BaseEnemy>())
        {
            GetComponent<BaseEnemy>().enabled = false;
        }
        int randomValue = Random.Range(-1,1);
        if(randomValue>=0)
        {
            rb.velocity = force;
        }else{
            rb.velocity = new Vector2(-force.x,force.y);
            rotationSpeed *=-1;
        }
        
    }
    void Update()
    {
        transform.Rotate(Vector3.forward*rotationSpeed);
    }
    private void DestroyAllColliders()
    {
        Collider2D[] collider2D = GetComponents<Collider2D>();
        foreach(Collider2D colliders in collider2D)
        {
            Destroy(colliders);
        }
    }
}
