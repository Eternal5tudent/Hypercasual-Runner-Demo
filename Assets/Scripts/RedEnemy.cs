using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : Enemy
{
    [SerializeField] Transform frontGroundCheck;
    [SerializeField] Transform rightGroundCheck;
    [SerializeField] Transform leftGroundCheck;
    [SerializeField] float groundCheckRayLength;
    [SerializeField] float rotateSpeed = 80f;


    private Vector3 DecideMoveDirection()
    {
        bool canGoFront = Physics.Raycast(frontGroundCheck.position, Vector3.down * groundCheckRayLength, whatIsGround);
        bool canGoRight = Physics.Raycast(rightGroundCheck.position, Vector3.down * groundCheckRayLength, whatIsGround);
        bool canGoLeft = Physics.Raycast(leftGroundCheck.position, Vector3.down * groundCheckRayLength, whatIsGround);

        if (canGoFront)
        {
            return frontGroundCheck.position - transform.position;
        }

        else if (canGoRight)
        {
            return rightGroundCheck.position - transform.position;
        }

        else if (canGoLeft)
        {
            return leftGroundCheck.position - transform.position;
        }

        // if you can't detect ground anywhere, just go forward
        return transform.forward;
    }

    protected override void Move()
    {
        Vector3 moveDirection = DecideMoveDirection();
        float angle = Vector3.Angle(transform.forward, moveDirection);
        float angleDirection = moveDirection.x / Mathf.Abs(moveDirection.x);
        //todo: the following code is very hacky
        if (Mathf.Abs(angle) >= 20)
        {
            movementSpeed = 2;
        }
        else
        {
            movementSpeed = 8;
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, angleDirection * angle, 0), Time.fixedDeltaTime * rotateSpeed * 2);
        base.Move();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(leftGroundCheck.position, Vector3.down * groundCheckRayLength);
        Gizmos.DrawRay(rightGroundCheck.position, Vector3.down * groundCheckRayLength);
        Gizmos.DrawRay(frontGroundCheck.position, Vector3.down * groundCheckRayLength);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, 2 * groundCheckRayLength * DecideMoveDirection());
    }
}
