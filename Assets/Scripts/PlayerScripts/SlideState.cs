using UnityEngine;
public class SlideState : BaseState
{
    PlayerController Pc = PlayerController.Pc; //rút gọn mỗi lần gọi Pc
    float GravitySave = PlayerController.Pc.Rb.gravityScale; // Biến để lưu Gravity và đặt lại khi thoát State
    public override void EnterState()
    {
        Pc.An.SetInteger(AnimatorVariable.State,(int)StateEnum.Climb); 
        Pc.Rb.velocity=new Vector2(0,-Pc.SpeedSlideDown); // Truyền vận tốc khi trượt xuống
        Pc.Rb.gravityScale=0; // Đặt Gravity bằng 0 để ko bị trọng lực kéo xuống khi bám
    }
    public override void UpdateState()
    {
        Pc.Flip();
        // move == 0 để xác nhận ko bám và đổi trạng thái
        if(!Pc.IsWall || (Pc.IsFacingRight && Pc.move == -1) || ( !Pc.IsFacingRight && Pc.move == 1) ){
            Pc.Psm.ChangeState(Pc.Psm.FallState);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            Pc.Psm.ChangeState(Pc.Psm.JumpState);
        }
        if(Pc.IsGround)
        {
            Pc.ParticlePlayer.TouchGroundParticle.Play();
            Pc.Psm.ChangeState(Pc.Psm.IdleState);
        }
    }
    public override void ExitState()
    {
        Pc.Rb.gravityScale=GravitySave;
    }
}
