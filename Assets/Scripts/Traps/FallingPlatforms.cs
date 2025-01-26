using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class FallingPlatforms : MonoBehaviour
{
    Transform cached_Transform;
    [SerializeField] float FallDelay = 0.5f; // Khoảng thời gian lơ lửng còn lại sau khi Player nhảy lên
    [SerializeField] float SimulationTime = 0.1f; // Thời gian dịch để xuống 1 tí tạo hiệu ứng khi Player đứng lên trên
    [SerializeField] float gravitySimulation = 1f; // Trọng lực 
    [SerializeField] float am = 0.15f; // biên độ giao động lên xuống
    [SerializeField] float fre =1f; // Tần số giao động
    [SerializeField] float timeDisable=3f;
    bool isFalling;
    bool IsTouchPlayer;
    float InitialY ; // Lưu toạ độ y để lơ lửng
    Collider2D Collider;
    Vector2 PositionSpawn; // Vị trí sẽ Spawn
    void Start()
    {
        cached_Transform = transform;
        InitialY=transform.position.y;
        SimulationTime=0.1f;
        PositionSpawn = transform.position;
        Collider = GetComponent<Collider2D>();
    }
    void FixedUpdate()
    {
        if(!isFalling){
            cached_Transform.position = new Vector2(cached_Transform.position.x,InitialY+am*Mathf.Sin(fre*Time.time*Mathf.PI));
        }
        if(IsTouchPlayer && SimulationTime>0){ // Hạ tâm giao động xuống 3*SimlationTime để tạo hiệu ứng
            InitialY-= Time.deltaTime* 3f;
            SimulationTime-=Time.deltaTime;
        }
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag(Tags.UnderPlayer)){
            IsTouchPlayer=true;
            StartCoroutine(falling(other.transform.parent));
        }
    }
    private IEnumerator falling(Transform Player)
    {
        yield return new WaitForSeconds(FallDelay);
        isFalling =true;
        float _timeDisable = timeDisable; // Thời gian disable sau khi bắt đầu rơi
        float velocity = 0;
        while(_timeDisable>0){
            _timeDisable-= Time.fixedDeltaTime;
            velocity += gravitySimulation * Time.fixedDeltaTime;
            cached_Transform.position -= new Vector3(0 ,velocity*Time.fixedDeltaTime);
            yield return null;
        }
        Player.SetParent(null);
        gameObject.SetActive(false);
    }
    void ReSpawn() // Phương thức để gọi lại khi nhân vật ReSpawn
    {
        transform.position=PositionSpawn;
        SimulationTime=0.1f;
        IsTouchPlayer=false;
        isFalling = false;
        Collider.enabled=true;
        InitialY=PositionSpawn.y;
    }
}