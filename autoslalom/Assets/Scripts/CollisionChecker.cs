using UnityEngine;
public class CollisionChecker : MonoBehaviour
{
    [SerializeField] private Collider2D carCollider;
    private void Start()
    {
        EventBus.GameStarted += EnableCollision;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventBus.GameEnded?.Invoke();
        carCollider.enabled = false;
    } 
    private void EnableCollision()
    {
        carCollider.enabled = true;
    }
}
