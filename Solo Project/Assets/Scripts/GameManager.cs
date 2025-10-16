using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
public class GameManager : MonoBehaviour
{

    PlayerController player;

    GameObject weaponUI; 
    GameObject pauseMenu;
    Button resume;
    Button mainMeu;
    Button quitGame;

    Image healthBar;
    TextMeshProUGUI ammoCounter;
    TextMeshProUGUI clip;
    TextMeshProUGUI fireMode;

    public bool isPaused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 1)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            weaponUI = GameObject.FindGameObjectWithTag("weaponUI");
            pauseMenu = GameObject.FindGameObjectWithTag("UI_Pause");
            resume = GameObject.FindGameObjectWithTag("UI_Resume").GetComponent<Button>();
            mainMeu = GameObject.FindGameObjectWithTag("UI_MainMenu").GetComponent<Button>();
            quitGame = GameObject.FindGameObjectWithTag("UI_Quit").GetComponent<Button>();

            pauseMenu.SetActive(false);

            healthBar = GameObject.FindGameObjectWithTag("UI_Health").GetComponent<Image>();
            ammoCounter = GameObject.FindGameObjectWithTag("UI_Ammo").GetComponent<TextMeshProUGUI>();
            clip = GameObject.FindGameObjectWithTag("UI_Clip").GetComponent<TextMeshProUGUI>();
            fireMode = GameObject.FindGameObjectWithTag("ui_fireMode").GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 1)
        {
            healthBar.fillAmount = (float)player.health / (float)player.maxHealth;

            if (player.currentWeapon != null)
            {
                weaponUI.SetActive(true);

                ammoCounter.text = "Ammo: " + player.currentWeapon.ammo;
                clip.text = "Clip: " + player.currentWeapon.clip + " / " + player.currentWeapon.clipSize;
            }
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            isPaused = true;

            pauseMenu.SetActive(true);

            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else
            Resume();
    }
    public void Resume()
    {
        if (isPaused)
        {
            isPaused = false;

            pauseMenu.SetActive(false);

            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
            Pause();
    }
    public void LoadLevel(int level)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }
    public void MainMenu()
    {
        LoadLevel(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
