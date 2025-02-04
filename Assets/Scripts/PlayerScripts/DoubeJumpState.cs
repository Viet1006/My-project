public class DoubleJumpState : BaseState
{
    PlayerController Pc = PlayerController.Pc;
    public override void EnterState()
    {
        Pc.ParticlePlayer.JumpParticle.Play(); // Phát hạt tạo effect
        Pc.PushUp(Pc.DoubleJumpForce);
        Pc.An.SetInteger(AnimatorVariable.State,(int)StateEnum.DoubleJump);
        Pc.DoubleJump=false;
    }
    public override void UpdateState()
    {
        Pc.MoveForward(Pc.speed,Pc.move);
    }
    public override void ExitState()
    {
        
    }
}
