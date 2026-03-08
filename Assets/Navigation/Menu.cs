using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
public class Menu : MonoBehaviour
{   



    public void OnGUI()
    {

        Cursor.lockState = CursorLockMode.None;
                    
            float menuWidth = 400;  // Define the width of the menu
            float menuHeight = 200; // Define the height of the menu
            float menuX = Screen.width / 2 - menuWidth / 2;  // Calculate the x position
            float menuY = Screen.height - menuHeight ; // Calculate the y position
            
            GUI.BeginGroup(new Rect(menuX, menuY, menuWidth, menuHeight));
           
            if (GUI.Button(new Rect(10, 40, menuWidth - 20, 30), "Tutorial"))
            {
              StartGame0();
                
            }             
            if (GUI.Button(new Rect(10, 80, menuWidth - 20, 30), "Gamemode 1"))
            {
              StartGame1();
            }
            if (GUI.Button(new Rect(10, 120, menuWidth - 20, 30), "Gamemode 2"))
            {
              StartGame2();
                
            }             

          
            if (GUI.Button(new Rect(10, 160, menuWidth - 20, 30), "Quit"))
            {
                Quit();
            }


            GUI.EndGroup();
    }
    public void StartGame0()
    {

            SceneManager.LoadScene("Tutorial");
    }
    public void StartGame1()
    {

        SceneManager.LoadScene("Gamemode 1");

        
    }
    public void StartGame2()
    {
  

        SceneManager.LoadScene("Gamemode 2");
    }
    public void Quit()
    {
        UnityEngine.Debug.Log("Quit");

        Application.Quit();
        // Assuming main.exe is the name of the process
        Process[] processes = Process.GetProcessesByName("main");

        foreach (Process process in processes)
        {
            process.Kill();
        }

    }
}
