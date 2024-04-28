using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public GameObject accountEntryPrefab;
    public AccountManagerBehaviour accountManagerBehaviour;  // Reference to the AccountManagerBehaviour
    public Transform contentPanel;  // Ensure this points to the panel containing the account entries
    void Start()
    {
        contentPanel = GameObject.Find("Content").transform; // Replace with FindGameObjectWithTag if using a tag
        PopulateLeaderboard();
    }
    void OnEnable()
    {
        PopulateLeaderboard();
    }

    public void PopulateLeaderboard()
    {
        if (accountManagerBehaviour != null)
        {
            List<Account> sortedAccounts = accountManagerBehaviour.AccountManager.GetLeaderboard();

            // Clear existing entries to ensure the leaderboard is up-to-date
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            // Instantiate a new entry prefab for each account in the sorted list
            foreach (var account in sortedAccounts)
            {
                if (!AccountManagerBehaviour.Instance.AccountManager.IsAdmin(account.Username))
                  {
                        GameObject entry = Instantiate(accountEntryPrefab, transform);
                        // Assuming your prefab has two TextMeshProUGUI components named accordingly
                        entry.transform.Find("UsernameText").GetComponent<TextMeshProUGUI>().text = account.Username;
                        entry.transform.Find("HighScoreText").GetComponent<TextMeshProUGUI>().text = account.HighScore.ToString();
                   }
                }
        }
        else
        {
            Debug.LogError("AccountManagerBehaviour reference not set in the Leaderboard script.");
        }
    }
    public void ToggleAdminButtons(bool show)
    {
        foreach (Transform entry in contentPanel)
        {
            entry.Find("DeleteButton").gameObject.SetActive(show);
            entry.Find("ClearHighScoreButton").gameObject.SetActive(show);
        }
    }
}
