using UnityEngine;
using System.Collections.Generic;
// Script tạo đường đi cho các Object di chuyển có quỹ đạo
public class PathFollower : MonoBehaviour
{
    private Transform cached_Transform;
    [Tooltip("Check this box to enable EditingMode,allowing you to modify waypoint")]
    public bool Editing = false;
    [Tooltip("Check this box the object will move in a loop")]
    public bool loop;  
    [HideInInspector] public List <Vector2> WayPoint = new List<Vector2>();
    private int targetIndex;  // Vị trí đang đi đến trong Waypoint
    /// <summary>
    /// This position is retrieved from waypoint based on the current target index
    /// </summary>
    public Vector2 targetPos // Vị trí đích hiện tại
    {
        get{
            return WayPoint[targetIndex];
        }
    }
    [Tooltip("This value determines how fast the object moves (units per second).")]
    public float speed;
    [Tooltip("The radius surround a waypoint that you want move or remove during editing mode")]
    public float handleRadius = 0.5f; 
    [Tooltip("The distance which consider the object reach the current target")]
    public float stopDistance = 0.1f; // Khoảng cách xem như là đến đích rồi
    bool isIncreasing =true; // Xem đi theo chiều tăng hay giảm trong mảng WayPoint
    bool allowExternalControl = false; // Cho phép bên ngoài điều khiển việc di chuyển
    public delegate void onTarget();
    /// <summary>
    /// This event is trigger when the object reach its target and you don't take movement controll form pathfollower system
    /// </summary>
    public onTarget OnTarget; 
    private void Start()
    {
        cached_Transform = transform;
        if(WayPoint.Count==0){
            Debug.Log(transform.position + "This positon has object without waypoint");
            enabled=false;
        }else{
            transform.position=WayPoint[0];
            targetIndex=0;
        }
        if(WayPoint.Count == 1){
            enabled = false;
        }
    }
    private void Update()
    {
        if(allowExternalControl){
            return;
        }
        if(IsOnTarget()){
            NextTargetSet();
            if(OnTarget!= null){
                OnTarget(); // Thực hiện Event khi đến đích ở các instance đăng ký
            }
        }
        MoveToNextPoint(speed);
    }
    /// <summary>
    /// This method will move the object towards the current target by changing the object's transform frame by frame
    /// </summary>
    /// <param name="speed">
    /// The speed determines how fast the object move towards the target
    /// The speed is multiplied by Time.fixedDeltaTime
    /// </param>
    public void MoveToNextPoint(float speed)
    {
        cached_Transform.position = Vector2.MoveTowards(transform.position,WayPoint[targetIndex],speed*Time.fixedDeltaTime);
    }
    /// <summary>
    /// This method allow you check whether the object has reached the target or not
    /// It compare the distance between the object's current position and the target's position
    /// </summary>
    /// <returns>
    /// Return true if the object is within defined stop distance form the target. If not it will returr false
    /// </returns>
    public bool IsOnTarget()
    {
        return Vector2.Distance(cached_Transform.position,WayPoint[targetIndex])<=stopDistance;
    }
    /// <summary>
    /// This method will set the target to next point is list of waypoint
    /// </summary>
    public void NextTargetSet() 
    {
        targetIndex = _GetNextIndex();
    }
    private int _GetNextIndex()
    {
        if(loop){
            if(targetIndex == WayPoint.Count-1){
                return 0;
            }else {
                return targetIndex +1;
            }
        }else{
            if(isIncreasing){
                if(targetIndex == WayPoint.Count-1){
                    isIncreasing=false;
                    return targetIndex -1;
                }else{
                    return targetIndex +1;
                }
            }else{
                if(targetIndex == 0){
                    isIncreasing = true;
                    return targetIndex +1;
                }else{
                    return targetIndex -1;
                }
            }
        }
    }
    /// <summary>
    /// This method take the movement control from PathFollower system
    /// Once call, the movement control will be managed by external classes and can not be return to PathFollower System
    /// </summary>
    public void TakeMovingControl()
    {
        allowExternalControl = true;
    }
}