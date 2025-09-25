using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
public class GameManager : MonoBehaviour
{
    GameObject weaponUI;
    GameObject PauseMenu;
    Image HealthBar;
    TextMeshProUGUI ammoCounter;
    TextMeshProUGUI clip;
    TextMeshProUGUI fireMode;

    public bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 1)
        {

            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            HealthBar = GameObject.FindGameObjectWithTag("UI_health").GetComponent<Image>();
            ammoCounter = GameObject.FindGameObjectWithTag("UI_Ammo").GetComponent<TextMeshProUGUI>();
            clip = GameObject.FindGameObjectWithTag("UI_Clip").GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.fillAmount = (float) Player.health / (float) Player.maxHealth;
        if (Player.currentWeapon  != null )
        {
            ammoCounter.text = "Ammo: " + Player.currentWeapon.ammo;
            clip.text = "Clip: " + Player.currentWeapon.clip + " / " + Player.currentWeapon.clipSize;

        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            isPaused = true;
            isPaused = true;
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
            Resume();
    }
    public void Resume()
    {
        if (!isPaused)
        {
            isPaused = true;
            isPaused = true;
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
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
