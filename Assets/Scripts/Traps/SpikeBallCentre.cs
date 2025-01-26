using UnityEngine;
public class SpikeBallCentre : MonoBehaviour
{
    [SerializeField] GameObject SpikeBall;
    [SerializeField] GameObject ChainSpikeBall; // Chain của SpikeBall
    static float DistanceChain = 0.5f;
    [SerializeField] float InitialSpeedRotate ; //Tốc độ quay ban đầu (Độ/s)
    float SpeedRotate; // Tốc độ quay hiện tại (Độ / s)
    [SerializeField] float RotationAcceleration ; //Gia tốc của Ball (Độ/s ^ 2) với Radius là 1
    void Start()
    {
        float Radius = Vector2.Distance(transform.position,SpikeBall.transform.position);
        if (Radius < DistanceChain){ // Bán kính nhỏ quá thì huỷ tránh lỗi
            Debug.Log("Radius SpikeBall quá nhỏ" + transform.position);
            Destroy(gameObject);
        }
        CreateChainSpikeBall(transform.position,SpikeBall.transform.position);
        SpeedRotate=InitialSpeedRotate;
        RotationAcceleration /= Mathf.Sqrt(Radius); // Cài đặt gia tốc theo chiều dai dây
    }
    void Update()
    {
        if(SpikeBall.transform.position.x>transform.position.x){ // Đổi hướng gia tốc
            RotationAcceleration = - Mathf.Abs(RotationAcceleration);
        }
        else{
            RotationAcceleration =  Mathf.Abs(RotationAcceleration);
        }
        SpeedRotate += RotationAcceleration* Time.deltaTime; // Mỗi s tốc độc quay tăng lên RotationAccleration
        transform.Rotate(0, 0, SpeedRotate * Time.deltaTime);
    }
    void CreateChainSpikeBall(Vector2 StartPoint,Vector2 EndPoint) // Vẽ các chain dọc theo đường thẳng
    {
        Vector2 CurrentPoint = StartPoint;
        Vector2 Direction = (EndPoint-StartPoint).normalized;
        while (Vector2.Distance(CurrentPoint,EndPoint) >= DistanceChain){
            Instantiate(ChainSpikeBall,CurrentPoint, Quaternion.identity,transform); // Tạo hình ảnh Chain tại CurrentPoint
            CurrentPoint += Direction * DistanceChain; // Chuyển sang điểm khác
        }
        Instantiate(ChainSpikeBall,EndPoint,Quaternion.identity,transform); // Tạo Chain tại điểm cuối
    }
    void OnDrawGizmosSelected()
    {
        float Radius = Vector2.Distance(transform.position,SpikeBall.transform.position);
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position,Radius); //Vẽ để xác định bán kinh mà SpikeBall quay tròn
        Gizmos.color=Color.green;
        Gizmos.DrawLine(transform.position,SpikeBall.transform.position); // Vẽ đường thằng nối ball và centre
    }
}