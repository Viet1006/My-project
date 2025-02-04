using UnityEngine;

public abstract class AttackableEnemy : BaseEnemy
{
    protected Vector2 raycastEmiter;
    public abstract void AttackBehaviour();
    public abstract bool DetechPlayer();
}
