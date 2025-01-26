using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour,IGetHit,Die
{
    public abstract void GetHit();
    public abstract void Die();
    public virtual void DetectPlayer(Vector2 eyesPos)
    {
        
    }
}