using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Gesture_Z : MonoBehaviour, IGesture
{
    public GameObject objectPrefab; // Prefab for the 3D object
    public Transform leftHandPointA; // Transform of point A on the left hand
    public Transform rightHandPointA; // Transform of point A on the right hand
    public Transform leftHandPointB; // Transform of point B on the left hand
    public Transform rightHandPointB; // Transform of point B on the right hand
    public Transform leftHandPointC; // Transform of point C on the left hand
    public Transform rightHandPointC; // Transform of point C on the right hand
    public float detectionDistance = 0.05f; // Distance threshold for detection
    public float cDistanceThreshold = 0.1f; // Editable distance threshold for point C
    public Vector3 spawnOffset = new Vector3(0f, 0.5f, 0f); // Editable spawn offset
    public bool spawnObject = false;
    public float heightThreshold = 0.1f; // Editable height difference threshold
    public string output = ""; // Output string

    // Flag to track if the gesture is in progress
    private bool isGestureInProgress = false;

    // Counter for support occurrences
    private int supportCount = 0;

    // Method to get the current output string
    public string GetOutput()
    {
        return isGestureInProgress ? $"Support[{supportCount}]" : "";
    }
     void Update()
    {
        // Calculate the distance between point A and point B of left and right hands
        float distanceA = Vector3.Distance(leftHandPointA.position, rightHandPointA.position);
        float distanceB = Vector3.Distance(leftHandPointB.position, rightHandPointB.position);

        // Calculate the distance between point C of left and right hands
        float distanceC = Vector3.Distance(leftHandPointC.position, rightHandPointC.position);

        // Calculate the height difference between point A and point B of left and right hands
        float heightDifferenceLeft = leftHandPointB.position.y - leftHandPointA.position.y;
        float heightDifferenceRight = rightHandPointB.position.y - rightHandPointA.position.y;

        // Check if points are not exactly at (0, 0, 0)
        bool pointsNotAtOrigin = leftHandPointB.position != Vector3.zero && rightHandPointB.position != Vector3.zero;

        // If the distances are below the detection threshold, points are not at origin,
        // and point B is above point A for both left and right hands,
        // and the distance between point C is greater than the threshold
        if ((distanceA < detectionDistance && distanceB < detectionDistance) &&
            pointsNotAtOrigin &&
            heightDifferenceLeft > heightThreshold && heightDifferenceRight > heightThreshold &&
            distanceC > cDistanceThreshold)
        {

            // If the gesture is not already in progress
            if (!isGestureInProgress)
            {
                // Set the gesture as in progress
                isGestureInProgress = true;

            if(spawnObject){
            // Create a new object at the specified spawn location
            Vector3 spawnPosition = (leftHandPointB.position + rightHandPointB.position) / 2 + spawnOffset;
            GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            }
            supportCount++;

            // Update the output string
            output = $"Support[{supportCount}]";

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
