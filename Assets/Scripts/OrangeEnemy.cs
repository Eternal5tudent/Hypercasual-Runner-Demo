using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeEnemy : Enemy
{
    [SerializeField] Transform groundCheckRayOrigin;
    [SerializeField] float groundCheckRayLength;
    [SerializeField] float jumpForce;

    protected override void Move()
    {
        base.Move();
        if (!IsBlockAhead())
        {
            if (IsGrounded())
            {
                Jump();
            }
        }

    }

    private bool IsBlockAhead()
    {
        return Physics.Raycast(groundCheckRayOrigin.position, groundCheckRayLength * Vector3.down, whatIsGround);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, groundCheckRayLength * Vector3.down, whatIsGround);
    }
   
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(groundCheckRayOrigin.position, groundCheckRayLength * Vector3.down);
        Gizmos.DrawRay(transform.position, groundCheckRayLength * Vector3.down);
    }
}
