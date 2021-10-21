using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTP : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 positionOffset, lookOffset;
    [Space]
    [SerializeField] private float sensivity = 12.0f;
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

        transform.eulerAngles += new Vector3(mouseInputs.y * sensivity * Time.deltaTime, mouseInputs.x * sensivity * Time.deltaTime);
    }
}
