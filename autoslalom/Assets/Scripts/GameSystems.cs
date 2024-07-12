using System.Collections.Generic;
using System;
using UnityEngine;

public class GameSystems : IGameSystems
{
    private readonly Dictionary<string, IGameSystems> gameSystems = new Dictionary<string, IGameSystems>();
    private static GameSystems instance;
    public static GameSystems Instance
    {
        get
        {
            if (instance == null)
                instance = new GameSystems();
            return instance;
        }
    }

    public void Register<T>(T system) where T: IGameSystems
    {
        string key = typeof(T).Name;
        if (gameSystems.ContainsKey(key))
        {
            Debug.LogError($"{key} is already registered");
            return;
        }
        gameSystems.Add(key, system);
    }
    public T Get<T>() where T: IGameSystems
    {
        string key = typeof(T).Name;
        if (!gameSystems.ContainsKey(key))
        {
            Debug.LogError($"{key} is not registered");
            throw new InvalidOperationException();
        }
        return (T)gameSystems[key];
    }
}
