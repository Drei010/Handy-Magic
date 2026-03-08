using UnityEngine;
using System.Collections;

public class Gesture_X : MonoBehaviour, IGesture
{
    // Prefab for the 3D object
    public GameObject objectPrefab;

    // Transforms for points A, B, and C on the left and right hands
    public Transform leftHandPointA, rightHandPointA, leftHandPointB, rightHandPointB, leftHandPointC, rightHandPointC;

    // Distance threshold for points A and B detection
    public float detectionDistanceAB = 0.05f;

    // Distance threshold for point C detection
    public float detectionDistanceC = 0.1f;

    // Editable spawn offset
    public Vector3 spawnOffset = new Vector3(0f, 0.5f, 0f);

    // Flag to determine whether to spawn an object
    public bool spawnObject = false;

    // Output string
    public string output = "";

    // Flag to track if the gesture is in progress
    private bool isGestureInProgress = false;

    // Counter for offense occurrences
    private int offenseCount = 0;

    // Method to get the current output string
    public string GetOutput()
    {
        return isGestureInProgress ? $"Offense[{offenseCount}]" : "";
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between point A, B, and C of left and right hands
        float distanceA = Vector3.Distance(leftHandPointA.position, rightHandPointA.position);
        float distanceB = Vector3.Distance(leftHandPointB.position, rightHandPointB.position);
        float distanceC = Vector3.Distance(leftHandPointC.position, rightHandPointC.position);

        // Check if points are not exactly at (0, 0, 0)
        bool pointsNotAtOrigin = leftHandPointB.position != Vector3.zero && rightHandPointB.position != Vector3.zero;

        // Check if point A is above point C
        bool aAboveC = leftHandPointA.position.y > leftHandPointC.position.y && rightHandPointA.position.y > rightHandPointC.position.y;

        // If the distances are below the detection threshold and points are not at origin and point A is above point C
        if ((distanceA < detectionDistanceAB && distanceB < detectionDistanceAB && distanceC < detectionDistanceC) && pointsNotAtOrigin && aAboveC)
        {
            // If the gesture is not already in progress
            if (!isGestureInProgress)
            {
                // Set the gesture as in progress
                isGestureInProgress = true;

                // If spawnObject is true, create a new object at the specified spawn location
                if (spawnObject)
                {
                    Vector3 spawnPosition = (leftHandPointB.position + rightHandPointB.position + leftHandPointC.position + rightHandPointC.position) / 4 + spawnOffset;
                    GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
                    // Add any specific behavior or components to the new object here
                }

                // Increment the offense count
                offenseCount++;

                // Update the output string
                output = $"Offense[{offenseCount}]";

                // Start a coroutine to reset the gesture flag after a delay
                StartCoroutine(ResetGestureFlagAfterDelay(3.0f));
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
