using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject restartPanel;
    public void StartGame()
    {
        startPanel.SetActive(false);
        GameManager.gameState = GameManager.GameState.GameOn;
    }
   public void RestartTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameEnd()
    {
        restartPanel.SetActive(true);
        GameManager.gameState = GameManager.GameState.GameOff;
    }
}
