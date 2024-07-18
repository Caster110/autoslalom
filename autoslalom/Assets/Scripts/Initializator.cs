using UnityEngine;
public class Initializator : MonoBehaviour
{
    private void Awake()
    {
        ScoreSaver.Initialize();
        EventBus.Initialize();
        GameStateManager.Initialize();
        Destroy(gameObject);
    }
}
