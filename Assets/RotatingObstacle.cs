using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField] float rotationSpeed;

    private void FixedUpdate()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime * 100, 0);
    }
}
