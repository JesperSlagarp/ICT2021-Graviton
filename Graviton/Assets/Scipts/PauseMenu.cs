using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool InventoryActive = false;
    public static bool StatsWindowActive = false;


    public GameObject pauseMenuUI;
    public GameObject inventoryUI;
    public GameObject statsWindowUI;
    // Update is called once per frame
    void Update()
    {
        //pause menu if press esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }else
            {
                Pause();
            }
        }

        //inventory if press E
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!InventoryActive)
            {
                InventoryDisplay();
            }
            else
            {
                InventoryFade();
            }
            
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!StatsWindowActive)
            {
                StatsWindowDisplay();
            }
            else
            {
                StatsWindowFade();
            }

        }
    }
//pause menu functions, start
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void LoadMenu()
    {
	    Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    //pause menu functions, end

    // inventory functions, start
    void InventoryDisplay()
    {
        inventoryUI.SetActive(true);
        Time.timeScale = 0f;
        InventoryActive = true;
    }
    void InventoryFade()
    {
        
        inventoryUI.SetActive(false); 
        Time.timeScale = 1f;
        InventoryActive = false;
    }

    void StatsWindowDisplay()
    {
        statsWindowUI.SetActive(true);
        Time.timeScale = 0f;
        StatsWindowActive = true;
    }
    void StatsWindowFade()
    {

        statsWindowUI.SetActive(false);
        Time.timeScale = 1f;
        StatsWindowActive = false;
    }
}
