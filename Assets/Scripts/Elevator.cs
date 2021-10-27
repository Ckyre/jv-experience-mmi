using UnityEngine;
using UnityEditor;

public class Elevator : Trigger
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
        {
            transform.position = new Vector3(transform.position.x, downPositionY, transform.position.z);
            targetIsUp = true;
        }
        else
            transform.position = new Vector3(transform.position.x, upPositionY, transform.position.z);

        targetPos = transform.position;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
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

        if (PlayerController.instance.GetIsParentedToElevator())
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > 60.0f)
            {
                PlayerController.instance.transform.parent = null;
                PlayerController.instance.SetIsParentedToElevator(false);
                Debug.Log(Vector3.Distance(transform.position, PlayerController.instance.transform.position));
            }
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

    protected override void OnPlayerEnter(Collider col)
    {
        base.OnPlayerEnter(col);

        PlayerController.instance.transform.parent = transform;
        PlayerController.instance.SetIsParentedToElevator(true);
    }

    protected override void OnPlayerExit(Collider col)
    {
        base.OnPlayerExit(col);

        PlayerController.instance.transform.parent = null;
        PlayerController.instance.SetIsParentedToElevator(false);
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
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x, downPositionY, transform.position.z), new Vector3(transform.position.x, upPositionY, transform.position.z));
    }
    #endif

}
