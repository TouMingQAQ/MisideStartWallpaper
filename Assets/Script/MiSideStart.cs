using System;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using VInspector;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum LookAtState
{
    None,
    Always,
    OnlyPress,
}


public class MiSideStart : MonoBehaviour
{
    private static readonly int Init = Animator.StringToHash("Init");
    private static readonly int Restart = Animator.StringToHash("Restart");
    public static readonly string Version = "0.0.5";
    public static MiSideStart instance;
    [Tab("Config")]
    public MisideConfig config;
    [Tab("Components")]
    public MitaControl control;
    public Camera mainCamera;
    public UniversalAdditionalCameraData additionalCameraData;
    public MouseToWorldControl mouseControl;

    [Tab("Normal")] 
    [InspectorName("帧率限制")]
    public int targetFrameRate = 60;
    [InspectorName("权重列表")]
    public List<float> weightList;
    [InspectorName("开场动画随机范围")]
    public Vector2Int startAnimationRange = new Vector2Int(0, 5);
    [SerializeField,ReadOnly]
    public bool canControl = true;
    [SerializeField,ReadOnly]
    private float lookAtIkWeight = 0;
    [SerializeField,ReadOnly]
    private float lookAtTargetIkWeight = 0;

    [Tab("Nod")]
    [SerializeField,ReadOnly]
    private bool noding;
    //[SerializeField]
    //private float nodStartDelay = 0.03f;   //尚未使用？
    [SerializeField]
    private Vector3 headOffset = new Vector3(0,0.375f,0);
    [SerializeField]
    private Vector3 nodEnd = new Vector3(0,-0.1f,0);
    [SerializeField]
    private AnimationCurve nodCurveStart = AnimationCurve.Linear(0,0,1,1);
    [SerializeField]
    private AnimationCurve nodCurveEnd = AnimationCurve.Linear(0,0,1,1);
    [SerializeField]
    private float waitTime = 0.025f;
    [SerializeField]
    private Vector2 nodDuration = new Vector2(0.3f,0.4f);
    
    [Tab("OnClick")]
    public ClickParticle clickParticle;
    public int clickCount = 2;
    public Vector2 clickDelayRange = new Vector2(0.4f, 0.6f);
    public Vector2Int clickRange = new Vector2Int(2, 4);
    [SerializeField,ReadOnly]
    public float clickTimer = 0;
    [SerializeField,ReadOnly]
    public int clickCountTimer = 0;
    
    [Tab("Mita")]
    public List<MitaControl> controlPrefabs = new();
    private void Awake()
    {
        instance = this;
        config.Init();
        LoadControl();
        mouseControl.offsetX = config.value.LookAtOffsetMultiplierX;
        mouseControl.offsetY = config.value.LookAtOffsetMultiplierY;
        Application.targetFrameRate = targetFrameRate;
        HideControl();
        control.animator.SetInteger(Init,GetStartAnimationIndex());
    }

    void LoadControl()
    {
        var index = Random.Range(0,controlPrefabs.Count);
        control = controlPrefabs[index];
        control = Instantiate(control,this.transform);
        control.start = this;
        //加载IK
        control.lookAtIk.solver.target = mouseControl.control;
    }
    int GetStartAnimationIndex()
    {
        if(startAnimationRange.x >= startAnimationRange.y)
            return startAnimationRange.x;
        var r = Random.Range(0,1f);
        var totalWeight = 0f;
        for (int i = startAnimationRange.x; i < startAnimationRange.y; i++)
        {
            totalWeight+=weightList[i];
        }
        var targetWeight = totalWeight * r;
        var currentWeight = 0f;
        for (int i = startAnimationRange.x; i < startAnimationRange.y; i++)
        {
            currentWeight += weightList[i];
            if (currentWeight >= targetWeight)
            {
                return i;
            }
        }

        return 0;
    }

#if UNITY_EDITOR
    [Button,Tab("Config")]
    public void OpenConfig()
    {
        EditorUtility.RevealInFinder(Application.streamingAssetsPath);
    }
#endif

    public void ApplyConfig()
    {
        clickCount = config.value.ClickCount;
        targetFrameRate = config.value.TargetFrameRate;
        startAnimationRange =  config.value.StartAnimationRange;
        mainCamera.allowHDR = true;
        additionalCameraData.antialiasing = AntialiasingMode.TemporalAntiAliasing;
        additionalCameraData.taaSettings.quality =  config.value.TAAQuality;
    } 
    
    [ContextMenu("NodOnShot")]
    public void NodOnShot()
    {
        if(noding)
            return;
        noding = true;
        var offset =headOffset;
        var q = DOTween.Sequence();
        // q.AppendInterval(nodStartDelay);
        var tween = DOTween.To(()=>headOffset,x=>headOffset=x,nodEnd,nodDuration.x).SetEase(nodCurveStart);
        q.Append(tween);
        q.AppendInterval(waitTime);
        tween = DOTween.To(()=>headOffset,x=>headOffset=x,offset,nodDuration.y).SetEase(nodCurveEnd);
        q.Append(tween);
        q.AppendCallback(()=>
        {
            noding = false;
            headOffset =offset;
        });
        q.Play().SetAutoKill(true);
    }
    private void Update()
    {
        control.lookAtIk.solver.headTargetOffset = headOffset;
        lookAtIkWeight = Mathf.Lerp(lookAtIkWeight, lookAtTargetIkWeight, 0.1f);
        control.lookAtIk.solver.SetLookAtWeight(lookAtIkWeight);
        if (clickTimer >= 0)
        {
            clickTimer -= Time.deltaTime;
            if (clickTimer <= 0)
            {
                //点击清零
                clickCountTimer = 0;
            }
        }
    }

    

    public void HideControl()
    {
        mouseControl.enabled = false;
        lookAtTargetIkWeight = 0;
        lookAtIkWeight = 0;
        canControl = false;
    }

    public void EnableControl()
    {
        mouseControl.enabled = true;
        canControl = true;
        lookAtTargetIkWeight = 1;
    }

    public void SaveConfig()
    {
        config.SaveConfig();
        ApplyConfig();
    }
}
/// <summary>
/// 使Json.Net可以正确序列化或反序列化Unity中的Vector数据
/// </summary>
public class VectorConverter : JsonConverter
{
    public override bool CanRead => true;
    public override bool CanWrite => true;
    public override bool CanConvert(Type objectType)
    {
        return typeof(Vector2) == objectType ||
               typeof(Vector2Int) == objectType ||
               typeof(Vector3) == objectType ||
               typeof(Vector3Int) == objectType ||
               typeof(Vector4) == objectType;
    }
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return objectType switch
        {
            var t when t == typeof(Vector2) => JsonConvert.DeserializeObject<Vector2>(serializer.Deserialize(reader).ToString()),
            var t when t == typeof(Vector2Int) => JsonConvert.DeserializeObject<Vector2Int>(serializer.Deserialize(reader).ToString()),
            var t when t == typeof(Vector3) => JsonConvert.DeserializeObject<Vector3>(serializer.Deserialize(reader).ToString()),
            var t when t == typeof(Vector3Int) => JsonConvert.DeserializeObject<Vector3Int>(serializer.Deserialize(reader).ToString()),
            var t when t == typeof(Vector4) => JsonConvert.DeserializeObject<Vector4>(serializer.Deserialize(reader).ToString()),
            _ => throw new Exception("Unexpected Error Occurred"),
        };
    }
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        switch (value)
        {
            case Vector2 v:
                writer.WritePropertyName("x");
                writer.WriteValue(v.x);
                writer.WritePropertyName("y");
                writer.WriteValue(v.y);
                break;
            case Vector2Int v:
                writer.WritePropertyName("x");
                writer.WriteValue(v.x);
                writer.WritePropertyName("y");
                writer.WriteValue(v.y);
                break;
            case Vector3 v:
                writer.WritePropertyName("x");
                writer.WriteValue(v.x);
                writer.WritePropertyName("y");
                writer.WriteValue(v.y);
                writer.WritePropertyName("z");
                writer.WriteValue(v.z);
                break;
            case Vector3Int v:
                writer.WritePropertyName("x");
                writer.WriteValue(v.x);
                writer.WritePropertyName("y");
                writer.WriteValue(v.y);
                writer.WritePropertyName("z");
                writer.WriteValue(v.z);
                break;
            case Vector4 v:
                writer.WritePropertyName("x");
                writer.WriteValue(v.x);
                writer.WritePropertyName("y");
                writer.WriteValue(v.y);
                writer.WritePropertyName("z");
                writer.WriteValue(v.z);
                writer.WritePropertyName("w");
                writer.WriteValue(v.w);
                break;
            default:
                throw new Exception("Unexpected Error Occurred");
        }
        writer.WriteEndObject();
    }
}