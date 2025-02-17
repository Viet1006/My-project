using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    Animator animator;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(Tags.Player)){
            animator = GetComponent<Animator>();
            animator.SetTrigger("Hit");
        }
    }
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
