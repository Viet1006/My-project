using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PigCtrl : BaseEnemy
{
    PathFollower pathFollower;
    [Tooltip("Thời gian đứng Idle sau khi đến đích patrol")]
    [SerializeField] float idleTime;
    float idleTimeRemaining;
    [SerializeField] float speedAngry;
    delegate void behaviour();
    behaviour currentBehaviour;
    public override void GetHit()
    {
        if(currentBehaviour == AngryBehaviour) // Get hit lần 2 thì chết
        {
            deathFall = GetComponent<DeathFall>();
            deathFall.enabled = true;
            enabled = false;
            an.SetTrigger(AnimatorVariable.Die);
        }else{
            an.SetTrigger(AnimatorVariable.GetHit);
        }
        an.SetBool(AnimatorVariable.IsAngry,true);
        currentBehaviour = AngryBehaviour;
    }
    void Start()
    {
        pathFollower = GetComponent<PathFollower>();
        pathFollower.TakeMovingControl();
        currentBehaviour = PatrolBehaviour;
        an = GetComponent<Animator>();
    }
    void Update()
    {
        currentBehaviour();
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
    void AngryBehaviour()
    {
        pathFollower.MoveToNextPoint(speedAngry);
        if(pathFollower.IsOnTarget())
        {
            pathFollower.NextTargetSet();
            FlipToTarget(pathFollower.targetPos);
        }
    }
}
