//Tạo các đối tượng của State, quản lý State nào đang chạy và chuyển đổi State
using System.Diagnostics;

public class PlayerStateMachine 
{
    public BaseState CurrentState;
    public IdleState IdleState=new IdleState();
    public RunState RunState=new RunState();
    public JumpState JumpState=new JumpState();
    public FallState FallState=new FallState();
    public SlideState SlideState = new SlideState();
    public DoubleJumpState DoubleJumpState = new DoubleJumpState();
    public InWindUpState InWindUpState = new InWindUpState();
    public void StartState()
    {
        CurrentState=IdleState;
    }
    public void UpdateState()
    {
        CurrentState.UpdateState();
    }
    public void ChangeState(BaseState newState)  //  Thực hiện chuyển State khi được gọi
    {
        CurrentState.ExitState();
        CurrentState=newState;
        CurrentState.EnterState();
    }
}