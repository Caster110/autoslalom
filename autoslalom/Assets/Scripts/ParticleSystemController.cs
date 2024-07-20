using UnityEngine;
public class ParticleSystemController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    private void Start()
    {
        EventBus.GameContinued += EnableParticles;
        EventBus.GameStarted += EnableParticles;
        EventBus.GameLeaved += EnableParticles;
        EventBus.GamePaused += DisableParticles;
    }
    private void EnableParticles()
    {
        particles.Play();
    }
    private void DisableParticles()
    {
        particles.Stop();
    }
}