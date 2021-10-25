using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, Actor
{
    #region Singleton
    public static PlayerController instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void OnDisable()
    {
        instance = null;
    }

    private void OnDestroy()
    {
        instance = null;
    }
    #endregion

    #region Events
    public delegate void InteractAction();
    public event InteractAction OnInteract;

    public delegate void DieAction();
    public event DieAction OnDie;

    public delegate void MoveAction();
    public event MoveAction OnMove;

    public delegate void CrouchAction();
    public event CrouchAction OnCrouch;
    #endregion

    #region Logic
    public PlayerProperties properties;
    public PlayerInputMapping inputCodes;
    [Space]
    [SerializeField] private CameraTP attachedCamera;
    [SerializeField] private Transform bushMesh;

    private PlayerState currentState;
    private Rigidbody rb;
    private Animator animator;
    private InteractionPoint listeningInteraction;
    private bool isHidden = false;
    private bool isParentedToElevator = false;
    private LayerMask groundMask;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        groundMask = LayerMask.GetMask("Ground");

        ActiveBush(false);
        SetState(new WalkPlayerState());
    }

    private void Update()
    {
        if (currentState != null)
            currentState.OnUpdate();

        // Events invoke
        if (Input.GetKeyDown(inputCodes.interact))
        {
            if (OnInteract != null)
                OnInteract();

            if (listeningInteraction != null)
                listeningInteraction.OnPlayerInteract();
        }
    }

    private void FixedUpdate()
    {
        if (currentState != null)
            currentState.OnFixedUpdate();
    }

    private void LateUpdate()
    {
    }

    public void SetState (PlayerState nextState)
    {
        if (currentState != null)
            currentState.OnDettach();

        currentState = nextState;

        currentState.OnAttach(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnEnterTrigger(other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnExitTrigger(other);
    }
    #endregion

    public void Die()
    {
        if (OnDie != null)
            OnDie();

        SetState(new DeadPlayerState());
    }

    public bool IsGrounded()
    {
        if(Physics.Raycast(transform.position, -Vector3.up, properties.grondDistanceCheck, groundMask))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
            return true;
        }

        // Else apply gravity
        rb.AddForce((-Vector3.up * properties.gravityForce) * Time.deltaTime);
        return false;
    }

    #region Getter & setter
    // Setters
    public void SetAttachedCamera (CameraTP newCamera)
    {
        attachedCamera = newCamera;
    }

    // Getters
    public CameraTP GetAttachedCamera()
    {
        return attachedCamera;
    }

    public Rigidbody GetRigidbody()
    {
        return rb;
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    public bool GetIsHidden()
    {
        return isHidden;
    }

    // Events triggers
    public void OnMoveInvoke()
    {
        if (OnMove != null)
            OnMove();
    }

    public void OnCrouchInvoke()
    {
        if (OnCrouch != null)
            OnCrouch();
    }

    // Elevator
    public void SetIsParentedToElevator (bool value)
    {
        isParentedToElevator = value;
    }

    public bool GetIsParentedToElevator()
    {
        return isParentedToElevator;
    }

    // Tall grass
    public void SetIsHidden(bool value)
    {
        isHidden = value;
    }

    // Pickable bush
    public void ActiveBush (bool active)
    {
        bushMesh.gameObject.SetActive(active);
    }

    public bool IsBushActive()
    {
        return bushMesh.gameObject.activeSelf;
    }

    // Interactions callback list
    public void SetInteractionListener (InteractionPoint point)
    {
        listeningInteraction = point;
    }

    public void RemoveInteractionListener()
    {
        listeningInteraction = null;
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 targetPos = transform.position - new Vector3(0, properties.grondDistanceCheck, 0);
        Gizmos.DrawLine(transform.position, targetPos);
    }
}
