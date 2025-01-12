using UnityEngine;
using UnityEngine.UI;

public class InstructionsPanelManager : MonoBehaviour
{
    public GameObject mainMenuPanel;    // Reference to the main menu panel
    public Button backButton;          // Reference to the Back button
    public Text instructionsText;      // Reference to the instructions text

    void Start()
    {
        // Set the instructions text directly
        if (instructionsText != null)
        {
            instructionsText.text = "The main objective of the game is to complete each level as quickly as possible while overcoming obstacles and collecting power-ups. Players aim to achieve the fastest time to climb the leaderboard.";
        }

        // Ensure the panel is initially hidden
        gameObject.SetActive(false);

        // Add listener to the Back button
        if (backButton != null)
        {
            backButton.onClick.AddListener(HideInstructions);
        }
    }

    // Show the instructions panel and hide the main menu
    public void ShowInstructions()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false); // Hide the main menu
        }
        gameObject.SetActive(true); // Show the instructions panel
    }

    // Hide the instructions panel and show the main menu
    public void HideInstructions()
    {
        gameObject.SetActive(false); // Hide the instructions panel
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true); // Show the main menu
        }
    }
}
