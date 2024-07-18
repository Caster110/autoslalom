using System;
using UnityEngine;

public static class EventBus
{
    public static void Initialize() { Debug.Log("EventBus initialized"); }

    public static Action GameEnded;
    public static Action GameStarted;
    public static Action GameContinued;
    public static Action GamePaused;
    public static Action GameLeaved;
    public static Action CameraStabilized;
    public static Action CarAppeared;
    public static Action <int> ResultGotten;
    public static Action <string> PlayerGotten;
}
