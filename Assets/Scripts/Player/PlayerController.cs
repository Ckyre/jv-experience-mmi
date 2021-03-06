using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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
    [Space]
    [SerializeField] private CameraTP attachedCamera;
    [SerializeField] private GameObject spineBone;
    [Space]
    [SerializeField] private GameObject bushShowcasePrefab;
    [SerializeField] private GameObject toy1, toy2, toy3;
    [Space]
    [SerializeField] private AudioSource footSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource, uiSource;
    [SerializeField] private AudioClip landingClip;

    private PlayerState currentState;
    private Rigidbody rb;
    private Animator animator;
    private PlayerAnimatorEvents animatorEvents;
    private Interactable listeningInteraction;
    private CapsuleCollider collider;
    private LayerMask groundMask;

    private bool isGrounded = false;
    private float lastGroundTime;
    private bool isHidden = false;
    private bool isInABush = false;
    private float pickBushTime;
    private float groundDistance;
    private bool isParentedToElevator = false;
    private bool walkInWater = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        animator = GetComponentInChildren<Animator>();
        animatorEvents = animator.GetComponent<PlayerAnimatorEvents>();
        groundMask = LayerMask.GetMask("Ground", "Default");
        collider = GetComponent<CapsuleCollider>();

        // Hight performances mode
        if (GameManager.gameData.highPerformance)
        {
            attachedCamera.GetComponentInChildren<PostProcessLayer>().enabled = false;

            foreach (Light light in FindObjectsOfType<Light>())
            {
                if (light.type == LightType.Point)
                    light.enabled = false;
            }
        }

        ActiveBush(false);
        SetState(new WalkPlayerState());
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position + new Vector3(0, -Mathf.Abs(properties.feetPos.y), 0), properties.groundCheckRadius, groundMask);
        
        // Ground distance
        RaycastHit groundHit;
        if(Physics.Raycast(transform.position + new Vector3(0, -Mathf.Abs(properties.feetPos.y), 0), -Vector3.up, out groundHit, Mathf.Infinity, groundMask))
            groundDistance = Vector3.Distance(transform.position, groundHit.point);
        else
            groundDistance = -1;


        if (!isGrounded)
        {
            // Gravity
            rb.AddForce((-Vector3.up * properties.gravityForce) * Time.deltaTime);
        }
        else
        {
            // Landing sound fx
            if ((Time.time - lastGroundTime) > 0.4f)
            {
                footSource.PlayOneShot(landingClip);
            }

            lastGroundTime = Time.time;
        }

        // Update current state
        if (currentState != null)
            currentState.OnUpdate();

        // Events invoke
        if (Input.GetKeyDown(GameManager.instance.inputCodes.interact))
        {
            if (OnInteract != null)
                OnInteract();

            if (listeningInteraction != null)
                listeningInteraction.OnPlayerTryInteract();

            if (IsBushActive() && (Time.time - pickBushTime) > 2.0f)
            {
                ActiveBush(false);
            }
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

    private void OnTriggerStay(Collider other)
    {
        currentState.OnStayTrigger(other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnExitTrigger(other);
    }
    #endregion

    #region Getter & setter
    // Setters
    public void SetAttachedCamera (CameraTP newCamera)
    {
        attachedCamera = newCamera;
    }

    // Getters
    public CapsuleCollider GetCollider()
    {
        return collider;
    }

    public float GetGroundDistance()
    {
        return groundDistance;
    }

    public AudioSource GetUIAudioSource()
    {
        return uiSource;
    }

    public PlayerAnimatorEvents GetAnimatorEvents()
    {
        return animatorEvents;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

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

    // Tall grass
    public void SetIsHidden(bool value)
    {
        isHidden = value;
    }

    public bool GetIsHidden()
    {
        return isHidden;
    }

    // Elevator
    public void SetIsParentedToElevator (bool value)
    {
        isParentedToElevator = value;
        if(value)
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        else
            rb.constraints = RigidbodyConstraints.FreezeRotation;

    }

    public bool GetIsParentedToElevator()
    {
        return isParentedToElevator;
    }

    // Interactions callback list
    public void SetInteractionListener (Interactable point)
    {
        listeningInteraction = point;
    }

    public void RemoveInteractionListener()
    {
        listeningInteraction = null;
    }
    #endregion

    // Toys
    public void PickToy (PickableToyInteraction.ToyType type, bool value = true)
    {
        switch (type)
        {
            case PickableToyInteraction.ToyType.Bear:
                toy1.SetActive(value);
                break;
            case PickableToyInteraction.ToyType.Rabbit:
                toy2.SetActive(value);
                break;
            case PickableToyInteraction.ToyType.Frog:
                toy3.SetActive(value);
                break;
        }
    }

    public bool GetToy (PickableToyInteraction.ToyType type)
    {
        switch (type)
        {
            case PickableToyInteraction.ToyType.Bear:
                return toy1.activeSelf;
            case PickableToyInteraction.ToyType.Rabbit:
                return toy2.activeSelf;
            case PickableToyInteraction.ToyType.Frog:
                return toy3.activeSelf;
        }

        return false;
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

    // Pickable bush
    public void ActiveBush (bool active)
    {
        if (active)
        {
            if (!isInABush)
            {
                isInABush = true;
                pickBushTime = Time.time;
                Instantiate(bushShowcasePrefab, spineBone.transform);
            }
        }
        else
        {
            if (isInABush)
            {
                isInABush = false;

                Rigidbody bushInstance = spineBone.GetComponentInChildren<Rigidbody>();
                bushInstance.constraints = RigidbodyConstraints.None;
                bushInstance.transform.parent = null;
                bushInstance.AddForce(attachedCamera.transform.forward * 1.8f, ForceMode.Impulse);
                bushInstance.GetComponent<TimeoutDestroy>().StartDestroying();
            }
        }
    }

    public bool IsBushActive()
    {
        return isInABush;
    }

    public void SetIsInWater (bool value)
    {
        walkInWater = value;
        GetComponentInChildren<PlayerAnimatorEvents>().SetIsInWater(value);
    }

    // Sounds
    public void PlaySFX (AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    private string currentMusicID = "";
    public void PlayMusic (AudioClip music, string musicID)
    {
        if(musicID != currentMusicID)
        {
            currentMusicID = musicID;
            StopCoroutine("ChangeMusic");
            StartCoroutine(ChangeMusic(music));
        }
    }

    private IEnumerator ChangeMusic (AudioClip music)
    {
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= Time.deltaTime * 0.05f;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        musicSource.volume = 0f;

        musicSource.Stop();
        musicSource.PlayOneShot(music);

        while (musicSource.volume < (startVolume - 0.05f))
        {
            musicSource.volume += Time.deltaTime * 0.05f;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        musicSource.volume = startVolume;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void Die()
    {
        if (currentState is DeadPlayerState)
            return;

        if (OnDie != null)
            OnDie();

        SetState(new DeadPlayerState());
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 targetPos = transform.position - new Vector3(0, Mathf.Abs(properties.feetPos.y), 0);
        Gizmos.DrawLine(transform.position, targetPos);

        Vector3 feetPos = (transform.right * properties.feetPos.x) + new Vector3(0, properties.feetPos.y, 0);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, -Mathf.Abs(properties.feetPos.y), 0), properties.groundCheckRadius);
    }
    #endif
}
