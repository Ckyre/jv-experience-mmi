using UnityEngine;

public class CameraTP : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 lookOffset;
    [SerializeField] private float maxYAngle = 50.0f;
    [SerializeField] private float cameraHeight = 2.0f;
    [SerializeField] private float zoomSpeed = 12.0f;
    [SerializeField] private LayerMask collisionMask;
    [Space]
    [SerializeField, Range(0, 15)] private float sensivity = 12.0f;
    [SerializeField] private bool invertY = false;

    private Camera cameraChild;

    private float zoom = 5.0f;
    private float targetZoom; // for lerp zoom
    private bool isLocked = false;

    private void Awake()
    {
        cameraChild = GetComponentInChildren<Camera>();
        cameraChild.transform.localPosition = new Vector3(0, cameraHeight, -Mathf.Abs(zoom));
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        zoom = Mathf.Lerp(zoom, targetZoom, Time.deltaTime * zoomSpeed);

        // Look at player
        transform.position = target.position;
        Vector3 lOffset = (target.forward * lookOffset.x) + (Vector3.up * lookOffset.y);
        cameraChild.transform.LookAt(target.position + lOffset);

        // Prevent going into meshes
        Vector3 cameraDirection = (target.transform.position - cameraChild.transform.position).normalized;
        Vector3 collisionCheckPoint = target.transform.position - (cameraDirection * targetZoom) * 1.3f;
        RaycastHit hit;
        if (Physics.Linecast(target.transform.position, collisionCheckPoint, out hit, collisionMask))
        {
            if (Vector3.Distance(target.transform.position, cameraChild.transform.position) > 1f)
                cameraChild.transform.position = Vector3.Lerp(cameraChild.transform.position, hit.point + (cameraDirection * 1.5f), 10f * Time.deltaTime);
        }
        else cameraChild.transform.localPosition = new Vector3(0, cameraHeight, -Mathf.Abs(zoom));

        if (!isLocked)
        {
            // Rotate by mouse
            Vector2 mouseInputs = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            if (invertY == false) mouseInputs.y *= -1;

            Vector3 finalRotation = transform.eulerAngles + new Vector3(mouseInputs.y * (sensivity * 20) * Time.deltaTime, mouseInputs.x * (sensivity * 20) * Time.deltaTime);

            // Clamp angle
            if (finalRotation.x > 360)
                finalRotation.x -= 360;
            else if (finalRotation.x < 0)
                finalRotation.x += 360;

            if (finalRotation.x > 0 && finalRotation.x < maxYAngle)
                transform.eulerAngles = finalRotation;
            else if (finalRotation.x > (360 - maxYAngle) && finalRotation.x < 360)
                transform.eulerAngles = finalRotation;

            #if UNITY_EDITOR
            // Unlock curosor
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }
            #endif
        }
    }

    public void SetZoom (float value)
    {
        targetZoom = value;
    }

    public void IsLock(bool value)
    {
        isLocked = value;
        Cursor.visible = value;

        #if UNITY_EDITOR
        Cursor.lockState = CursorLockMode.None;
        #endif
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (cameraChild)
        {
            Vector3 cameraDirection = (target.transform.position - cameraChild.transform.position).normalized;
            Vector3 collisionCheckPoint = target.transform.position - (cameraDirection * targetZoom) * 1.3f;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(target.transform.position, collisionCheckPoint);
        }
    }
    #endif
}
