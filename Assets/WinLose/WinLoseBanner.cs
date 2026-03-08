using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class WinLoseBanner : MonoBehaviour
{
  public SpellHistory spellHistoryScript;
    public Enemy_Stats enemyStats;
    public Char_Stats charStats;

    public Texture2D victoryImage;
    public Texture2D loseImage;
    public AudioClip winSound;
    public AudioClip loseSound;

    private bool showBanner = false;
    private AudioSource audioSource;

    private Rect boxRect; // Declare boxRect here
    private Rect imageRect; // Declare imageRect here

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ShowBannerAfterDelay());
    }

    IEnumerator ShowBannerAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        showBanner = true;
    }

    void OnGUI()
{
    if (showBanner)
    {
        string sceneName = SceneManager.GetActiveScene().name;

        boxRect = new Rect(Screen.width / 4, Screen.height / 4 -100f, Screen.width / 2 , Screen.height / 2 );
       

        float imageWidth = boxRect.width * 1f;
        float imageHeight = imageWidth * (victoryImage.height / (float)victoryImage.width);
        float imageX = boxRect.x + (boxRect.width - imageWidth) / 2;
        float imageY = boxRect.y + (boxRect.height - imageHeight) / 2 + 100f;

        imageRect = new Rect(imageX, imageY, imageWidth, imageHeight);
        // Add label above the image
        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.white;
        Rect labelRect = new Rect(imageRect.x, imageRect.y - 60f, imageRect.width, 40f);

        if (enemyStats.CurrentHealthPoints <= 0||charStats.CurrentHealthPoints <= 0){

                GUI.Box(boxRect,"");
            if (enemyStats.CurrentHealthPoints <= 0)
                {
                    GUI.DrawTexture(imageRect, victoryImage);
                    HandleOutcome(winSound);
                Debug.Log("Game status: Player Wins");
                }
                else if (charStats.CurrentHealthPoints <= 0)
                {
                    GUI.DrawTexture(imageRect, loseImage);
                    HandleOutcome(loseSound);
                Debug.Log("Game status: Player Lost");
                }
                     GUI.Label(labelRect, sceneName, style);
                    Time.timeScale = 0;

if (spellHistoryScript != null)
{
    
    // Get the Downloads folder path
    string downloadsFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

    // Specify the file path in the Downloads folder where you want to save the text file
    string filePath = Path.Combine(downloadsFolderPath, "SpellHistory.txt");

    // Create or overwrite the file with the spell history
    using (StreamWriter writer = new StreamWriter(filePath))
    {
        // Loop through each item in spellHistory and write it to the file
        foreach (string spell in spellHistoryScript.spellHistory)
        {
            writer.WriteLine(spell);
                            Debug.Log(spell);
        }
    }

    // Log a message indicating that the spell history has been saved to the file
    Debug.Log("Spell history has been saved to: " + filePath);
}
        }
    }
}


    void HandleOutcome(AudioClip clip)
    {
        float buttonWidth = boxRect.width * 0.5f;
        float buttonHeight = 30f;
        float buttonX = boxRect.x + (boxRect.width - buttonWidth) / 2;
        float buttonY = imageRect.y + imageRect.height + 50f;

        Rect buttonRect = new Rect(buttonX, buttonY, buttonWidth, buttonHeight);

        PlaySound(clip);

        if (GUI.Button(buttonRect, "Back to Menu"))
        {
            BackToMain();
        }
    }

    void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("Menu");
    }
}
