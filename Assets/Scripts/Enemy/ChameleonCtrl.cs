using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
public class ChameleonCtrl : AttackableEnemy
{
    PathFollower pathFollower;
    [Tooltip("Thời gian đứng Idle sau khi đến đích patrol")]
    // [SerializeField] float idleTime = 0.7f;
    float idleTimeRemaining;
    [SerializeField] private float attackRange;
    [SerializeField]GameObject attackZone;
    void Start()
    {
        transformEmitter = transform.GetChild(0);
        runParticle.Stop();
        currentBehaviour = IdleBehaviour;
    }
    void Update()
    {
        currentBehaviour();
    }
    public override void AttackBehaviour()
    {
        an.SetInteger(AnimatorVariable.State, (int)StateEnum.DetectPlayer);
        runParticle.Stop();
    }
    void IdleBehaviour()
    {
        if(DetechPlayer())
        {
            currentBehaviour = ChaseBehaviour;
            an.SetInteger(AnimatorVariable.State, (int)StateEnum.Run);
            runParticle.Play();
        }
    }
    void ChaseBehaviour()
    {
        if(!DetechPlayer())
        {
            currentBehaviour = IdleBehaviour;
            an.SetInteger(AnimatorVariable.State, (int)StateEnum.Idle);
            runParticle.Stop();
            return;
        }
        if(Mathf.Abs(PlayerController.Pc.transform.position.x - transform.position.x) > attackRange)
        {
            transform.position = new Vector2(
                Mathf.MoveTowards(transform.position.x, PlayerController.Pc.transform.position.x, speed * Time.deltaTime),
                transform.position.y
            );
            FlipToTarget(PlayerController.Pc.transform.position);
        }
        else
        {
            currentBehaviour = AttackBehaviour;
        }
    }
    public override bool DetechPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transformEmitter.position, PlayerController.Pc.transform.position - transformEmitter.position + new Vector3(0, 0.5f, 0), Mathf.Infinity, ~LayerMask.GetMask("Enemy"));
        if (hit.collider != null && (hit.collider.CompareTag(Tags.Player) || hit.collider.CompareTag(Tags.UnderPlayer)))
        {
            return true;
        }
        return false;
    }
    public void EndAttack()
    {
        attackZone.SetActive(false);
        currentBehaviour = IdleBehaviour;
        an.SetInteger(AnimatorVariable.State, (int)StateEnum.Idle);
    }
    public void Attack()
    {
        attackZone.SetActive(true);
    }
}