using System.Collections.Generic;
using UnityEngine;

public class Pig : XMovableEnemy,Patroll
{
    PathFollower pathFollower;
    [SerializeField] private float speed=5f;
    PigState currentState;
    BehaviourEnum currentBehaviour;
    List<BehaviourEnum> pigBehaviour = new List<BehaviourEnum>
    {
        BehaviourEnum.Idle,
        BehaviourEnum.Chase,
        BehaviourEnum.Patroll,
    };
    new public void Awake()
    {
        base.Awake();
    }
    public void Start()
    {
        currentState = PigState.NormalPig;
        currentBehaviour = BehaviourEnum.Patroll;
        pathFollower = GetComponent<PathFollower>();
        pathFollower.TakeMovingControl();
    }
    public override void Die()
    {
        
    }
    public override void GetHit()
    {
        
    }
    void Update()
    {
        ExcuteBehaviour();
    }
    void ExcuteBehaviour()
    {
        switch (currentBehaviour)
        {
            case BehaviourEnum.Patroll: 
            Patroll(); break;
        }
    }
    public void Patroll()
    {
        pathFollower.MoveToNextPoint(speed);
        if(pathFollower.IsOnTarget()){
            pathFollower.NextTargetSet();
            Flip(pathFollower.targetPos);
        }
    }
}
enum PigState
{
    NormalPig,
    AngryPig,
}