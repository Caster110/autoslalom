using UnityEngine;
public class ScoreCounter : MonoBehaviour
{
    private float distanceForPoint = 1f;
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
        int result = Mathf.RoundToInt((reachedDistance - startDistance)/distanceForPoint);
        return result;
    }
    private void SendResult()
    {
        EventBus.ResultGotten?.Invoke(CountResult());
    }
}
