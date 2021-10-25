using UnityEngine;

[CreateAssetMenu(fileName = "Player properties", menuName = "Scriptable objects/Player properties")]
public class PlayerProperties : ScriptableObject
{
    [Header("Movements")]
    public float walkSpeed = 10.0f;
    public float crouchWalkSpeed = 5.0f;
    public float rotateSpeed = 10.0f;
    public float gravityForce = 20.0f;
    public float grondDistanceCheck = 2.0f;
    [Header("Camera")]
    public float cameraZoom = 3.0f;
    public float crouchCameraZoom = 6.0f;
}
