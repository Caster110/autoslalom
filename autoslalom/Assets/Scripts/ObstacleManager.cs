using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour, IGameSystems
{
    [SerializeField] private Transform spawnPoint;
    private float spawnPeriod = 10f;
    private Vector3 lastSpawnPosition;
    private List<GameObject> disabledObstacles = new List<GameObject>();
    private List<GameObject> enabledObstacles = new List<GameObject>();
    private void Start()
    {
        GameSystems.Instance.Register(this);
        EventBus.CameraStabilized += () => Despawn(enabledObstacles);

        foreach (Transform obstacle in transform)
            disabledObstacles.Add(obstacle.gameObject);
    }   
    private void Update()
    {
        if (GameStateManager.Current == GameStates.Running)
        {
            if (spawnPoint.position.x - lastSpawnPosition.x >= spawnPeriod)
            Spawn();
        }
    }
    private void Spawn()
    {
        GameObject currentObstacle = disabledObstacles[Random.Range(0, disabledObstacles.Count)];
        currentObstacle.SetActive(true);
        disabledObstacles.Remove(currentObstacle);
        enabledObstacles.Add(currentObstacle);
        lastSpawnPosition = spawnPoint.position;
        currentObstacle.transform.position = lastSpawnPosition;
    }
    private void Despawn(List<GameObject> obstacles)
    {
        foreach(GameObject obstacle in obstacles)
            Despawn(obstacle);
    }
    public void Despawn(GameObject obstacle)
    {
        disabledObstacles.Add(obstacle);
        enabledObstacles.Remove(obstacle);
        obstacle.SetActive(false);
    }
}
