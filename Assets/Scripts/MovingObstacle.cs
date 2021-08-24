using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float distance;

    private int direction = 1;
    private Vector3 initialPos;
    private Vector3 targetPos { get { return initialPos + direction * distance * Vector3.right; } }

    private void Start()
    {
        initialPos = transform.position;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position += speed * Time.deltaTime * direction * Vector3.right;
        if (direction > 0)
        {
            if (transform.position.x >= targetPos.x)
            {
                direction = -1;
            }
        }
        else if (direction < 0)
        {
            if (transform.position.x <= targetPos.x)
            {
                direction =  1;
            }
        }
    }
}
