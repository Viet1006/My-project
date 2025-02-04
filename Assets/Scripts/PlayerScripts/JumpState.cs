using Unity.VisualScripting;
using UnityEngine;
public class JumpState: BaseState
{
    float DelayDoubleJump; // Delay để ngay lúc ấn nhảy ko bị chuyển sang DoubleJump
    float DelayJumpWall; // Delay ko bám tường lúc mới nhảy
    public float JumpForce=0; // Lực nhảy (cần được truyền vào trước khi chuyển state)
    PlayerController Pc = PlayerController.Pc;
    public override void EnterState()
    {
        Pc.ParticlePlayer.JumpParticle.Play(); // Phát hạt tạo effect
        DelayJumpWall=Pc.JumpWallTime;
        DelayDoubleJump=0.03f; // Delay tránh việc ấn nhảy thì bị chuyển sang DoubleJump
        if(JumpForce==0){
            JumpForce=Pc.JumpForce; // nếu lực nhảy chưa được truyền thì truyền theo lực nhảy default của Player
        }
        Pc.PushUp(JumpForce);
        Pc.An.SetInteger(AnimatorVariable.State,(int)StateEnum.Jump); 
    }
    public override void UpdateState()
    {
        DelayJumpWall-=Time.deltaTime;
        DelayDoubleJump-=Time.deltaTime;
        Pc.MoveForward(Pc.speed,Pc.move);
        if(Pc.IsWall && Pc.move != 0 && DelayJumpWall<0){ // Đang nhảy thì chạm tường và di chuyển sát vào để bám
            Pc.Psm.ChangeState(Pc.Psm.SlideState);
            Pc.DoubleJump=true;
        }
        if(Pc.Rb.velocity.y<0.01f){ // Vận tốc nhỏ thì chuyển sang Fall
            Pc.Psm.ChangeState(Pc.Psm.FallState);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && Pc.DoubleJump && DelayDoubleJump<0){ // Chuyển sang DoubleJump
            Pc.Psm.ChangeState(Pc.Psm.DoubleJumpState);
        }
    }
    public override void ExitState()
    {
        DelayDoubleJump=0.03f;
        JumpForce=0;
    }
}
