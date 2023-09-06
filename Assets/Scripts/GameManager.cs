using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        GameOn,
        GameOff,
        TeamSelection
    }
    public static GameState gameState;
    void Start()
    {
        gameState = GameState.TeamSelection;
    }
}
