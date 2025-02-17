using Unity.Mathematics;
using UnityEngine;

public class TrunkCtrl : AttackableEnemy
{
    PathFollower pathFollower;
    [Tooltip("Thời gian đứng Idle sau khi đến đích patrol")]
    [SerializeField] float idleTime = 0.7f;
    float idleTimeRemaining;
    [SerializeField] GameObject trunkBullet;
    void Start()
    {
        pathFollower = GetComponent<PathFollower>();
        pathFollower.TakeMovingControl();
        currentBehaviour = PatrolBehaviour;
        an = GetComponent<Animator>();
        transformEmitter = transform.GetChild(0);
        runParticle.Stop();
    }
    void Update()
    {
        currentBehaviour();
        if(DetechPlayer())
        {
            currentBehaviour = AttackBehaviour;
        }
    }
    void PatrolBehaviour()
    {
        pathFollower.MoveToNextPoint(speed);
        
        if(pathFollower.IsOnTarget())
        {
            runParticle.Stop();
            currentBehaviour = IdleBehaviour;
            idleTimeRemaining = idleTime;
            an.SetInteger(AnimatorVariable.State,(int)StateEnum.Idle);
        }
    }
    void IdleBehaviour()
    {
        idleTimeRemaining -= Time.deltaTime;
        if(idleTimeRemaining <= 0)
        {
            currentBehaviour = PatrolBehaviour;
            pathFollower.NextTargetSet();
            FlipToTarget(pathFollower.targetPos);
            an.SetInteger(AnimatorVariable.State,(int)StateEnum.Run);
            runParticle.Play();
        }
    }
    public override void AttackBehaviour()
    {
        an.SetInteger(AnimatorVariable.State,(int)StateEnum.DetectPlayer);
    }
    public override bool DetechPlayer()
    {
        return Physics2D.Raycast(transformEmitter.position,-transform.right).collider.CompareTag(Tags.Player);
    }
    public void Attack()
    {
        Instantiate(trunkBullet,transformEmitter.position,transform.rotation);
    }
    public void EndAttack()
    {
        an.SetInteger(AnimatorVariable.State,(int)StateEnum.Run);
        currentBehaviour = PatrolBehaviour;
        runParticle.Play();
    }
}
