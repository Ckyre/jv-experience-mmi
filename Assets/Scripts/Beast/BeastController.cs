using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastController : MonoBehaviour
{
    [SerializeField] private Transform pointsParent;
    [SerializeField] private Transform eye;
    [SerializeField] private float commonHeight = 8.0f;

    private Light eyeLight;

    private List<InterestPointBeast> interestPoints;
    private bool patternRunning = false;

    private void Awake()
    {
        eyeLight = eye.GetComponentInChildren<Light>();
    }

    private void Start()
    {
        UpdatePoints();
        StartPattern();
    }

    private void UpdatePoints()
    {
        interestPoints = new List<InterestPointBeast>(pointsParent.GetComponentsInChildren<InterestPointBeast>());
    }

    public void StartPattern()
    {
        Debug.Log("Starting pattern!");
        StartCoroutine(Pattern());
    }

    public void StopPattern()
    {
        patternRunning = false;
        StopCoroutine(Pattern());
    }

    private IEnumerator Pattern()
    {
        patternRunning = true;

        while (patternRunning)
        {
            eye.transform.position = interestPoints[0].transform.position;
            eyeLight.spotAngle = interestPoints[0].zoneSize;

            for (int i = 0; i < interestPoints.Count; i++)
            {
                // Stay
                yield return new WaitForSeconds(interestPoints[i].stayTime);

                // Transition
                int nextPoint = i + 1;
                if (i == interestPoints.Count - 1) nextPoint = 0;

                if (interestPoints[i].transitionTime < 0.15f)
                {
                    // Instant transition
                    eyeLight.spotAngle = interestPoints[nextPoint].zoneSize;
                    eye.transform.position = interestPoints[nextPoint].transform.position;
                }
                else
                {
                    // Smooth transition
                    int iterationsCount = Mathf.RoundToInt(interestPoints[i].transitionTime / Time.deltaTime);

                    for(int y = 0; y < iterationsCount; y++)
                    {
                        Vector3 targetPosition = interestPoints[nextPoint].transform.position;

                        eye.transform.position = Vector3.Lerp(eye.transform.position, targetPosition, (float)y / iterationsCount);
                        eyeLight.spotAngle = Mathf.Lerp(eyeLight.spotAngle, interestPoints[nextPoint].zoneSize, (float)y / iterationsCount);
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                }

                
            }
        }
    }

    private void OnDrawGizmos()
    {
        UpdatePoints();

        Gizmos.color = Color.red;
        for(int i = 0; i < interestPoints.Count; i++)
        {
            if(i < interestPoints.Count - 1)
                Gizmos.DrawLine(interestPoints[i].transform.position, interestPoints[i + 1].transform.position);
            else
                Gizmos.DrawLine(interestPoints[i].transform.position, interestPoints[0].transform.position);
        }
    }
}
