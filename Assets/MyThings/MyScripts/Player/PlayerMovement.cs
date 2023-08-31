using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private Vector3 playerVelocity;
    private PlayerInput playerInput;
    [SerializeField] private float moveSpeed;
    private bool isMoving;
    private Animator anim;

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //Handles movement using navmesh and the input system
        if (playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move.Normalize();
        move *= moveSpeed * Time.deltaTime;

        if (move.magnitude > 0.0f) // Check if the move vector's magnitude is greater than a small threshold
        {
            isMoving = true; // Player is moving
            agent.transform.LookAt(agent.transform.position + move, Vector3.up);
            anim.SetFloat("Move", 1.0f);
            agent.Move(move);
        }
        else
        {
            isMoving = false; // Player is not moving
            anim.SetFloat("Move", 0.0f);
        }
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}