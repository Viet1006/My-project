using UnityEngine;

interface IXMovable : IFlipable
{
    void XMove(float Speed,int Direction);
}
interface RangeAtk
{
    
}
interface IFlipable
{
    void Flip( Vector2 Target);
}
interface IYMoveable
{
    void Fly(float Speed , int Direction);
}
interface IGetHit
{
    void GetHit()
    {

    }
}
interface Die
{
    void Die();
}
interface Chase
{
    void Chase(Vector2 chaseTarget);
}
interface Patroll
{
    void Patroll();
}