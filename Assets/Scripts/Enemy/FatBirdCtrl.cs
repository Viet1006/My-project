using UnityEngine;

public class FatBirdCtrl : AttackableEnemy
{   
    PathFollower pathFollower;
    float speedDown = 7f; // Tốc độ khi bay xuống
    [SerializeField] float acceleration; // Gia tốc rơi xuống
    [SerializeField] ParticleSystem flyParticle;
    [SerializeField] float shakeTime , am , fre;
    void Start()
    {
        pathFollower = GetComponent<PathFollower>();
        currentBehaviour = IdleBehaviour;
        pathFollower.speed = speed;
        runParticle.Stop();
        flyParticle.Stop();
    }
    void Update()
    {
        currentBehaviour();
    }
    void IdleBehaviour()
    {
        if(DetechPlayer())
        {
            currentBehaviour = AttackBehaviour;
            an.SetInteger(AnimatorVariable.State, (int)StateEnum.DetectPlayer);
            pathFollower.enabled = false;
        }
    }
    public override void AttackBehaviour()
    {
        speedDown += acceleration * Time.deltaTime;
        transform.position += Vector3.down * speedDown * Time.deltaTime;
    }
    void FlyUpBehaviour()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
    public override bool DetechPlayer()
    {
        return Physics2D.Raycast(transform.position,Vector2.down,Mathf.Infinity,~LayerMask.GetMask("Enemy","Default")).collider.CompareTag("Player");
    }
    public void FlyParticle()
    {
        runParticle.Play();
        flyParticle.Play();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(Tags.Ground))
        {
            speedDown = 7;
            currentBehaviour = IdleBehaviour;
            an.SetTrigger("Ground");
            ShakeCinemachine.Cinemachine.ShakeCam(transform.position,am,fre,shakeTime);
        }
    }
    public void EndGround()
    {
        pathFollower.enabled = true;
        an.SetInteger(AnimatorVariable.State, (int)StateEnum.Idle);
    }
}
