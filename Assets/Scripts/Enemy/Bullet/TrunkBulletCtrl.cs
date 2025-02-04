using UnityEngine;
public class TrunkBulletCtrl : BaseBullet
{
    void Update()
    {
        transform.position += -transform.right * Time.deltaTime *speed;
    }
    public override void HandleCollision()
    {
        Destroy(this.gameObject);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name + "CollsionEnter2D");
    }
    void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name + "CollsionStay2D");
    }
    void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name + "CollsionExit2D");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name + "TriggerEnter2D");
    }
    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name + "TriggerStay2D");
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name + "TriggerExit2D");
    }
}
