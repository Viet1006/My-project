using UnityEngine;

public class TrampolineController : MonoBehaviour
{
       public static float PushForce = 21f;
       Animator An;
       void Start()
       {
              An = GetComponent<Animator>();
       }
       void OnTriggerEnter2D(Collider2D other){
              if(other.CompareTag(Tags.UnderPlayer)){
                     An.SetTrigger("Active");
              }
       }
}