using UnityEngine;
public class RunState : BaseState
{
    PlayerController Pc=PlayerController.Pc;
    public override void EnterState()
    {
        Pc.An.SetInteger(AnimatorVariable.State,(int)StateEnum.Run);
        Pc.ParticlePlayer.RunParticle.Play();
    }
    public override void UpdateState()
    {
        Pc.MoveForward(Pc.speed,Pc.move);
        if(Pc.move==0)
        {
            Pc.Psm.ChangeState(Pc.Psm.IdleState);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Pc.Psm.ChangeState(Pc.Psm.JumpState);
        }
        if(Pc.Rb.velocity.y<-0.1f)
        {
            Pc.Psm.ChangeState(Pc.Psm.FallState);
        }
    }
    public override void ExitState()
    {
        Pc.ParticlePlayer.RunParticle.Stop();
    }
}