using System;
using UnityEngine;
#if !UNITY_ANDROID
using UnityEngine.InputSystem;
#endif
using VInspector;

public class MouseToWorldControl : MonoBehaviour
{
    public Transform control;
    public float depth = 2;

    public Camera camera;
    public float smoothTime = 0.3f;
    public float maxSpeed = 10;
    [InspectorName("Y轴跟随限制")]
    public Vector2 limitY = new Vector2(0.8f,0.6f);
    [ReadOnly]
    public Vector3 velocity;
    public Vector4 offset = Vector4.one;
    private Vector3 targetPositionCache;
    private Vector3 targetPosition;
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
        UpdateTargetPosition();
        control.position = Vector3.SmoothDamp(control.position, targetPosition, ref velocity, smoothTime, maxSpeed);
    }
    void UpdateTargetPosition()
    {
        if(control == null || !MiSideStart.config.LookAtMouse)
            return;
        var center = Screen.safeArea.center;
#if !UNITY_ANDROID
        var mousePos = Mouse.current.position.ReadValue();
#else
        var mousePos = (Vector2)Input.mousePosition;
#endif
#if !UNITY_ANDROID
        if (!Mouse.current.leftButton.isPressed)
        {
            targetPosition = targetPositionCache;
            return;
        }
#else
        if (Input.touchCount <= 0)
        {
            targetPosition = targetPositionCache;
            return;
        }
#endif
        var distance = mousePos - center;
      
        var mulX = distance.x < 0 ? offset.x : offset.z;
        var mulY = distance.y < 0 ? offset.y : offset.w;
        var screenHeight = Screen.safeArea.height / 2;

        if(distance.y > screenHeight * limitY.x)
        {
            distance.y = screenHeight * limitY.x;
        }else if(distance.y < -screenHeight * limitY.y)
        {
            distance.y = -screenHeight * limitY.y;
        }
        mousePos = new Vector2(mulX, mulY) * distance;
        targetPosition = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth));
    }
}
