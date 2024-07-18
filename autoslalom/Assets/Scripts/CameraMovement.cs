using UnityEngine;
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve stoppingCurve;
    private float curveTimer;
    private float currentVelocity;
    private float defaultVelocity = 13.5f;
    private float maximumVelocity = 27f;
    private float acceleration = 0.2f;
    private void Start()
    {
        SetDefault();
        EventBus.GameStarted += SetDefault;
        EventBus.GameLeaved += SetDefault;
    }
    private void Update()
    {
        if (GameStateManager.Current == GameStates.Paused)
        {
            return;
        }
        if (GameStateManager.Current == GameStates.Running)
        {
            if (currentVelocity < maximumVelocity)
                currentVelocity += acceleration * Time.deltaTime;
        }
        else if (GameStateManager.Current == GameStates.Ended)
        {
            currentVelocity -= stoppingCurve.Evaluate(curveTimer) * Time.deltaTime;
            curveTimer += Time.deltaTime;
            if (currentVelocity <= defaultVelocity)
            {
                EventBus.CameraStabilized?.Invoke();
                SetDefault();
            }
        }
        transform.position += Vector3.right * currentVelocity * Time.deltaTime;
    }
    private void SetDefault()
    {
        curveTimer = 0f;
        currentVelocity = defaultVelocity;
    }
}
