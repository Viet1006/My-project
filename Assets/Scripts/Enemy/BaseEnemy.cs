using System;
using Unity.Mathematics;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public float speed =0.4f;
    protected DeathFall deathFall;
    public Animator an;
    public virtual void GetHit()
    {
        deathFall = GetComponent<DeathFall>();
        deathFall.enabled = true;
        enabled = false;
        if(an == null)
        {
            an = GetComponent<Animator>();
        }
        an.SetTrigger(AnimatorVariable.Die);
    }
    public void FlipToTarget(Vector2 target)
    {
        float direction = target.x - transform.position.x;
        if(direction > 0 && transform.localRotation.y == 0)
        {
            transform.localRotation = quaternion.Euler(0,-math.PI,0);
        }
        if(direction < 0 && transform.localRotation.y == -1)
        {
            transform.localRotation = quaternion.Euler(0,0,0);
        }
    }
}