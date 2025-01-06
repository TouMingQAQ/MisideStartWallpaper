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
    public Vector4 offset = Vector4.one;
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
        var distance = mousePos - center;
        var mulX = distance.x < 0 ? offset.x : offset.z;
        var mulY = distance.y < 0 ? offset.y : offset.w;
        mousePos = new Vector2(mulX, mulY) * distance;
        var position = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth));
        control.position = Vector3.SmoothDamp(control.position, position, ref velocity, smoothTime, maxSpeed);
    }
}
