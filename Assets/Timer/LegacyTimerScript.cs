using UnityEngine;

public class LegacyTimerScript : MonoBehaviour
{
    private float timeElapsed = 0.0f; // Initialize the timer

    void Update()
    {
        timeElapsed += Time.deltaTime; // Increment the timer
    }

    void OnGUI()
    {
        // Set the position to upper middle side of the screen
        float screenWidth = Screen.width;
        GUI.Box(new Rect(screenWidth / 4, 10, screenWidth / 2, 50), FormatTime(timeElapsed));
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
