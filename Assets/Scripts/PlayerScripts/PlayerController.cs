using Unity.Mathematics;
using UnityEngine;
// Script này lưu giữ các thuộc tính, phương thức của player 
// Tạo các đối tượng của các class dùng điều khiển player
public class PlayerController : MonoBehaviour
{   // Tạo 1 instance duy nhất của class này để các class khác truy cập dễ dàng
    public static PlayerController Pc; 
    public float move; // hướng đi
    public float Speed; // tốc độ nhân vật
    public float JumpForce; // Lực nhảy
    public float DoubleJumpForce; // Lực nhảy DoubleJump
    public bool DoubleJump ; // Kiểm tra liệu được nhảy doubleJump ko
    public bool IsGround;
    public bool IsWall;
    public bool IsFacingRight;
    public float SpeedSlideDown; //Tốc độ khi đang trượt tường xuống
    public float JumpWallTime; // Thời gian ko cho bám tường sau khi JumpWall
    public bool IsOnMovablePlat;
    public PlayerStateMachine Psm;
    public ParticlePlayer ParticlePlayer;
    public Rigidbody2D Rb;
    public Animator An;
    void Awake()
    {
        Pc=this;
        Psm= new PlayerStateMachine();
        Psm.StartState();
        DoubleJump = true;
        ParticlePlayer=GetComponent<ParticlePlayer>();
        
    }
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        Psm.UpdateState();
        if(IsGround){
            DoubleJump = true;
        }
    }
    public void MoveForward(float MoveSpeed,float Direction) // Player đi về phía trước
    {
        Rb.velocity=new Vector2(MoveSpeed*Direction,Rb.velocity.y);
        if(Direction!=0){
            Pc.flip();
        }
    }
    public void flip() // Lật hướng đi của Player
    {
        if(move == 1){
            transform.localRotation=quaternion.Euler(0,0,0);
            IsFacingRight = true;
        } else if(move == -1 ){
            transform.localRotation=quaternion.Euler(0,math.PI,0);
            IsFacingRight = false;
        }
    }
    public void PushUp(float ForcePush) // làm player nhảy lên
    {
        Rb.velocity = new Vector2(Rb.velocity.x,ForcePush);
    }
    public void StopDoubleJump()
    {
        Psm.ChangeState(Psm.FallState);
    }
    void OnCollisionStay2D(Collision2D Object)
    {
        if(Object.collider.CompareTag("DeadObjects")){
            Debug.Log("Dead");
        }
    }
}