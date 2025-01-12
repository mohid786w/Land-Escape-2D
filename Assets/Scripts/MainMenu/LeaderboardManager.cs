using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    public Button leaderboardButton;
    public Transform leaderboardContent;
    public GameObject leaderboardEntryPrefab;
    public GameObject leaderboardPanel;
    private List<(string, float)> leaderboard = new List<(string, float)>();
    public static LeaderboardManager Instance { get; private set; }
    
    private void Start()
    {        
        if (leaderboardButton != null)
            leaderboardButton.onClick.AddListener(ShowLeaderboard);
        
        LoadLeaderboard();
    }

    public void LoadLeaderboard()
    {
        leaderboard.Clear();
        leaderboard = LoadLeaderboardFromPlayerPrefs();
        DisplayLeaderboard();
    }
    
    private List<(string, float)> LoadLeaderboardFromPlayerPrefs()
    {
        List<(string, float)> leaderboard = new List<(string, float)>();

        for (int i = 0; i < 10; i++)
        {
            string username = PlayerPrefs.GetString($"Leaderboard_Username_{i}", "");
            float time = PlayerPrefs.GetFloat($"Leaderboard_Time_{i}", float.MaxValue);

            if (!string.IsNullOrEmpty(username) && time != float.MaxValue)
            {
                leaderboard.Add((username, time));
                Debug.Log($"Loaded leaderboard entry: {username} - {time:F2} seconds");
            }
        }
        Debug.Log($"Total leaderboard entries loaded: {leaderboard.Count}");

        return leaderboard;
    }

    public void DisplayLeaderboard()
    {
        foreach (Transform child in leaderboardContent)
            Destroy(child.gameObject);
        
        if (leaderboard.Count == 0)
        {
            GameObject newEntry = Instantiate(leaderboardEntryPrefab, leaderboardContent);
            newEntry.SetActive(true);

            TMP_Text entryText = newEntry.GetComponent<TMP_Text>();
            if (entryText != null)
                entryText.text = "No records!";
        }
        else
        {
            int count = 0;
            foreach (var entry in leaderboard)
            {
                if (count >= 3) break;

                Debug.Log($"Username: {entry.Item1}, Time: {entry.Item2}");

                GameObject newEntry = Instantiate(leaderboardEntryPrefab, leaderboardContent);
                newEntry.SetActive(true);
            
                TMP_Text entryText = newEntry.GetComponent<TMP_Text>();
                if (entryText != null)
                    entryText.text = $"{entry.Item1}: {entry.Item2:F2} seconds";

                count++;
            }
        }
    }
    
    public void ClearLeaderboard()
    {
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.DeleteKey($"Leaderboard_Username_{i}");
            PlayerPrefs.DeleteKey($"Leaderboard_Time_{i}");
        }

        PlayerPrefs.Save();

        leaderboard.Clear();
        DisplayLeaderboard();

        Debug.Log("Leaderboard has been cleared.");
    }
    
    public void ShowLeaderboard()
    {
        if (leaderboardPanel != null)
        {
            leaderboardPanel.SetActive(true);
            LoadLeaderboard();
            DisplayLeaderboard();
        }
    }

    public void HideLeaderboard()
    {
        leaderboardPanel.SetActive(false);
    }

}