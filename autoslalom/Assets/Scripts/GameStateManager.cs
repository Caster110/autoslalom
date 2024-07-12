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
        EventBus.GameOpened += () => ChooseState(GameStates.Appearing);
        EventBus.GameStarted += () => ChooseState(GameStates.Running);
        EventBus.GamePaused += () => ChooseState(GameStates.Paused);
        EventBus.GameContinued += () => ChooseState(GameStates.Running);
        EventBus.GameRestarted += () => ChooseState(GameStates.Running);
        EventBus.CameraStabilized += () => ChooseState(GameStates.Appearing);
        EventBus.GameEnded += () => ChooseState(GameStates.Ended);
        EventBus.CarAppeared += () => ChooseState(GameStates.Idle);
    }
    public static void Initialize() { Debug.Log("GameStateManager initialized"); }
}