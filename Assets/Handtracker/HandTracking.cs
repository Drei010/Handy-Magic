    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class HandTracking : MonoBehaviour
    {
        public UDPReceive udpReceive;
        public GameObject[] leftHandPoints; // Array for left hand landmarks
        public GameObject[] rightHandPoints; // Array for right hand landmarks
        void Start()
        {

        }

        void Update()
        {
            string data = udpReceive.data;

            if (!string.IsNullOrEmpty(data) && data.Length >= 2)
            {
                data = data.Remove(0, 1);
                data = data.Remove(data.Length - 1, 1);

                string[] points = data.Split(',');

                if (points.Length >= 126) // Make sure you have enough elements in the points array for both hands
                {
                    for (int i = 0; i < 21; i++)
                    {
                        // Left Hand Landmarks
                        float leftX = float.Parse(points[i * 3]) / 100;
                        float leftY = float.Parse(points[i * 3 + 1]) / 100;
                        float leftZ = float.Parse(points[i * 3 + 2]) / 100;

                        //leftHandPoints[i].transform.localPosition = new Vector3((leftX + leftZ*(-1))/2, leftY, (leftZ + leftX*(-1))/2);
                      // leftHandPoints[i].transform.localPosition = new Vector3(leftX + (leftZ*percentage), leftY, leftZ + (leftX*percentage));
                        leftHandPoints[i].transform.localPosition = new Vector3(leftX, leftY, leftZ);


                        // Right Hand Landmarks
                        float rightX = float.Parse(points[(i + 21) * 3]) / 100;
                        float rightY = float.Parse(points[(i + 21) * 3 + 1]) / 100;
                        float rightZ = float.Parse(points[(i + 21) * 3 + 2]) / 100;

                       // rightHandPoints[i].transform.localPosition = new Vector3(rightX+ (rightZ*percentage), rightY,rightZ +  (rightX*percentage));
                        rightHandPoints[i].transform.localPosition = new Vector3(rightX, rightY, rightZ);

                    }
                }
            }
        }
    }

