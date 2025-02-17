using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MushroomCtrl : BaseEnemy
{
    PathFollower pathFollower;
    [Tooltip("Thời gian đứng Idle sau khi đến đích patrol")]
    [SerializeField] float idleTime = 0.7f;
    float idleTimeRemaining;
    void Start()
    {
        pathFollower = GetComponent<PathFollower>();
        pathFollower.TakeMovingControl();
        currentBehaviour = PatrolBehaviour;
        an = GetComponent<Animator>();
        runParticle.Stop();
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
            runParticle.Play();
            currentBehaviour = PatrolBehaviour;
            pathFollower.NextTargetSet();
            FlipToTarget(pathFollower.targetPos);
            an.SetInteger(AnimatorVariable.State,(int)StateEnum.Run);
        }
    }
}