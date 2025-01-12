using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private TMP_Text briefText;
    [SerializeField] private string nextLevelName;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (LevelManager.Instance.HasKey())
            {        
                if (nextLevelName != "GameOver")
                {
                    SoundManager.Instance.PlayLevelCompleteSound();
                }
                else
                {
                    SoundManager.Instance.PlayGameCompleteSound();

                    TimerController timerController = FindObjectOfType<TimerController>();
                    if (timerController != null)
                    {
                        timerController.StopTimer();
                    }

                    // LeaderboardManager leaderboardManager = LeaderboardManager.Instance;
                    // Debug.Log("Adding New Score...");
                    // if (leaderboardManager != null)
                    //     leaderboardManager.AddNewScore();
                    // else
                    //     Debug.Log("leaderboardManager is null");

                    // // Debugging: Check if the CompletionTime key exists and log its value
                    // if (PlayerPrefs.HasKey("CompletionTime"))
                    // {
                    //     float savedCompletionTime = PlayerPrefs.GetFloat("CompletionTime");
                    //     Debug.Log("Completion Time loaded: " + savedCompletionTime);
                    //
                    // }
                    // else
                    // {
                    //     Debug.Log("No Completion Time found in PlayerPrefs at level:" + nextLevelName);
                    // }


                    // Store the username and time to PlayerPrefs
                    string username = PlayerPrefs.GetString("Username", "Player");
                    float completionTime = PlayerPrefs.GetFloat("CompletionTime", 0f);
                    
                    List<(string, float)> leaderboard = LoadLeaderboardFromPlayerPrefs();
                    
                    if (!leaderboard.Exists(entry => entry.Item1 == username && entry.Item2 == completionTime))
                    {
                        leaderboard.Add((username, completionTime));
                        leaderboard.Sort((x, y) => x.Item2.CompareTo(y.Item2));
                        if (leaderboard.Count > 10)
                            leaderboard = leaderboard.GetRange(0, 10);

                        SaveLeaderboardToPlayerPrefs(leaderboard);
                    }
                }
                SceneManager.LoadScene(nextLevelName);

            }
            else
                ShowBriefText("You need to find the key first!");
        }
    }
    
    private List<(string, float)> LoadLeaderboardFromPlayerPrefs()
    {
        List<(string, float)> leaderboard = new List<(string, float)>();

        for (int i = 0; i < 10; i++)
        {
            string username = PlayerPrefs.GetString($"Leaderboard_Username_{i}", "");
            float time = PlayerPrefs.GetFloat($"Leaderboard_Time_{i}", float.MaxValue);

            if (!string.IsNullOrEmpty(username) && time != float.MaxValue)
                leaderboard.Add((username, time));
        }

        return leaderboard;
    }
    
    private void SaveLeaderboardToPlayerPrefs(List<(string, float)> leaderboard)
    {
        for (int i = 0; i < leaderboard.Count; i++)
        {
            Debug.Log($"Adding score: {leaderboard[i].Item1} - {leaderboard[i].Item2}");
            PlayerPrefs.SetString($"Leaderboard_Username_{i}", leaderboard[i].Item1);
            PlayerPrefs.SetFloat($"Leaderboard_Time_{i}", leaderboard[i].Item2);
        }

        PlayerPrefs.Save();
    }
    
    private void ShowBriefText(string message)
    {
        briefText.text = message;
        briefText.gameObject.SetActive(true);
        StartCoroutine(HideBriefTextAfterDelay(2f));
    }

    private IEnumerator HideBriefTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        briefText.gameObject.SetActive(false);
    }
}