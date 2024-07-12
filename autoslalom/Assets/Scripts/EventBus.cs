using System;
using Unity.VisualScripting;
using UnityEngine;

public static class EventBus
{
    public static void Initialize() { Debug.Log("EventBus initialized"); }

    public static Action GameEnded;
    public static Action GameStarted;
    public static Action GameContinued;
    public static Action GamePaused;
    public static Action GameRestarted;
    public static Action GameOpened;
    public static Action CameraStabilized;
    public static Action CarAppeared;
    public static Action<int> ScoreIncreased;
}
