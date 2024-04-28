using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountEntry : MonoBehaviour
{
    [SerializeField]
    private GameObject usernameText;
    [SerializeField]
    private GameObject highScoreText;

    public void OnResetHighScoreButtonPressed()
    {
        AccountManagerBehaviour.Instance.AccountManager.ResetHighScore(usernameText.GetComponent<TextMeshProUGUI>().text);
        // Additional code to update UI
        highScoreText.GetComponent<TextMeshProUGUI>().text = "0";
    }

    // Example method that might be called when an admin presses a button to delete an account
    public void OnDeleteAccountButtonPressed()
    {
        AccountManagerBehaviour.Instance.AccountManager.DeleteAccount(usernameText.GetComponent<TextMeshProUGUI>().text);
        // Additional code to update UI
        Destroy(gameObject);
    }
}
