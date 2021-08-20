using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public float DragX { get; private set; } // delta-movement percentage of screen-width
    private Vector2 lastMousePos;
    private float screenWidth;

    public Action onMouseUp;
    public Action onMouseDown;

    private void Start()
    {
        screenWidth = Camera.main.pixelWidth;
    }

    private void LateUpdate()
    {
        Vector2 currentMousePos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0)) // Touch-Start
        {
            lastMousePos = currentMousePos;
            onMouseDown?.Invoke();
        }
        else if(Input.GetMouseButton(0)) // Touch-Drag
        {
            DragX = (currentMousePos - lastMousePos).x * 100 / screenWidth; 
        }
        else if (Input.GetMouseButtonUp(0)) // Touch-End
        {
            DragX = 0;
            onMouseUp?.Invoke();
        }
        lastMousePos = currentMousePos;

    }
}
