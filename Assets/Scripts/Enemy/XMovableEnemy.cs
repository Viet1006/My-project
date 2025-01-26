using UnityEngine;

public abstract class XMovableEnemy : BaseEnemy , IXMovable
{
    private int _xDirection;
    protected int xDirection{ //getter,setter của xDirection
        get{
            return _xDirection;
        }
        set{
            if(value >=1){
                _xDirection =1;
            }else if (value<=-1)
            {
                _xDirection = -1;
            }else {
                _xDirection =0;
            }
        }
    }
    protected Transform cached_Transform;
    public void Flip(Vector2 Target)
    {
        if(transform.rotation.eulerAngles.y!=180&&transform.position.x <= Target.x)
        {
            cached_Transform.rotation=Quaternion.Euler(0,180,0);
        }if(transform.rotation.eulerAngles.y!= 0 && transform.position.x > Target.x )
        {
            transform.rotation=Quaternion.Euler(0,0,0);
        }
    }
    public virtual void XMove(float speed, int direction)
    {
        cached_Transform.position += Vector3.right * speed * direction * Time.fixedDeltaTime;
    }
    protected void Awake() // Thay thế cho phương thức khởi tạo các Child nên gọi lại phương thức này để tránh bị null pointer
    {
        cached_Transform = transform;
    }
}
