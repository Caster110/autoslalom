using System.Collections.Generic;
using UnityEngine;
public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private Transform obstacleStorage;
    private List<GameObject> disabledObstacles = new List<GameObject>();
    private List<GameObject> enabledObstacles = new List<GameObject>();
    private Vector3 lastSpawnPosition;
    private System.Random randomizer;
    private float spawnPeriod = 12f;
    private void Start()
    {
        EventBus.CameraStabilized += () => Despawn(enabledObstacles);
        EventBus.GameLeaved += () => Despawn(enabledObstacles);
        EventBus.GameStarted += () => Despawn(enabledObstacles);
        randomizer = new System.Random();
        foreach (Transform obstacle in obstacleStorage)
            disabledObstacles.Add(obstacle.gameObject);
    }   
    private void FixedUpdate()
    {
        if (GameStateManager.Current == GameStates.Running)
        {
            if (transform.position.x - lastSpawnPosition.x >= spawnPeriod)
            Spawn();
        }
    }
    private void Spawn()
    {
        GameObject currentObstacle = disabledObstacles[randomizer.Next(disabledObstacles.Count)];
        disabledObstacles.Remove(currentObstacle);
        enabledObstacles.Add(currentObstacle);
        currentObstacle.SetActive(true);
        lastSpawnPosition = transform.position;
        currentObstacle.transform.position = lastSpawnPosition;
    }
    private void Despawn(List<GameObject> obstacles)
    {
        for (int i = obstacles.Count - 1; i >= 0; i--)
            Despawn(obstacles[i]);
    }
    public void Despawn(GameObject obstacle)
    {
        obstacle.SetActive(false);
        disabledObstacles.Add(obstacle);
        enabledObstacles.Remove(obstacle);
    }
}