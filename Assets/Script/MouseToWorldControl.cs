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
    public Vector2 offset = Vector2.one;
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
        var center = Screen.safeArea.center;
        var mousePos = (Vector2)Input.mousePosition;
        mousePos = offset * (mousePos - center);
        var position = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth));
        control.position = Vector3.SmoothDamp(control.position, position, ref velocity, smoothTime, maxSpeed);
    }
}
