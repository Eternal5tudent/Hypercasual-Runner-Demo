using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float turnSpeed;
    [Range(30, 80)]
    [SerializeField] private float maxRotation = 60f;

    private float dragX;
    private InputManager inputManager;
    private new Rigidbody rigidbody;
    private bool resetingRotation;
    private bool isAlive;

    public static Action onPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance;
        rigidbody = GetComponent<Rigidbody>();
        inputManager.onMouseUp += () => ResetRotation(true);
        inputManager.onMouseDown += () => ResetRotation(false);
        Enemy.onAttack += Die;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        dragX = inputManager.DragX;
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            HandleRotation();
            Vector3 newVelocity = new Vector3(transform.forward.x * movementSpeed, rigidbody.velocity.y, movementSpeed);
            rigidbody.velocity = newVelocity;
        }
    }

    private void HandleRotation()
    {
        float rotationY = transform.localEulerAngles.y;
        rotationY = (rotationY > 180) ? rotationY - 360 : rotationY;
        if (resetingRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), Time.fixedDeltaTime * turnSpeed * 2);
        }
        if (dragX > 0)
        {
            if (rotationY < maxRotation)
                transform.Rotate(0, dragX * turnSpeed * Time.fixedDeltaTime, 0);
        }
        else if (dragX < 0)
        {
            if (rotationY > -maxRotation)
                transform.Rotate(0, dragX * turnSpeed * Time.fixedDeltaTime, 0);
        }
    }

    private void ResetRotation(bool reset)
    {
        resetingRotation = reset;
    }

    private void Die()
    {
        if (isAlive) // execute only if the player hasn't died already
        {
            isAlive = false;
            rigidbody.velocity = Vector3.zero;
            onPlayerDeath?.Invoke();
        }
    }
}
