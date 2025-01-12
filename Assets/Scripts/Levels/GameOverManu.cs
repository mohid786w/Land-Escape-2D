using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManu : MonoBehaviour
{
    public void ReStartGame()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
    
    public void GoToMainManu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
