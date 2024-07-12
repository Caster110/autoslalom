using UnityEngine;

public class Score : MonoBehaviour, IGameSystems
{
    public int result { get; private set; }
    private float distanceForPoint = 1f;
    private float reachedDistance;
    private float countedDistance;
    private void Start()
    {
        GameSystems.Instance.Register(this);

        EventBus.GameStarted += SetDefault;
        EventBus.GameRestarted += SetDefault;
    }
    private void Update()
    {
        if(GameStateManager.Current == GameStates.Running)
        {
            reachedDistance = transform.position.x - countedDistance;
            if (reachedDistance >= distanceForPoint)
            {
                IncreaseScore();
            }
        }
    }
    private void IncreaseScore()
    {
        countedDistance += distanceForPoint;
        result++;
        EventBus.ScoreIncreased?.Invoke(result);
    }
    private void SetDefault()
    {
        result = 0;
        countedDistance = transform.position.x;
    }
}
