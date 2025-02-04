using UnityEngine;
public class IdleState:BaseState
{
    PlayerController Pc= PlayerController.Pc; // Rút gọn mỗi lần tham chiếu đến PlayerController 
    public override void EnterState()
    {
        Pc.An.SetInteger(AnimatorVariable.State,(int)StateEnum.Idle);
        Pc.Rb.velocity = Vector2.zero;
    }
    public override void UpdateState()
    {
        if(Pc.move!=0)
        {
            Pc.Psm.ChangeState(Pc.Psm.RunState);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Pc.Psm.ChangeState(Pc.Psm.JumpState);
        }
    }
    public override void ExitState()
    {
        
    }
}
