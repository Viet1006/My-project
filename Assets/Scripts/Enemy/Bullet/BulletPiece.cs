using UnityEngine;
using System.Collections;

public class BulletPiece : MonoBehaviour
{
    Rigidbody2D rb;
    [Tooltip("Force applied on the X axis (min, max)"), SerializeField] Vector2 forceXRange;
    [Tooltip("Duration of the blink effect"), SerializeField] float blinkDuration;
    [Tooltip("Number of blinks"), SerializeField] int blinkCount;
    private float delayTime = 0.03f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(transform.right.x * Random.Range(forceXRange.x, forceXRange.y), 0), ForceMode2D.Impulse);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(Tags.Ground)&&delayTime<=0)
        {
            
            StartCoroutine(Blink());
        }   
    }
    void Update()
    {
        delayTime -= Time.deltaTime;
    }
    IEnumerator Blink()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(blinkDuration);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(blinkDuration);
        }
        Destroy(this.gameObject);
    }
}
