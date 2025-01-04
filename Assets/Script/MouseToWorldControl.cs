using System;
using UnityEngine;

public class MouseToWorldControl : MonoBehaviour
{
    public Transform control;
    public float depth = 2;

    public Camera camera;
    public float smoothTime = 0.3f;
    public float maxSpeed = 10;
    public Vector3 velocity;
    private Vector3 targetPositionCache;
    private void OnEnable()
    {
        targetPositionCache = control.position;
    }

    private void OnDisable()
    {
        control.position = targetPositionCache;
    }

    private void Update()
    {
        if(control == null)
            return;
        var mousePos = Input.mousePosition;
        var position = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth));
        control.position = Vector3.SmoothDamp(control.position, position, ref velocity, smoothTime, maxSpeed);
    }
}
