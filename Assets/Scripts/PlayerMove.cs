using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float rotateSpeed = 10.0f;
    [Space]
    [SerializeField] private CameraTP attachedCamera;

    private Rigidbody rb;
    private Vector2 moveInputs;
    private Vector3 moveDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveInputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Rotation toward camera
        if (moveInputs != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, attachedCamera.transform.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
        }

        // Movement
        Vector3 right = moveInputs.x * attachedCamera.transform.right;
        Vector3 forward = moveInputs.y * attachedCamera.transform.forward;
        moveDirection = (right + forward).normalized;
    }

    void FixedUpdate()
    {
        // Apply move force
        Vector3 moveStep = new Vector3(moveDirection.x * moveSpeed * Time.fixedDeltaTime, 0, moveDirection.z * moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(transform.position + moveStep);
    }

}
