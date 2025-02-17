using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class RinoCtrl : AttackableEnemy
{
    [Tooltip("Speed are increased by acceleration * time.deltaTime"),SerializeField] private float acceleration;
    Rigidbody2D rb;
    [SerializeField] Vector2 force;
    private bool isHitWall;
    [SerializeField] float shakeTime , am , fre;
    [SerializeField] ParticleSystem hitWallParticle;
    void Start()
    {
        speed = 0;
        currentBehaviour = IdleBehaviour;
        transformEmitter = transform.GetChild(0);
        rb = GetComponent<Rigidbody2D>();
        runParticle.Stop();
        hitWallParticle.Stop();
    }
    void Update()
    {
        currentBehaviour(); 
    }
    void IdleBehaviour()
    {
        if(DetechPlayer())
        {
            runParticle.Play();
            currentBehaviour = AttackBehaviour;
            an.SetInteger(AnimatorVariable.State, (int)StateEnum.Run);
        }
    }
    public override void AttackBehaviour()
    {
        speed += acceleration * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, transform.position - transform.right, speed * Time.deltaTime);
    }
    public override bool DetechPlayer()
    {
        return Physics2D.Raycast(transformEmitter.position,-transform.right).collider.CompareTag("Player");
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag(Tags.Wall))
        {
            speed = 0;
            an.SetInteger(AnimatorVariable.State, (int)StateEnum.SpecialState);
            rb.AddForce(new Vector2(transform.right.x*force.x,force.y), ForceMode2D.Impulse);
            currentBehaviour = IdleBehaviour;
            isHitWall = true;
            runParticle.Stop();
            ShakeCinemachine.Cinemachine.ShakeCam(transform.position,am,fre,shakeTime);
            hitWallParticle.Play();
        }
        if(other.collider.CompareTag(Tags.Ground) && isHitWall)
        {
            isHitWall = false;
            an.SetInteger(AnimatorVariable.State, (int)StateEnum.Idle);
            Flip();
        }
    }
}
