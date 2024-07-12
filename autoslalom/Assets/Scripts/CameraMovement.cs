using UnityEngine;

public class CameraMovement : MonoBehaviour, IGameSystems
{
    private float defaultVelocity = 5f;
    private float currentVelocity;
    private float maximumVelocity = 10f;
    private float acceleration = 0.01f;
    private float deceleration;

    void Start()
    {
        GameSystems.Instance.Register(this);
        EventBus.GameRestarted += SetDefault;
    }

    void Update()
    {
        if (GameStateManager.Current == GameStates.Paused)
        {
            return;
        }
        if (GameStateManager.Current == GameStates.Appearing ||
            GameStateManager.Current == GameStates.Idle)
        {
            transform.position += Vector3.right * defaultVelocity * Time.deltaTime;
        }
        else if (GameStateManager.Current == GameStates.Running)
        {
            transform.position += Vector3.right * currentVelocity * Time.deltaTime;
            if (currentVelocity <= maximumVelocity)
                currentVelocity += acceleration;
        }
        else if (GameStateManager.Current == GameStates.Ended)
        {

        }
    }
    private void SetDefault()
    {
        currentVelocity = defaultVelocity;
    }
}
