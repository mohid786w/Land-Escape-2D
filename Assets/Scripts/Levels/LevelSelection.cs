using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public GameObject levelSelectionPanel; 
    private TimerController timerController;
    private LeaderboardManager leaderboardManager;

    private void Start()
    {
        timerController = FindObjectOfType<TimerController>();
        leaderboardManager = FindObjectOfType<LeaderboardManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && levelSelectionPanel.activeSelf)
            HideLevelSelection();
    }
    
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        Time.timeScale = 1f;
        PauseManu.isPaused = false;
    }

    public void ShowLevelSelection()
    {
        gameObject.SetActive(true);
    }

    public void HideLevelSelection()
    {
        gameObject.SetActive(false);
    }
    
    public void ToggleLevelSelection()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}