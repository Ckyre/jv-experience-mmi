using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastController : MonoBehaviour, Actor
{
    [SerializeField] private float commonHeight = 8.0f;
    [Space]
    [SerializeField] private Transform pointsParent;
    [SerializeField] private Transform eye;
    [SerializeField] private Transform detectionTrigger;

    private Light eyeLight;
    private List<InterestPointBeast> interestPoints;
    
    private bool patternRunning = false;
    private LayerMask groundMask;

    private void Awake()
    {
        eyeLight = eye.GetComponentInChildren<Light>();
        interestPoints = new List<InterestPointBeast>(pointsParent.GetComponentsInChildren<InterestPointBeast>());
    }

    private void Start()
    {
        eyeLight.transform.localPosition = Vector3.zero;
        groundMask = LayerMask.GetMask("Ground");
        StopPattern();
    }

    public void KillPlayer()
    {
        if (!PlayerController.instance.GetIsHidden())
        {
            Debug.Log("Player died");
            PlayerController.instance.Die();
        }
    }

    #region Moving pattern
    public void StartPattern()
    {
        eye.gameObject.SetActive(true);
        StartCoroutine(Pattern());
    }

    public void StopPattern()
    {
        patternRunning = false;
        eye.gameObject.SetActive(false);
        StopCoroutine(Pattern());
    }

    private IEnumerator Pattern()
    {
        patternRunning = true;

        while (patternRunning)
        {
            {
                Vector3 targetPos = interestPoints[0].transform.position;
                targetPos.y = commonHeight;
                eye.transform.position = targetPos;

                SetLightSize(interestPoints[0].zoneSize);
            }

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
                    Vector3 targetPos = interestPoints[nextPoint].transform.position;
                    targetPos.y = commonHeight;
                    eye.transform.position = targetPos;

                    SetLightSize(interestPoints[nextPoint].zoneSize);
                }
                else
                {
                    // Smooth transition
                    int iterationsCount = Mathf.RoundToInt(interestPoints[i].transitionTime / Time.fixedDeltaTime) + 1;

                    Vector3 startPos = interestPoints[i].transform.position;
                    startPos.y = commonHeight;
                    Vector3 targetPos = interestPoints[nextPoint].transform.position;
                    targetPos.y = commonHeight;

                    for (int y = 0; y < iterationsCount; y++)
                    {
                        eye.transform.position = Vector3.Lerp(startPos, targetPos, (float)y / iterationsCount);

                        float lerpSize = Mathf.Lerp(interestPoints[i].zoneSize, interestPoints[nextPoint].zoneSize, (float)y / iterationsCount);
                        SetLightSize(lerpSize);

                        yield return new WaitForSeconds(Time.fixedDeltaTime);
                    }
                }

            }
        }
    }

    private void SetLightSize (float size)
    {
        RaycastHit hit;
        if(Physics.Raycast(eye.transform.position, -eye.transform.up, out hit, Mathf.Infinity, groundMask))
        {
            // Looking for spot angle
            // opposite = size / 2
            // adjacent = groundDistance
            // tan(x) = opposite / adjacent
            // spotAngle = arctan(x)

            // Get ground distance
            float groundDistance = Vector3.Distance(eye.transform.position, hit.point);
            eyeLight.range = groundDistance + 5.0f;

            // Get spot angle
            float tangent = (size / 2) / groundDistance;
            float angleRad = Mathf.Atan(tangent);
            float angleDeg = (angleRad * 2) * (180 / Mathf.PI);
            eyeLight.spotAngle = angleDeg;

            // Adapt detection trigger
            detectionTrigger.localScale = Vector3.one * (size * 0.8f);
            detectionTrigger.localPosition = new Vector3(0, -groundDistance, 0);
        }
    }
    #endregion

    public void Die() { }

    private void OnDrawGizmos()
    {
        interestPoints = new List<InterestPointBeast>(pointsParent.GetComponentsInChildren<InterestPointBeast>());

        Gizmos.color = Color.red;
        for(int i = 0; i < interestPoints.Count; i++)
        {
            if(i < interestPoints.Count - 1)
            {
                Vector3 startPos = interestPoints[i].transform.position;
                startPos.y = commonHeight;
                Vector3 targetPos = interestPoints[i + 1].transform.position;
                targetPos.y = commonHeight;

                Gizmos.DrawLine(startPos, targetPos);
            }
            else
            {
                Vector3 startPos = interestPoints[i].transform.position;
                startPos.y = commonHeight;
                Vector3 targetPos = interestPoints[0].transform.position;
                targetPos.y = commonHeight;

                Gizmos.DrawLine(startPos, targetPos);
            }
        }
    }
}
