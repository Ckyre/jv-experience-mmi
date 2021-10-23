using UnityEngine;
using UnityEditor;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float downPositionY = 0.0f;
    [SerializeField] private float upPositionY = 5.0f;
    [SerializeField] private bool startAtDown = true;
    [SerializeField] private float upSpeed = 5.0f;
    [SerializeField] private float downSpeed = 5.0f;

    private bool isMoving = false;
    private bool isFreeze = false;
    private bool targetIsUp = false;
    private Vector3 targetPos, startPos;
    private float tAddition = 0;

    private void Start()
    {
        if (startAtDown)
            transform.position = new Vector3(transform.position.x, downPositionY, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, upPositionY, transform.position.z);

        targetPos = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            isMoving = false;
            targetPos = transform.position;
        }

        if (isMoving && !isFreeze)
        {
            if (targetIsUp)
            {
                transform.position = Vector3.Lerp(startPos, targetPos, tAddition + (Time.deltaTime * upSpeed));
                tAddition += Time.deltaTime * upSpeed;
            }
            else
            {
                transform.position = Vector3.Lerp(startPos, targetPos, tAddition + (Time.deltaTime * downSpeed));
                tAddition += Time.deltaTime * downSpeed;
            }
        }
    }

    public void GoUp()
    {
        if (!isMoving)
        {
            startPos = transform.position;
            tAddition = 0;
            targetPos = new Vector3(transform.position.x, upPositionY, transform.position.z);
            isMoving = true;
            targetIsUp = true;
        }
    }

    public void GoDown()
    {
        if (!isMoving)
        {
            startPos = transform.position;
            tAddition = 0;
            targetPos = new Vector3(transform.position.x, downPositionY, transform.position.z);
            isMoving = true;
            targetIsUp = false;
        }
    }

    public void Toggle()
    {
        if (!isMoving)
        {
            targetIsUp = !targetIsUp;
            startPos = transform.position;
            tAddition = 0;
            targetPos = new Vector3(transform.position.x, (targetIsUp ? upPositionY : downPositionY), transform.position.z);
            isMoving = true;
        }
    }

    public void Freeze (bool freeze)
    {
        isFreeze = freeze;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.transform == PlayerController.instance.transform)
        {
            PlayerController.instance.transform.parent = transform;
            PlayerController.instance.SetIsParentedToElevator(true);
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.transform == PlayerController.instance.transform)
        {
            PlayerController.instance.transform.parent = null;
            PlayerController.instance.SetIsParentedToElevator(false);
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!EditorApplication.isPlaying)
        {
            if (startAtDown)
                transform.position = new Vector3(transform.position.x, downPositionY, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x, upPositionY, transform.position.z);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x, downPositionY, transform.position.z), new Vector3(transform.position.x, upPositionY, transform.position.z));
    }
    #endif

}
