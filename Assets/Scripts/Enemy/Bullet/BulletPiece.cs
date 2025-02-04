using UnityEngine;

public class BulletPiece : MonoBehaviour
{
    Rigidbody2D rb;
    [Tooltip("Lực đẩy theo chiều X tối thiểu ")]
    [SerializeField] float minForceX;
    [Tooltip("Lực đẩy theo chiều X tối đa ")]
    [SerializeField] float maxForceX;
    [Tooltip("Lực đẩy theo chiều Y tối thiểu ")]
    [SerializeField] float minForceY;
    [Tooltip("Lực đẩy theo chiều Y tối đa ")]
    [SerializeField] float maxForceY;
    void Start()
    {
        rb =GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
    }
}
