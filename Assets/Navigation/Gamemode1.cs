using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
public class Gamemode1 : MonoBehaviour
{
    private bool isMenuVisible = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           isMenuVisible = true;
        }

    }
       void Start()
    {
        Time.timeScale = 1;
         isMenuVisible = false;

    }
    public void OnGUI()
    {
        if (isMenuVisible)
        {
             Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            
            float menuWidth = 400;  // Define the width of the menu
            float menuHeight = 200; // Define the height of the menu
            float menuX = Screen.width / 2 - menuWidth / 2;  // Calculate the x position
            float menuY = Screen.height / 2 - menuHeight / 2; // Calculate the y position
            
            GUI.BeginGroup(new Rect(menuX, menuY, menuWidth, menuHeight));
            GUI.Box(new Rect(0, 0, menuWidth, menuHeight), "Gamemode 1");

            if (GUI.Button(new Rect(10, 40, menuWidth - 20, 30), "Resume Game"))
            {
                ToggleMenu();
            }

            if (GUI.Button(new Rect(10, 80, menuWidth - 20, 30), "Restart Game"))
            {
                RestartGame();
            }

            if (GUI.Button(new Rect(10, 120, menuWidth - 20, 30), "Back to Main Menu"))
            {
                BackToMain();
            }
            if (GUI.Button(new Rect(10, 160, menuWidth - 20, 30), "Quit"))
            {
                Quit();
            }



            GUI.EndGroup();
        }
        else{
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void ToggleMenu()
    {
        Time.timeScale = 1;
        isMenuVisible = false;
         
    }
    

    public void BackToMain()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
        void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
