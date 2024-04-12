using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private GameObject optionPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playGame()
    {
        SceneManager.LoadScene("TypingPrototype");
    }

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

}
