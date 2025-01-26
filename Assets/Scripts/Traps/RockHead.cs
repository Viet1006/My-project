using System.Collections.Generic;
using UnityEngine;
public class RockHead : MonoBehaviour
{
    [SerializeField] List<DirectionEnum> ListDirection; // Lưu các hướng di chuyển của Head
    int CurrentIndex; // Index trong mảng Direction để cập nhật sang Direction tiếp theo
    public float MaxSpeed; // Tốc độ tối đa
    public float Accleration; // Gia tốc
    float CurrentSpeed; // Tốc độ hiện tại
    [SerializeField] Rigidbody2D HeadRb;
    Vector2 Direction; // Hướng đi hiện tại của Head
    float EdgeOfBox;
    RaycastHit2D[] Result = new RaycastHit2D[5]; // Mảng lưu các object dể kiểm tra đổi hướng
    public BoxCollider2D HeadCollider;
    public Animator An;
    public  float TimeShake,Am,Fre; // Thời gian,biên độ, tần số khi va chạm với wall/ground;
    void Start()
    {
        CurrentIndex=-1;
        Direction = ChangeDirection(ListDirection,ref CurrentIndex);
        EdgeOfBox= HeadCollider.size.x;
    }
    void FixedUpdate()
    {
        // Tạo raycast để kiểm tra chạm tường với 1 cạnh theo Direction
        // Size 0.95 để tránh kiểm tra các cạnh xung quanh , 0.15 dịch về phía trước 1 tí để kiểm tra chạm tường
        Result = Physics2D.BoxCastAll(transform.position,0.9f*HeadCollider.size,0,Direction,0.15f*EdgeOfBox);
        for(int i =0 ; i < Result.Length ; i++){
            if(Result[i].collider.CompareTag(Tags.Ground)||Result[i].collider.CompareTag(Tags.Wall)){
                CurrentSpeed = 0; // Đặt lại vận tốc
                HeadRb.velocity = Vector2.zero; // Dừng Head lại
                An.SetFloat("Vertical",Direction.y);
                An.SetFloat("Horizontal",Direction.x);
                An.SetTrigger("Hit");
                ShakeCinemachine.Cinemachine.ShakeCam(transform.position,Am,Fre,TimeShake);
                Direction = ChangeDirection(ListDirection,ref CurrentIndex);
            }
        }
        if(CurrentSpeed <= MaxSpeed) {
            CurrentSpeed += Accleration * Time.deltaTime; // Mỗi 1s vận tốc tăng lên Accleration
            HeadRb.velocity = Direction * CurrentSpeed; // Cập nhật vận tốc
        }
    }
    public void ChangeTag()
    {
        gameObject.tag = Tags.DeadObjects; // Thay đổi tag để nếu lúc va chạm gây dame cho Player
    }
    public void EndChangeTag()
    {
        gameObject.tag = Tags.Untagged;
    }
    // Thay đổi Direction dựa vào list Direction được thêm từ trước
    Vector2 ChangeDirection(List<DirectionEnum> _ListDirection,ref int Index) 
    {
        Index++;
        CurrentIndex = CurrentIndex % _ListDirection.Count; // Đặt lại CurrentIndex = 0 nếu vượt quá Index
        switch (_ListDirection[Index])
        {
            case DirectionEnum.Up: return new Vector2(0,1);
            case DirectionEnum.Down: return new Vector2(0,-1);
            case DirectionEnum.Right: return  new Vector2(1,0);
        }
        return new Vector2(-1,0);
    }
}