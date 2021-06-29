using UnityEngine;
using System;

public class GameEvents {
    private static GameEvents instance;

    public static GameEvents getInstance() {
        if (instance == null) {
            instance = new GameEvents();
        }

        return instance;
    }

    private GameEvents() { }

    public event Action onReachedGoal;

    public void ReachedGoal() {
        onReachedGoal?.Invoke();
    }
}