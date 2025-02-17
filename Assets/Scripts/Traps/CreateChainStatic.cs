using System.Collections.Generic;
using UnityEngine;

public class CreateChainStatic : MonoBehaviour
{
    PathFollower pathFollower;
    [SerializeField]GameObject Chain; // Các chain
    Transform parent; // Nơi chứa các Chain
    [SerializeField] float distance=1; // Khoảng cách các Chain
    void Start()
    {
        parent = GameObject.Find("StoreChain").transform;
        pathFollower=GetComponent<PathFollower>();
        List<Vector2> cached_wayPoint = pathFollower.WayPoint;
        if(cached_wayPoint.Count>=2){
            for(int i =0; i< pathFollower.WayPoint.Count-1;i++){
                CreateChain.CreateChain2Point(cached_wayPoint[i],cached_wayPoint[i+1],Chain,parent,distance);
            }
            if(pathFollower.loop){
                CreateChain.CreateChain2Point(cached_wayPoint[cached_wayPoint.Count-1],cached_wayPoint[0],Chain,parent,distance);
            }
        }
    }
}
