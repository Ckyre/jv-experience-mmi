using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Singleton
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

    // Logic
    private PlayerState currentState;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetState(new WalkPlayerState());
    }

    private void Update()
    {
        if (currentState != null)
            currentState.OnUpdate();
    }

    private void SetState (PlayerState nextState)
    {
        if (currentState != null)
            currentState.onDettach();

        currentState = nextState;
        currentState.OnAttach();
    }
}
