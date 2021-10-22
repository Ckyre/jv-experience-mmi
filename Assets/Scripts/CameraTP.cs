using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTP : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float maxAngle = 50.0f;
    [SerializeField] private float cameraHeight = 2.0f;
    [SerializeField] private float zoomSpeed = 12.0f;
    [SerializeField] private LayerMask collisionMask;
    [Space]
    [SerializeField, Range(0, 15)] private float sensivity = 12.0f;
    [SerializeField] private bool invertY = false;

    private Camera cameraChild;

    private float zoom = 5.0f;
    private float targetZoom; // for lerp zoom

    void Awake()
    {
        cameraChild = GetComponentInChildren<Camera>();
        cameraChild.transform.localPosition = new Vector3(0, cameraHeight, -Mathf.Abs(zoom));
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Linecast(target.transform.position, transform.position, out hit, collisionMask))
        {
            Debug.Log("Clamp zoom");
            zoom = Vector3.Distance(transform.position, hit.point);
        }
        else
        {
            zoom = Mathf.Lerp(zoom, targetZoom, Time.deltaTime * zoomSpeed);
        }

        // Look at player
        transform.position = target.position;
        cameraChild.transform.LookAt(target);
        cameraChild.transform.localPosition = new Vector3(0, cameraHeight, -Mathf.Abs(zoom));

        // Rotate by mouse
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

    public void SetZoom (float value)
    {
        targetZoom = value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(target.transform.position, transform.position);
    }
}
