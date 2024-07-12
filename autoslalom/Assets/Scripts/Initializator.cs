using UnityEngine;

public class Initializator : MonoBehaviour
{
    void Awake()
    {
        EventBus.Initialize();
        GameStateManager.Initialize();
        EventBus.GameOpened?.Invoke();
    }
}
