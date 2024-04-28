using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private GameObject optionPanel;
    [SerializeField]
    private GameObject loginPanel;
    [SerializeField]
    private GameObject leaderboardPanel;
    [SerializeField]
    private GameObject AdminToolsPanel;
    [SerializeField]
    private GameObject AdminAccessButton;
    public TMP_InputField usernameInput; // Make sure these are correctly assigned in the Unity inspector
    public TMP_InputField passwordInput;
    public TextMeshProUGUI feedbackText; // This should be TextMeshProUGUI for UI text display
    public TMP_InputField AdminusernameInput; // Make sure these are correctly assigned in the Unity inspector
    public TMP_InputField AdminpasswordInput;
    public TextMeshProUGUI AdminfeedbackText; // This should be TextMeshProUGUI for UI text display
    private GameObject content;
    
    //UI References
    public GameObject TyperPrefab;
    public GameObject SpawnerPrefab;
    public GameObject gamePrefab;
    public GameObject WordBankPrefab;
    public GameObject typer;
    public GameObject spawner;
    public GameObject game;
    public GameObject wordBank;

    // Start is called before the first frame update
    void Start()
    {
        AccountManagerBehaviour.Instance.AccountManager.CreateAdminAccount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void playGame()
    {
        SceneManager.LoadScene("TypingPrototype");
    }*/

    //Will close the game when clicked
    public void quitGame()
    {
        Application.Quit();
    }
     //Will show the Option menu when clicked and disable the Main menu
    public void showOptions()
    {
        menuPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    //Will show the Main menu when clicked and disable the Option menu
    public void ShowMenu()
    {
        menuPanel.SetActive(true);
        optionPanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        loginPanel.SetActive(false);
        AdminToolsPanel.SetActive(false);

    }

    public void ShowLogin()
    {
        menuPanel.SetActive(false);
        loginPanel.SetActive(true);
        AdminToolsPanel.SetActive(false);

    }

    public void ShowLeaderboard()
    {
        menuPanel.SetActive(false);
        leaderboardPanel.SetActive(true);
        AdminToolsPanel.SetActive(false);
        AdminAccessButton.SetActive(true);

    }
    public void showAdminToolsPanel()
    {
        AdminAccessButton.SetActive(false);
        AdminToolsPanel.SetActive(true);
    }    
    public void CreateAccount()
    {
        string username = usernameInput.text;  // Directly access the text property of TMP_InputField
        string password = passwordInput.text;  // Same here for password

        // Check minimum length requirements

        if (username.Length <= 3)
        {
            feedbackText.text = "Username must be longer than 3 characters.";
            return;
        }
        if (password.Length <= 6)
        {
            feedbackText.text = "Password must be longer than 6 characters.";
            return;
        }
        bool success = AccountManagerBehaviour.Instance.AccountManager.CreateAccount(username, password);
        if (success)
        {
            feedbackText.text = "Account created successfully!";
            Debug.Log("Account created successfully!");
        }
        else
        {
            feedbackText.text = "Account creation failed. User already exists.";
            Debug.Log("Account creation failed. User already exists.");
        }
    }

    public void Login()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        // Check minimum length requirements
        if (username.Length <= 0)
        {
            feedbackText.text = "Please enter username"; // Correctly accessing the text property
            return;
        }
        if (password.Length <= 0)
        {
            feedbackText.text = "Please enter password";
            return;
        }

        bool success = AccountManagerBehaviour.Instance.AccountManager.Login(username, password);
        if (success)
        {
            feedbackText.text = "Login Successful";
            Debug.Log("Login successful!");
            AccountManagerBehaviour.Instance.currentAccount = username;
            StartGame();
        }
        else
        {
            feedbackText.text = "Invalid usernamne or password, please try again";
            Debug.Log("Login failed. Check username and password.");
        }
    }
    public void AdminLogin()
    {
        content = GameObject.Find("Content");
        string username = AdminusernameInput.text;
        string password = AdminpasswordInput.text;

        if (username.Length <= 0)
        {
            feedbackText.text = "Please enter username";
            return;
        }
        if (password.Length <= 0)
        {
            feedbackText.text = "Please enter password";
            return;
        }

        bool success = AccountManagerBehaviour.Instance.AccountManager.Login(username, password);
        if (success && AccountManagerBehaviour.Instance.AccountManager.IsAdmin(username)) // Check if the user is also an admin
        {
            AdminfeedbackText.text = "Admin Privilages Granted";
            Debug.Log("Admin login successful!");
            //AccountManagerBehaviour.Instance.currentAccount = username;
            content.GetComponent<Leaderboard>().ToggleAdminButtons(true);  // Enable admin buttons

        }
        else
        {
            AdminfeedbackText.text = "Invalid Credentials";
            Debug.Log("Login failed. Check username and password, or not an admin.");
        }
    }

    private void StartGame()
    {

        //disable menu
        menuPanel.SetActive(false);
        optionPanel.SetActive(false);
        loginPanel.SetActive(false);
        //start game
        //wordBank = Instantiate(WordBankPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        game = Instantiate(gamePrefab);
        //typer = Instantiate(TyperPrefab, new Vector3(0, -5.25f, 0), Quaternion.identity);
        //spawner = Instantiate(SpawnerPrefab, new Vector3(400, 1000f, 0), Quaternion.identity);


    }
    public void EndGame()
    {
        //enable menu
        ShowMenu();
        Destroy(game);
    }
}

