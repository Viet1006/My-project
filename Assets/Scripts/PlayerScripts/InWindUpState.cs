// Trạng thái bị gió thổi lên
using UnityEngine;
public class InWindUpState : BaseState
{
    PlayerController Pc = PlayerController.Pc;
    public override void EnterState()
    {
        Pc.An.SetInteger(AnimatorVariable.State,(int)StateEnum.Jump); 
    }
    public override void UpdateState()
    {
        Pc.Rb.AddForce(new Vector2(0,Fan.PushForce),ForceMode2D.Force);
        Pc.MoveForward(Pc.speed,Pc.move);
    }
    public override void ExitState()
    {

    }
}
