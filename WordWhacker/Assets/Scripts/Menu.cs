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
    public TMP_InputField usernameInput; // Make sure these are correctly assigned in the Unity inspector
    public TMP_InputField passwordInput;
    public TextMeshProUGUI feedbackText; // This should be TextMeshProUGUI for UI text display
    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void ShowLogin()
    {
        menuPanel.SetActive(false);
        loginPanel.SetActive(true);
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
            feedbackText.text = "Login successful!";
            Debug.Log("Login successful!");
        }
        else
        {
            feedbackText.text = "Login failed. Check username and password.";
            Debug.Log("Login failed. Check username and password.");
        }
    }
}

