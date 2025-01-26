using System.Collections.Generic;
using UnityEngine;
public class SpikeHead : MonoBehaviour
{
    [SerializeField] List<DirectionEnum> ListDirection; // Lưu các hướng di chuyển của Head
    int CurrentIndex; // Index trong mảng Direction để cập nhật sang Direction tiếp theo
    public float MaxSpeed; // Tốc độ tối đa
    public float Accleration; // Gia tốc
    float CurrentSpeed; // Tốc độ hiện tại
    [SerializeField] Rigidbody2D HeadRb;
    Vector2 Direction; // Hướng đi hiện tại của Head
    RaycastHit2D[] Result; // List lưu các object dể kiểm tra đổi hướng
    public BoxCollider2D HeadCollider;
    public Animator An;
    float EdgeOfBox; // Lấy cạnh của Box
    public float TimeShake,Am,Fre; // Thời gian,biên độ, tần số khi va chạm với wall/ground;
    void Start()
    {
        CurrentIndex=-1;
        Direction = ChangeDirection(ListDirection,ref CurrentIndex);
        EdgeOfBox = HeadCollider.size.x;
    }
    void FixedUpdate()
    {
        // Tạo raycast để kiểm tra chạm tường với 1 cạnh theo Direction
        // Size 0.95 để tránh kiểm tra các cạnh xung quanh , 0.1 dịch về phía trước 1 tí để kiểm tra chạm tường
        Result = Physics2D.BoxCastAll(transform.position,0.9f*HeadCollider.size,0,Direction,0.1f*EdgeOfBox);
        for(int i =0 ; i < Result.Length ; i++){
            if(Result[i].collider.CompareTag(Tags.Ground) || Result[i].collider.CompareTag(Tags.Wall)){
                CurrentSpeed = 0; // Đặt lại vận tốc
                HeadRb.velocity = Vector2.zero; // Dừng Head lại
                An.SetFloat("Vertical",Direction.y); 
                An.SetFloat("Horizontal",Direction.x);
                An.SetTrigger("Hit");
                ShakeCinemachine.Cinemachine.ShakeCam(transform.position,Am,Fre,TimeShake); // Rung cam
                Direction = ChangeDirection(ListDirection,ref CurrentIndex);
            }
        }
        if(CurrentSpeed <= MaxSpeed ){
            CurrentSpeed += Accleration * Time.deltaTime; // Mỗi 1s vận tốc tăng lên Accleration
            HeadRb.velocity = Direction * CurrentSpeed; // Cập nhật vận tốc
        }
    }
    Vector2 ChangeDirection(List<DirectionEnum> _ListDirection,ref int Index)
    {
        Index++;
        CurrentIndex = CurrentIndex % _ListDirection.Count;
        switch (_ListDirection[Index])
        {
            case DirectionEnum.Up: return new Vector2(0,1);
            case DirectionEnum.Down: return new Vector2(0,-1);
            case DirectionEnum.Right: return  new Vector2(1,0);
        }
        return new Vector2(-1,0);
    }
}