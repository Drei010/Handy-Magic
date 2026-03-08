using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gesture_A : MonoBehaviour, IGesture
{
    public GameObject objectPrefab; // Prefab for the 3D object
    public Transform leftHandPointA; // Transform of point A on the left hand
    public Transform rightHandPointA; // Transform of point A on the right hand
    public Transform leftHandPointB; // Transform of point B on the left hand
    public Transform rightHandPointB; // Transform of point B on the right hand
    public float detectionDistance = 0.05f; // Distance threshold for detection
    public Vector3 spawnOffset = new Vector3(0f, 0.5f, 0f); // Editable spawn offset

    private bool isGestureInProgress = false;
    private int activateCount = 0;
    public string output = "";
    public bool spawnObject = false;
    public string GetOutput()
    {
        return isGestureInProgress ? $"Activate[{activateCount}]" : "";
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
            // Lock the process
            if (!isGestureInProgress)
            {
                isGestureInProgress = true;

                // Create a new object at the specified spawn location
                if (spawnObject)
                {
                    Vector3 spawnPosition = (leftHandPointB.position + rightHandPointB.position) / 2 + spawnOffset;
                    GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
                }
                    activateCount++;
                    // Update the output string
                    output = $"Activate[{activateCount}]";

                // Delay for 3 seconds and then reset the gesture flag
                StartCoroutine(ResetGestureFlagAfterDelay(3.0f));
            }
        }
        else
        {
            // Reset the gesture flag immediately if the gesture is not in progress
            if (isGestureInProgress)
            {
                ResetGestureFlag();
            }
        }
    }

    private IEnumerator ResetGestureFlagAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetGestureFlag();
    }

    void ResetGestureFlag()
    {
        // Reset the gesture flag
        isGestureInProgress = false;
    }
}
