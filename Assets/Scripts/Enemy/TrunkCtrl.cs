using Unity.Mathematics;
using UnityEngine;

public class TrunkCtrl : AttackableEnemy
{
    PathFollower pathFollower;
    [Tooltip("Thời gian đứng Idle sau khi đến đích patrol")]
    [SerializeField] float idleTime = 0.7f;
    float idleTimeRemaining;
    delegate void behaviour();
    behaviour currentBehaviour;
    Transform transformEmiter;
    [SerializeField] GameObject trunkBullet;
    public override void GetHit()
    {
        
    }
    void Start()
    {
        pathFollower = GetComponent<PathFollower>();
        pathFollower.TakeMovingControl();
        currentBehaviour = PatrolBehaviour;
        an = GetComponent<Animator>();
        transformEmiter = transform.GetChild(0).transform;
    }
    void Update()
    {
        raycastEmiter = transformEmiter.position;
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
        }
    }
    public override void AttackBehaviour()
    {
        an.SetInteger(AnimatorVariable.State,(int)StateEnum.DetectPlayer);
    }
    public override bool DetechPlayer()
    {
        return Physics2D.Raycast(raycastEmiter,-transform.right).collider.CompareTag(Tags.Player);
    }
    public void Attack()
    {
        Instantiate(trunkBullet,transformEmiter.position,transform.rotation);
    }
    public void EndAttack()
    {
        an.SetInteger(AnimatorVariable.State,(int)StateEnum.Run);
        currentBehaviour = PatrolBehaviour;
    }
}
