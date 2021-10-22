using System.Collections;
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

    private PlayerState currentState;
    private Rigidbody rb;
    private Animator animator;
    private bool isHidden = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
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
        }
    }

    private void FixedUpdate()
    {
        if (currentState != null)
            currentState.OnFixedUpdate();
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

    #region Getter & setter
    public CameraTP GetAttachedCamera()
    {
        return attachedCamera;
    }

    public void SetAttachedCamera (CameraTP newCamera)
    {
        attachedCamera = newCamera;
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

    public void SetIsHidden (bool value)
    {
        isHidden = value;
    }

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
    #endregion
}
