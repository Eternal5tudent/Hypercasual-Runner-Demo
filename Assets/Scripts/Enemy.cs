using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float movementSpeed = 8f;
    [SerializeField] protected LayerMask whatIsGround;

    protected Rigidbody rb;
    protected NavMeshAgent nav;
    protected bool isControlledByNav;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
    }

    protected virtual void Start()
    {
        if (!isControlledByNav)
        {
            EnableNavAgent(false);
        }
    }

    private void Update()
    {
        if (isControlledByNav)
        {
             // if enemy has reached its destination
            if (nav.pathStatus == NavMeshPathStatus.PathComplete && nav.remainingDistance == 0)
            {
                EnableNavAgent(false);
            }
        }

    }

    protected virtual void FixedUpdate()
    {
        if (!isControlledByNav)
            Move();
    }

    protected virtual void Move() // Logic used for the enemy movement goes here
    {
        rb.velocity = transform.forward * movementSpeed + Vector3.up * rb.velocity.y;
    }

    private void EnableNavAgent(bool enabled)
    {
        if (enabled)
        {
            nav.enabled = true;
            nav.speed = movementSpeed;
            isControlledByNav = true;
            rb.isKinematic = true;
        }
        else
        {
            nav.enabled = false;
            isControlledByNav = false;
            rb.isKinematic = false;
        }
    }

    // Area inside of which the enemy can spawn 
    public void SetDestinationArea(Vector3 destination)
    {
        EnableNavAgent(true);
        nav.SetDestination(destination);
    }
}
