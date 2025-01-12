using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManu : MonoBehaviour
{
    public GameObject pauseManu;
    public GameObject levelSelectionPanel;
    public static bool isPaused;
    
    void Start()
    {
        pauseManu.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }
    
    public void ResumeGame()
    {
        pauseManu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    
    public void PauseGame()
    {
        pauseManu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    public void RestartLevel()
    {
        pauseManu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void LoadGame()
    {
        levelSelectionPanel.SetActive(true);
    }
    
    public void GoToMainManu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("Main Menu");
    }
    
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}