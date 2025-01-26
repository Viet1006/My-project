using UnityEngine;
public class CheckColiderWall : MonoBehaviour
{
    void OnTriggerStay2D (Collider2D Object)
    {
        switch(Object.tag)
        {
            case "Wall" : 
            PlayerController.Pc.IsWall = true; break;
        }
    }
    void OnTriggerExit2D(Collider2D Object)
    {
        switch(Object.tag)
        {
            case "Wall" : 
            PlayerController.Pc.IsWall = false; break;
        }
    }
}
