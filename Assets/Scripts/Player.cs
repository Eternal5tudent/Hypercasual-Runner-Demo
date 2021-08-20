using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float turnSpeed;
    [Range(30, 80)]
    [SerializeField] private float maxRotation = 60f;

    private float dragX;
    private InputManager inputManager;
    private new Rigidbody rigidbody;
    private bool resetingRotation;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance;
        rigidbody = GetComponent<Rigidbody>();
        inputManager.onMouseUp += () => ResetRotation(true);
        inputManager.onMouseDown += () => ResetRotation(false);
    }

    // Update is called once per frame
    void Update()
    {
        dragX = inputManager.DragX;
        
    }

    private void FixedUpdate()
    {
        float rotationY = transform.localEulerAngles.y;
        rotationY = (rotationY > 180) ? rotationY - 360 : rotationY;
        if (resetingRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), Time.fixedDeltaTime * turnSpeed);
        }
        if (dragX > 0)
        {
            if(rotationY < maxRotation)
                transform.Rotate(0, dragX * turnSpeed * Time.fixedDeltaTime, 0);
        }
        else if (dragX < 0)
        {
            if (rotationY > -maxRotation)
                transform.Rotate(0, dragX * turnSpeed * Time.fixedDeltaTime, 0);
        }
        Vector3 newVelocity = transform.forward * movementSpeed + Vector3.up * rigidbody.velocity.y;
        rigidbody.velocity = newVelocity;
    }

    private void ResetRotation(bool reset)
    {
        resetingRotation = reset;
    }
}
