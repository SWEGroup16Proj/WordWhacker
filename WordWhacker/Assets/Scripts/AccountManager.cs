using UnityEngine;
using System.IO;

public class AccountManagerBehaviour : MonoBehaviour
{
    public static AccountManagerBehaviour Instance { get; private set; }
    public AccountManager AccountManager { get; private set; }

    public string accountFilePath;
    public string currentAccount;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            accountFilePath = Path.Combine(Application.persistentDataPath, "accounts.json");
            AccountManager = new AccountManager();
            AccountManager.LoadAccountsFromFile(accountFilePath);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        AccountManager.SaveAccountsToFile(accountFilePath);
    }
}
