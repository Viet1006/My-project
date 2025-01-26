using Unity.Mathematics;
using UnityEngine;
// Lớp này để hình thành các phương thức tạo chain
public class CreateChain 
{
    public static void CreateChain2Point(Vector2 Start, Vector2 End,GameObject Chain,Transform Parent,float distance)
    {
        Vector2 Direction = (End-Start).normalized;
        Vector2 Current = Start; // Điểm tạo chain
        do{
            Current += Direction * distance;
            GameObject newObject = Object.Instantiate(Chain,Current,quaternion.identity);
            newObject.transform.SetParent(Parent);
        }while(Vector2.Distance(Current,End)>=distance);
        GameObject startObject = Object.Instantiate(Chain,Start,quaternion.identity);
        startObject.transform.SetParent(Parent);
        GameObject endObject = Object.Instantiate(Chain,End,quaternion.identity);
        endObject.transform.SetParent(Parent);
    }
}
