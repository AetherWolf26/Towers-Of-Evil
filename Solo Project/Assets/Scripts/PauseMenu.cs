using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject Pausemenu;
    public GameObject Resume;
    public GameObject MainMenu;
    public GameObject Quit;

    void Start()
    {
        Pausemenu = GameObject.FindGameObjectWithTag("UI_Pause");
        Resume = GameObject.FindGameObjectWithTag("UI_Resume");
        MainMenu = GameObject.FindGameObjectWithTag("UI_MainMenu");
        Quit = GameObject.FindGameObjectWithTag("UI_Quit");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
            
        }
            
    }

    public void PauseGame()
    {
        isPaused = true;
        Pausemenu.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Game Paused");
        Resume.Visible(true);
        MainMenu.Visible(true);
        Quit.Visible(true);
    }
    
    public void ResumeGame()
    {
        isPaused = false;
        Pausemenu.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Game Resumed");
        Resume.Visible(false);
        MainMenu.Visible(false);
        Quit.Visible(false);
    }
}
