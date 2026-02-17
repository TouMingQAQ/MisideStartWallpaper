using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VInspector;

#if UNITY_ANDROID
using UnityEngine.InputSystem.Android;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;
#endif
public class MouseToWorldControl : MonoBehaviour
{
    public Transform control;
    public float depth = 2;

    public Camera worldCamera; // 更改：重命名以避免与基类成员冲突
    public float smoothTime = 0.3f;
    public float maxSpeed = 10;
    [ReadOnly]
    public Vector3 velocity;
    public Vector2 offsetX = Vector2.one;
    public Vector2 offsetY = Vector2.one;
    [SerializeField,ReadOnly]
    private Vector2 distance;
    private Vector3 targetPositionCache;
    private Vector3 targetPosition;
#if UNITY_ANDROID 
    private Vector2 gyroscopePos;
#endif
    private void Awake()
    {
        #if UNITY_ANDROID 
        if(!Application.isEditor)
            InputSystem.EnableDevice(Gyroscope.current);//启用陀螺仪
        gyroscopePos = new Vector2(Screen.width *0.5f, Screen.height *0.5f);
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
        if(control == null || MiSideStart.instance.config.value.LookAtState == LookAtState.None)
            return;
        var center = Screen.safeArea.center;
        var mousePos = new Vector2(Screen.width *0.5f, Screen.height *0.5f);
#if UNITY_ANDROID
        if (Application.isEditor)
        {
            mousePos = Mouse.current.position.ReadValue();
        }
        else
        {
            if (Gyroscope.current != null && Gyroscope.current.enabled)
            {
                var gyroscopeScale = MiSideStart.config.gyroscopeScale;
                var gyroscopeSafeAreaScale = MiSideStart.config.gyroscopeSafeAreaScale;
                // 获取角速度 (rad/s)
                Vector3 angularVelocity = Gyroscope.current.angularVelocity.ReadValue();
                Vector2 offset = new Vector2(-angularVelocity.y,angularVelocity.x);
                gyroscopePos += (offset*gyroscopeScale);
                var width = (Screen.width * 0.5f);
                var height = (Screen.height * 0.5f);
                var offsetWidth = width * gyroscopeSafeAreaScale.x;
                var offsetHeight = height * gyroscopeSafeAreaScale.y;
                var safeArea = new Vector4(-offsetWidth,offsetWidth+Screen.width,-offsetHeight, offsetHeight+Screen.height);
                
                
                gyroscopePos.x = Mathf.Clamp(gyroscopePos.x,safeArea.x,safeArea.y);
                gyroscopePos.y = Mathf.Clamp(gyroscopePos.y,safeArea.z,safeArea.w);
                mousePos = gyroscopePos;
            }
        }
 
#else
        mousePos = Mouse.current.position.ReadValue();
#endif

#if UNITY_ANDROID
        if(!Application.isEditor)
            targetPosition = targetPositionCache;
#else
        if (!Mouse.current.leftButton.isPressed && MiSideStart.instance.config.value.LookAtState == LookAtState.OnlyPress)
        {
            targetPosition = targetPositionCache;
            return;
        }
#endif
        distance = mousePos - center;
        //限制跟随距离为屏幕大小，超出屏幕大小的无效
        var limitX = Screen.width * 0.5f;
        var limitY = Screen.height * 0.5f;
        distance.x = Mathf.Clamp(distance.x, -limitX, limitX);
        distance.y = Mathf.Clamp(distance.y, -limitY, limitY);
        var mulX = distance.x < 0 ? offsetX.x : offsetX.y;
        var mulY = distance.y < 0 ? offsetY.x : offsetY.y;
        var screenHeight = Screen.safeArea.height / 2;
        
        mousePos = new Vector2(mulX, mulY) * distance;
        targetPosition = worldCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth)); // 更改：使用新的变量名
    }
}