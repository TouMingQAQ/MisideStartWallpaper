using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;
using VInspector;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class MouseToWorldControl : MonoBehaviour
{
    public Transform control;
    public float depth = 2;

    public Camera worldCamera; // 更改：重命名以避免与基类成员冲突
    public float smoothTime = 0.3f;
    public float maxSpeed = 10;
    [InspectorName("Y轴跟随限制")]
    public Vector2 limitY = new Vector2(0.8f,0.6f);
    [ReadOnly]
    public Vector3 velocity;
    public Vector4 offset = Vector4.one;
    private Vector3 targetPositionCache;
    private Vector3 targetPosition;

    private void Awake()
    {
        #if UNITY_ANDROID 
        InputSystem.EnableDevice(Gyroscope.current);//启用陀螺仪
        #endif
    }

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
        if(control == null || MiSideStart.config.LookAtState == LookAtState.None)
            return;
        var center = Screen.safeArea.center;
        var mousePos = Vector2.zero;
#if UNITY_ANDROID
        if (Application.isEditor)
        {
            mousePos = Mouse.current.position.ReadValue();
        }
        else
        {
            if (Gyroscope.current != null && Gyroscope.current.enabled)
            {
                // 获取角速度 (rad/s)
                Vector3 angularVelocity = Gyroscope.current.angularVelocity.ReadValue();
                
        
                Debug.Log($"角速度: {angularVelocity}");
            }
        }
 
#else
        mousePos = Mouse.current.position.ReadValue();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        if (Touchscreen.current.primaryTouch.isInProgress)
        {
            targetPosition = targetPositionCache;
            return;
        }
#else
        if (!Mouse.current.leftButton.isPressed && MiSideStart.config.LookAtState == LookAtState.OnlyPress)
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
        }
        else if(distance.y < -screenHeight * limitY.y)
        {
            distance.y = -screenHeight * limitY.y;
        }
        mousePos = new Vector2(mulX, mulY) * distance;
        targetPosition = worldCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth)); // 更改：使用新的变量名
    }
}