using UnityEngine;

[CreateAssetMenu(fileName = "Input mapping", menuName = "Scriptable objects/Input mapping")]
public class PlayerInputMapping : ScriptableObject
{
    public KeyCode interact = KeyCode.E;
    public KeyCode crouch = KeyCode.C;
    public KeyCode moveForward = KeyCode.W;
    public KeyCode moveBackward = KeyCode.S;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode active = KeyCode.Return;
    public KeyCode close = KeyCode.Escape;

    public Vector2 GetMoveAxis()
    {
        float x = 0.0f;
        if (Input.GetKey(moveRight))
            x = 1.0f;
        else if (Input.GetKey(moveLeft))
            x = -1.0f;

        float y = 0.0f;
        if (Input.GetKey(moveForward))
            y = 1.0f;
        else if (Input.GetKey(moveBackward))
            y = -1.0f;

        return new Vector2(x, y);
    }
}
