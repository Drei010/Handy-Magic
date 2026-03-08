using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
public class TutorialLevel : MonoBehaviour
{
    private int currentPanel = 0;
    private string[] panelNames = { 
        "Gesture A", "Gesture B", "Gesture X", "Gesture Y", "Gesture Z","Blast", "Heal", "Block", "Counter", "Bolt",
        "Thorns", "Weaken", "ATK Up", "DEF Up", "Fireball"};
    private int totalPanels = 15;
    public Texture[] panelTextures;
    private bool panelsVisible = true;
    private bool panelsHelpVisible = false;
    private bool panelsMenuVisible = false;
    private bool  panelsGuideVisible = false;

    void Start()
    {
        panelsVisible = true; // Show panels by default
        ShowPanels(panelsVisible); // Show panels
        panelsMenuVisible = true; // Show menu
        
        Time.timeScale = panelsVisible ? 0 : 1; // Pause game if panels are visible
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panelsVisible = !panelsVisible;
            ShowPanels(panelsVisible);
            panelsMenuVisible = true;
            panelsGuideVisible = false;
            Time.timeScale = panelsVisible ? 0 : 1;
        }
    }

    void OnGUI()
    {
        if (panelsVisible)
        {
            Time.timeScale = 0;
            float panelWidth = Screen.width /4;
            float panelHeight = (Screen.height / 2 )+100;
            float panelX = (Screen.width - panelWidth) / 2;
            float panelY = (Screen.height - panelHeight) / 2;

            GUI.skin.box.fontSize = 20;

            if (panelsMenuVisible){
            GUI.Box(new Rect(panelX+50, panelY, panelWidth-100, panelHeight+50),"Menu");

            if (GUI.Button(new Rect(panelX+100 , panelY + 50, 300, 80), "Resume Game"))
            {
                ResumeGame();
            } 
           if (GUI.Button(new Rect(panelX+100 , panelY + 150, 300, 80), "Restart Game"))
            {
                RestartGame();
            }
            if (GUI.Button(new Rect(panelX+100 , panelY + 250, 300, 80), "Back to Main Menu"))
            {
                BackToMain();
            }

            if (GUI.Button(new Rect(panelX+100 , panelY + 350, 300, 80), "Show Spell Book"))
            {
              OpenSpellBook();
            }
             if (GUI.Button(new Rect(panelX+100 , panelY + 450, 300, 80), "Show Guide"))
            {
              OpenGuide();
            }
            }
            if (panelsGuideVisible){
                //Player Stats bar
            GUI.skin.label.fontSize = 20;
            GUI.Box(new Rect(panelX-400, panelY-30, 400, 28), "");
            GUI.Label(new Rect(panelX-280, panelY-30, 400, 28), "Player Status bars");
            //Player Health bar
            GUI.Box(new Rect(panelX-400, panelY+5, 400, 28), "");
            GUI.Label(new Rect(panelX-380 , panelY+5, 180, 50), "Health points");
             //Player Mana bar
            GUI.Box(new Rect(panelX-400, panelY+35, 400, 28), "");
            GUI.Label(new Rect(panelX-380 , panelY+35, 180, 50), "Mana points");
            //Player Ultimate bar
            GUI.Box(new Rect(panelX-400, panelY+65, 400, 28), "");
            GUI.Label(new Rect(panelX-380 , panelY+65, 180, 50), "Ultimate points");


            //Enemy Indicator

            GUI.Box(new Rect(panelX+120, panelY+100, 200, 200), "");
            GUI.Label(new Rect(panelX+180, panelY+100, 200, 200), "Enemy");


                //Enemy Stats bar
            GUI.skin.label.fontSize = 20;
            GUI.Box(new Rect(panelX+470, panelY-30, 400, 28), "");
            GUI.Label(new Rect(panelX+580, panelY-30, 400, 28), "Enemy Status bars");
            //Enemy Health bar
            GUI.Box(new Rect(panelX+470, panelY+5, 400, 28), "");
            GUI.Label(new Rect(panelX+700 , panelY+5, 180, 50), "Health points");
             //Enemy Mana bar
            GUI.Box(new Rect(panelX+470, panelY+35, 400, 28), "");
            GUI.Label(new Rect(panelX+700 , panelY+35, 180, 50), "Mana points");
            //PlayEnemyer Ultimate bar
            GUI.Box(new Rect(panelX+470, panelY+65, 400, 28), "");
            GUI.Label(new Rect(panelX+700 , panelY+65, 180, 50), "Ultimate points");

            GUI.skin.label.fontSize = 12;
            }
           

         if (panelsHelpVisible){
            GUI.Box(new Rect(0, panelY-100, Screen.width, panelHeight+200),"");
            GUI.skin.label.fontSize = 50;
            GUI.Label(new Rect(panelX-320 , panelY -90, 180, 50), "Tutorial");
            GUI.skin.label.fontSize = 12;


            GUI.Box(new Rect(panelX-450, panelY, panelWidth, panelHeight+50), "Hand Gestures");

            //GEstures
            if (GUI.Button(new Rect(panelX-430 , panelY +50, 130, 50), "Activate"))
            {
                ShowPanel(0);
            } 
             if (GUI.Button(new Rect(panelX-280 , panelY +50, 130, 50), "Launch"))
            {
               ShowPanel(1);
            }
            if (GUI.Button(new Rect(panelX-130 , panelY +50, 130, 50), "Offense"))
            {
               ShowPanel(2);
            }

            if (GUI.Button(new Rect(panelX-430 , panelY +120, 130, 50), "Defense"))
            {
                 ShowPanel(3);
            }
            if (GUI.Button(new Rect(panelX-280 , panelY +120, 130, 50), "Support"))
            {
                 ShowPanel(4);
            }
            //Basic
            GUI.skin.label.fontSize = 24;
            GUI.Label(new Rect(panelX-280 , panelY +170, 130, 50), "Basic Spells");
            GUI.skin.label.fontSize = 12;

            if (GUI.Button(new Rect(panelX-430 , panelY +220, 130, 50), "Blast"))
            {
                ShowPanel(5);
            } 
           if (GUI.Button(new Rect(panelX-280 , panelY +220, 130, 50), "Heal"))
            {
               ShowPanel(6);
            }
            if (GUI.Button(new Rect(panelX-130 , panelY +220, 130, 50), "Shield"))
            {
               ShowPanel(7);
            }

            //Advanced
                GUI.skin.label.fontSize = 24;
                GUI.Label(new Rect(panelX-280 , panelY +270, 180, 50), "Advanced Spells");
                GUI.skin.label.fontSize = 12;

            if (GUI.Button(new Rect(panelX-430 , panelY +320, 130, 50), "Counter"))
            {
                ShowPanel(8);
            } 
           if (GUI.Button(new Rect(panelX-280 , panelY +320, 130, 50), "Bolt"))
            {
               ShowPanel(9);
            }
            if (GUI.Button(new Rect(panelX-130 , panelY +320, 130, 50), "Thorns"))
            {
               ShowPanel(10);
            }
            if (GUI.Button(new Rect(panelX-430 , panelY +390, 130, 50), "Weaken"))
            {
                ShowPanel(11);
            } 
           if (GUI.Button(new Rect(panelX-280 , panelY +390, 130, 50), "ATK up"))
            {
               ShowPanel(12);
            }
            if (GUI.Button(new Rect(panelX-130 , panelY +390, 130, 50), "DEF up"))
            {
               ShowPanel(13);
            }
            if (GUI.Button(new Rect(panelX-430 , panelY +460, 130, 50), "Fireball"))
            {
                ShowPanel(14);
            } 

            GUI.Box(new Rect(panelX+50, panelY, panelWidth*2, panelHeight-150), panelNames[currentPanel]);
            
            if (panelTextures != null && panelTextures.Length > currentPanel && panelTextures[currentPanel] != null)
                {
                    float originalWidth = panelTextures[currentPanel].width;
                    float originalHeight = panelTextures[currentPanel].height;
                    float maxHeight = 400f;
                    float maxWidth= 940f;

                    // Calculate new dimensions while maintaining aspect ratio
                    float newWidth, newHeight;
                    if (originalWidth > originalHeight)
                    {
                        newWidth = Mathf.Min(originalWidth, maxWidth);
                        newHeight = originalHeight * (newWidth / originalWidth);
                    }
                    else
                    {
                        newHeight = Mathf.Min(originalHeight, maxHeight);
                        newWidth = originalWidth * (newHeight / originalHeight);
                    }

                    float textureX = panelX  +60; // Adjusted to place pictures in the rightmost panel
                    float textureY = panelY + (panelHeight - newHeight) / 2;

                    GUI.DrawTexture(new Rect(textureX, textureY-70, newWidth, newHeight), panelTextures[currentPanel]);
                }

            GUI.Box(new Rect(panelX+50, panelY+400, panelWidth*2, panelHeight-350), "Description");

             if (currentPanel == 5)
                {
                GUI.Label(new Rect(panelX+70 , panelY+440, panelWidth*2, panelHeight-350), "First Spell is Blast consisting of just Gesture x. The Blast spell would summon a small magic blast dealing damage to the target.");

                }
                else if (currentPanel == 6)
                {
                GUI.Label(new Rect(panelX+70 , panelY+440, panelWidth*2, panelHeight-350), "2nd Spell is “Heal” consisting of just Gesture z. The Heal spell is self-explanatory, as it will heal the player.");

                }
                else if (currentPanel == 7)
                {
                GUI.Label(new Rect(panelX+70 , panelY+440, panelWidth*2, panelHeight-350), "3rd Spell is “Shield” consisting of just Gesture y. The Shield spell would summon a shield in front of you for a short duration, blocking any damage as the shield is active.");

                }
                if (currentPanel == 8)
                {
                GUI.Label(new Rect(panelX+70 , panelY+440, panelWidth*2, panelHeight-350), "The 4th spell is called “Counter”. The Counter spell will summon a barrier similar to the Shield Spell but for an even shorter duration it will also release a blast that will damage the enemy.");

                }
                else if (currentPanel == 9)
                {
                GUI.Label(new Rect(panelX+70 , panelY+440, panelWidth*2, panelHeight-350), "The 5th spell is called “Bolt”. The Bolt spell will summon a small and fast bolt of magic that will deal more damage and increase the damage of your next attack.");

                }
                else if (currentPanel == 10)
                {
                GUI.Label(new Rect(panelX+70 , panelY+440, panelWidth*2, panelHeight-350), "The 6th spell is “Thorns”. The Thorns spell when cast will allow you to deal a small amount of damage to the enemy everytime you take damage.");

                }
                if (currentPanel == 11)
                {
                GUI.Label(new Rect(panelX+70 , panelY+440, panelWidth*2, panelHeight-350), "The 7th spell is “Weaken”. The Weaken spell will inflict a debuff on the target, reducing its damage for a set amount of time.");

                }
                else if (currentPanel == 12)
                {
                GUI.Label(new Rect(panelX+70 , panelY+440, panelWidth*2, panelHeight-350), "The 8th spell is “ATK UP”. The ATK UP spell will just greatly increase your damage for a set duration.");

                }
                else if (currentPanel == 13)
                {
                GUI.Label(new Rect(panelX+70 , panelY+440, panelWidth*2, panelHeight-350), "The 9th spell is “DEF UP”. The DEF UP spell will decrease the damage the player takes for a set duration.");

                }
                else if (currentPanel == 14)
                {
                GUI.Label(new Rect(panelX+70 , panelY+440, panelWidth*2, panelHeight-350), "The 10th spell is “Fireball”. The Fireball spell is treated as an “Ultimate” spell. It can only be cast should you fill up the ultimate bar. The spell would summon a large fireball towards the target and deal a great amount of damage.");

                }


         }
        }

    }

    void ShowPanel(int index)
    {
        currentPanel = Mathf.Clamp(index, 0, totalPanels - 1);
    }

    void ShowPanels(bool visible)
    {
        panelsVisible = visible;
         panelsHelpVisible = false;
    }

    public void BackToMain()
    {

        SceneManager.LoadScene("Menu");
    }
    void ResumeGame()
    {
        panelsVisible = false;
        Time.timeScale = 1;
    }
    void OpenSpellBook()
    {
        panelsHelpVisible = true;
        panelsMenuVisible = false;
         panelsGuideVisible = false;           
    }
        void OpenGuide()
    {
        panelsHelpVisible = false;
        panelsMenuVisible = false;    
        panelsGuideVisible = true;    
    }
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}