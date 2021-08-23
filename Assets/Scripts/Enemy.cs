using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float movementSpeed = 8f;
    [SerializeField] protected LayerMask whatIsGround;

    public static Action onAttack;

    protected Rigidbody rb;
    protected Animator animator;
    protected NavMeshAgent nav;
    protected bool isControlledByNav;
    protected bool hasAttacked;
    protected Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        if (!isControlledByNav)
        {
            EnableNavAgent(false);
        }
        player = Player.Instance.transform;
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

        if (!hasAttacked)
        {
            // if enemy is too close to the player, attack him 
            if (Vector3.Distance(transform.position, player.position) <= 2)
            {
                AttackPlayer();
            }
        }
    }

    protected virtual void FixedUpdate()
    {
        if (!isControlledByNav && !hasAttacked)
            Move();
    }

    // Logic used for the enemy movement goes here
    protected virtual void Move() 
    {
        rb.velocity = transform.forward * movementSpeed + Vector3.up * rb.velocity.y;
    }

    protected virtual void AttackPlayer()
    {
        onAttack?.Invoke();
        hasAttacked = true;
        rb.velocity = Vector3.zero;
        StartCoroutine(JumpOnPlayer_Cor(0.2f));
        animator.SetBool("dead", true);
    }

    private IEnumerator JumpOnPlayer_Cor(float duration)
    {
        float startTime = Time.time;
        float timePassed = 0;
        while (timePassed <= duration)
        {
            timePassed = Time.time - startTime;
            float operationCompletion = timePassed / duration;
            transform.position = Vector3.Lerp(transform.position, player.position + Vector3.up * 2, operationCompletion);
            yield return new WaitForFixedUpdate();
        }
    }

    #region Navigation
    private void EnableNavAgent(bool enabled)
    {
        if (enabled)
        {
            isControlledByNav = true;
            nav.enabled = true;
            nav.speed = movementSpeed;
            rb.isKinematic = true;
        }
        else
        {
            nav.enabled = false;
            isControlledByNav = false;
            rb.isKinematic = false;
            transform.rotation = Quaternion.identity;
        }
    }

    // Area inside of which the enemy can spawn 
    public void SetDestinationArea(Vector3 destination)
    {
        EnableNavAgent(true);
        nav.SetDestination(destination);
    }
    #endregion
}
