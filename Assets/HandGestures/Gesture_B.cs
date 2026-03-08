using UnityEngine;
using System.Collections;

public class Gesture_B : MonoBehaviour, IGesture
{
    public GameObject objectPrefab;
    public Transform leftHandPointA, rightHandPointA, leftHandPointB, rightHandPointB;
    public float detectionDistance = 0.05f;
    public Vector3 spawnOffset = new Vector3(0f, 0.5f, 0f);
    public bool spawnObject = false;
    public string output = "";
    private bool isGestureInProgress = false;
    private int launchCount = 0;

    public string GetOutput()
    {
        return isGestureInProgress ? $"Launch[{launchCount}]" : "";
    }

    void Update()
    {
        float distanceA = Vector3.Distance(leftHandPointA.position, rightHandPointA.position);
        float distanceB = Vector3.Distance(leftHandPointB.position, rightHandPointB.position);
        bool pointsNotAtOrigin = leftHandPointB.position != Vector3.zero && rightHandPointB.position != Vector3.zero;

        if ((distanceA < detectionDistance && distanceB < detectionDistance) && pointsNotAtOrigin)
        {
            if (!isGestureInProgress)
            {
                isGestureInProgress = true;

                if (spawnObject)
                {
                    Vector3 spawnPosition = (leftHandPointB.position + rightHandPointB.position) / 2 + spawnOffset;
                    GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
                    // Add specific behavior or components to the new object here
                }

                launchCount++;
                output = $"Launch[{launchCount}]";

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
        isGestureInProgress = false;
    }
}
