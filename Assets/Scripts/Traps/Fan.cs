using UnityEngine;
public class Fan : MonoBehaviour
{
    public static float PushForce =15;
    public float TimeOn;
    public float TimeOff;
    Animator An;
    [SerializeField] ParticleSystem FanParticle;
    GameObject WindZone;
    void Start()
    {
        WindZone = transform.GetChild(1).gameObject;
        An=GetComponent<Animator>();
        On();
        
    }
    void On()
    {
        An.SetTrigger("On");
        FanParticle.Play(); // Phát hạt tạo hiệu ứng
        WindZone.SetActive(true);
        Invoke("Off",TimeOn);
    }
    void Off()
    {
        An.SetTrigger("Off");
        FanParticle.Stop(); // Dừng phát hạt tạo hiệu ứng
        WindZone.SetActive(false);
        Invoke("On",TimeOff);
    }
}