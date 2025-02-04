using UnityEngine;
public class CheckColliderGround : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D Object)
    {
        switch(Object.tag)
        {
            case Tags.Trampoline: 
            PlayerController.Pc.Psm.JumpState.JumpForce = TrampolineController.PushForce;
            PlayerController.Pc.Psm.ChangeState(PlayerController.Pc.Psm.JumpState); break;
            case Tags.Windup:
            PlayerController.Pc.Psm.ChangeState(PlayerController.Pc.Psm.InWindUpState); break;
            case Tags.MovablePlatform:
            transform.parent.SetParent(Object.transform); break;
        }
        if(Object.gameObject.layer == LayerMask.NameToLayer("Ground")){
            PlayerController.Pc.IsGround=true;
        }
        if(Object.GetComponent<BaseEnemy>() != null){
            PlayerController.Pc.Psm.JumpState.JumpForce = PlayerController.Pc.jumpForceEnemy;
            PlayerController.Pc.Psm.ChangeState(PlayerController.Pc.Psm.JumpState);
            Object.GetComponent<BaseEnemy>().GetHit();
        }
    }
    void OnTriggerExit2D(Collider2D Object)
    {
        switch(Object.tag)
        {
            case Tags.Windup:
            PlayerController.Pc.Psm.ChangeState(PlayerController.Pc.Psm.FallState); break;
            case Tags.Ground:
            PlayerController.Pc.IsGround=false; break;
            case Tags.MovablePlatform:
            transform.parent.SetParent(null); break;
        }
        if(Object.gameObject.layer == LayerMask.NameToLayer(Tags.Ground)){
            PlayerController.Pc.IsGround=false;
        }
    }
}