using UnityEngine;
public class FlameThrower : MonoBehaviour
{
    GameObject Flame;
    Animator An;
    bool IsThrowing;
    void  Start ()
    {
        Flame = transform.GetChild(0).gameObject;
        An= GetComponent<Animator>(); 
    }
    public void ThrowFlame() // Bắt đầu phóng lửa
    {
        Flame.SetActive(true);
    }
    public void EndOn()
    {
        Flame.SetActive(false);
        IsThrowing=false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(Tags.UnderPlayer)&& !IsThrowing)
        {
            An.SetTrigger("Active");
            IsThrowing=true;
        }
    }
}
