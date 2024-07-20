using UnityEngine;
public class ScoreCounter : MonoBehaviour
{
    private int pointsForDistance = 100;
    private float startDistance;
    private void Start()
    {
        EventBus.GameStarted += SetStartDistance;
        EventBus.GameEnded += SendResult;
    }
    private void SetStartDistance()
    {
        startDistance = transform.position.x;
    }
    private int CountResult()
    {
        float reachedDistance = transform.position.x;
        int result = Mathf.RoundToInt(reachedDistance - startDistance) * pointsForDistance;
        return result;
    }
    private void SendResult()
    {
        EventBus.ResultGotten?.Invoke(CountResult());
    }
}
