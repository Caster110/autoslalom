using UnityEngine;

public static class GameStateManager
{
    public static GameStates Current { get; private set; }
    static void ChooseState(GameStates state)
    {
        Current = state;
    }
    static GameStateManager()
    {
        EventBus.GameStarted += () => ChooseState(GameStates.Running);
        EventBus.GamePaused += () => ChooseState(GameStates.Paused);
        EventBus.GameContinued += () => ChooseState(GameStates.Running);
        EventBus.CameraStabilized += () => ChooseState(GameStates.Appearing);
        EventBus.GameEnded += () => ChooseState(GameStates.Ended);
        EventBus.GameLeaved += () => ChooseState(GameStates.Idle);
        EventBus.CarAppeared += () => ChooseState(GameStates.Idle);
    }
    public static void Initialize() { Debug.Log("GameStateManager initialized"); }
}