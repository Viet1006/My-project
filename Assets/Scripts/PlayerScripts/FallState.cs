using UnityEngine;

public class FallState:BaseState
{
    PlayerController Pc= PlayerController.Pc; // Rút gọn mỗi lần tham chiếu đến PlayerController 
    public override void EnterState()
    {
        Pc.An.SetInteger("State",(int)StateEnum.Fall);
    }
    public override void UpdateState()
    {
        Pc.MoveForward(Pc.Speed,Pc.move);
        //  move khác 0 để xác nhận user muốn bám
        if(Pc.IsWall && Pc.move != 0) {
            Pc.Psm.ChangeState(Pc.Psm.SlideState);
            Pc.DoubleJump=true;
        }
        if(Pc.IsGround) {
            Pc.Psm.ChangeState(Pc.Psm.IdleState);
            Pc.DoubleJump=true;
            Pc.ParticlePlayer.TouchGroundParticle.Play(); 
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && Pc.DoubleJump)
        {
            Pc.Psm.ChangeState(Pc.Psm.DoubleJumpState);
        }
    }
    public override void ExitState()
    {
        
    }
}
