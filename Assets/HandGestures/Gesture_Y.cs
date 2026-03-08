using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Gesture_Y : MonoBehaviour, IGesture
{
    public GameObject objectPrefab; // Prefab for the 3D object
    public Transform leftHandPointA; // Transform of point A on the left hand
    public Transform rightHandPointA; // Transform of point A on the right hand
    public Transform leftHandPointB; // Transform of point B on the left hand
    public Transform rightHandPointB; // Transform of point B on the right hand
    public float detectionDistance = 0.05f; // Distance threshold for detection
    public Vector3 spawnOffset = new Vector3(0f, 0.5f, 0f); // Editable spawn offset
    public bool spawnObject = false;
    public string output = ""; // Output string

  // Flag to track if the gesture is in progress
    private bool isGestureInProgress = false;

    // Counter for defense occurrences
    private int defenseCount = 0;

    // Method to get the current output string
    public string GetOutput()
    {
        return isGestureInProgress ? $"Defense[{defenseCount}]" : "";
    }

    void Update()
    {
        // Calculate the distance between point A and point B of left and right hands
        float distanceA = Vector3.Distance(leftHandPointA.position, rightHandPointA.position);
        float distanceB = Vector3.Distance(leftHandPointB.position, rightHandPointB.position);

        // Check if points are not exactly at (0, 0, 0)
        bool pointsNotAtOrigin = leftHandPointB.position != Vector3.zero && rightHandPointB.position != Vector3.zero;

        // If the distances are below the detection threshold and points are not at origin
        if ((distanceA < detectionDistance && distanceB < detectionDistance) && pointsNotAtOrigin)
        {
            // If the gesture is not already in progress
            if (!isGestureInProgress)
            {
                // Set the gesture as in progress
                isGestureInProgress = true;

            // Create a new object at the specified spawn location
            if(spawnObject){
            Vector3 spawnPosition = (leftHandPointB.position + rightHandPointB.position) / 2 + spawnOffset;
            GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            }
            // Update the output string
                defenseCount++;

                // Update the output string
                output = $"Defense[{defenseCount}]";
        }
        }
        else
        {
            // If the gesture was in progress, reset the gesture flag immediately
            if (isGestureInProgress)
            {
                ResetGestureFlag();
            }
        }
    }
        // Coroutine to reset the gesture flag after a delay
    private IEnumerator ResetGestureFlagAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetGestureFlag();
    }

    // Method to reset the gesture flag
    void ResetGestureFlag()
    {
        isGestureInProgress = false;
    }
}
