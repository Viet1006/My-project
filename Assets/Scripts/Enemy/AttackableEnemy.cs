using UnityEngine;

public abstract class AttackableEnemy : BaseEnemy
{
    // Nơi phát raycast để xác định vị trí của player
    protected Transform transformEmitter;
    public abstract void AttackBehaviour();
    public abstract bool DetechPlayer();
}
