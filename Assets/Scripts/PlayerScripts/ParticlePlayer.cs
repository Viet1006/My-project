using UnityEngine;
// Tạo và lưu các Particle của Player
public class ParticlePlayer : MonoBehaviour
{
    public ParticleSystem RunParticle;
    public ParticleSystem TouchGroundParticle;
    public ParticleSystem JumpParticle;
    void Start()
    {
        RunParticle.Stop();
        TouchGroundParticle.Stop();
        JumpParticle.Stop();
    }
}
