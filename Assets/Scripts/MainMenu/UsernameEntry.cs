using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UsernameEntry : MonoBehaviour
{
    public TMP_InputField usernameInput;

    private void Start()
    {
        usernameInput.onEndEdit.AddListener(OnEndEdit);
    }

    private void OnEndEdit(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return))
            SubmitUsername();
    }
    
    public void SubmitUsername()
    {
        string username = usernameInput.text.Trim();
        if (!string.IsNullOrEmpty(username))
        {
            PlayerPrefs.SetString("Username", username);
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
            Debug.Log("Please enter a valid username.");
        }
    }
}