using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTP : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float maxAngle;
    [SerializeField] private Vector3 positionOffset, lookOffset;
    [Space]
    [SerializeField, Range(0, 15)] private float sensivity = 12.0f;
    [SerializeField] private bool invertY = false;

    private Camera cameraChild;

    void Awake()
    {
        cameraChild = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        transform.position = target.position + positionOffset;
        cameraChild.transform.LookAt(target.position + lookOffset);

        Vector2 mouseInputs = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (invertY == false) mouseInputs.y *= -1;

        Vector3 finalRotation = transform.eulerAngles + new Vector3(mouseInputs.y * (sensivity * 20) * Time.deltaTime, mouseInputs.x * (sensivity * 20) * Time.deltaTime);

        // Clamp angle
        if (finalRotation.x > 360)
            finalRotation.x -= 360;
        else if (finalRotation.x < 0)
            finalRotation.x += 360;

        if (finalRotation.x > 0 && finalRotation.x < maxAngle)
            transform.eulerAngles = finalRotation;
        else if (finalRotation.x > (360 - maxAngle) && finalRotation.x < 360)
            transform.eulerAngles = finalRotation;

    }
}
