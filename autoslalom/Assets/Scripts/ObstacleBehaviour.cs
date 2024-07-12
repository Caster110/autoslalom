using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    [SerializeField] private ObstacleManager manager;
    [SerializeField] private Transform despawnPoint;
    void Update()
    {
        if (despawnPoint.position.x - transform.position.x >= 0f)
            manager.Despawn(gameObject);
    }
}
