using System;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using FMODUnity;
using Newtonsoft.Json;
using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.EventSystems;
using VInspector;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif
public struct MiSideConfig
{
    /// <summary>
    /// 开场动画范围
    /// </summary>
    public Vector2Int StartAnimationRange;
    /// <summary>
    /// 视野跟踪幅度
    /// </summary>
    public Vector4 LookAtOffsetMultiplier;
    /// <summary>
    /// 限制帧率
    /// </summary>
    public int TargetFrameRate;
    /// <summary>
    /// 跟随鼠标
    /// </summary>
    public bool LookAtMouse;
    /// <summary>
    /// 跟随音乐点头
    /// </summary>
    public bool MusicHead;

    /// <summary>
    /// 触发点击动画的连续点击次数
    /// </summary>
    public int ClickCount;
    
    public static MiSideConfig Default()
    {
        return new MiSideConfig()
        {
            StartAnimationRange = new Vector2Int(0, 5),
            TargetFrameRate = 60,
            LookAtMouse = true,
            MusicHead = true,
            ClickCount = 2,
            LookAtOffsetMultiplier = new Vector4(3f, 3f, 3f, 3f),
        };
    }
}
public class MiSideStart : MonoBehaviour,IPointerClickHandler
{
    private static readonly int Init = Animator.StringToHash("Init");
    private static readonly int OnClick = Animator.StringToHash("OnClick");
    public static MiSideConfig config;

    [Tab("Components")]
    public Animator animator;
    public MouseToWorldControl mouseControl;
    public LookAtIK lookAtIk;
    public ParticleSystem winkParticles;
    public Transform winkRoot;
    [Tab("Config")]
    [SerializeField] private string ConfigPath;
    [Tab("Normal")] 
    [InspectorName("帧率限制")]
    public int targetFrameRate = 60;
    [InspectorName("权重列表")]
    public List<float> weightList;
    [InspectorName("开场动画随机范围")]
    public Vector2Int startAnimationRange = new Vector2Int(0, 5);
    [SerializeField,ReadOnly]
    private bool canControl = true;
    [Tab("Nod")]
    [SerializeField,ReadOnly]
    private bool noding;
    [SerializeField]
    private float nodStartDelay = 0.03f;
    [SerializeField]
    private Vector3 headOffset = new Vector3(0,0.375f,0);
    [SerializeField]
    private Vector3 nodEnd = new Vector3(0,-0.1f,0);
    [SerializeField]
    private AnimationCurve nodCurve = AnimationCurve.Linear(0,0,1,1);
    [SerializeField]
    private float waitTime = 0.025f;
    [SerializeField]
    private Vector2 nodDuration = new Vector2(0.3f,0.4f);
    
    [Tab("OnClick")]
    public EventReference onClickMiSideAudio;
    public ClickParticle clickParticle;
    public int clickCount = 2;
    public Vector2 clickDelayRange = new Vector2(0.4f, 0.6f);
    public Vector2Int clickRange = new Vector2Int(2, 4);
    [SerializeField,ReadOnly]
    private float clickTimer = 0;
    [SerializeField,ReadOnly]
    private int clickCountTimer = 0;

    private void Awake()
    {
        ConfigPath = Application.persistentDataPath + "/MiSideStartConfig.json";
        LoadConfig();
        mouseControl.offset = config.LookAtOffsetMultiplier;
        Application.targetFrameRate = targetFrameRate;
        HideControl();
        winkParticles = Instantiate(winkParticles,winkRoot);
        animator.SetInteger(Init,GetStartAnimationIndex());
    }
    [Button,Tab("Normal")]
    public void TestAnimationWeight()
    {
        Debug.Log($"Index:{GetStartAnimationIndex()}");
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
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }
#endif
    void LoadConfig()
    {
        if (!File.Exists(ConfigPath))
        {
            config = MiSideConfig.Default();
            var json = JsonConvert.SerializeObject(config, Formatting.Indented,new VectorConverter());
            File.WriteAllText(ConfigPath, json);
        }
        else
        {
            var json = File.ReadAllText(ConfigPath);
            try
            {
                config = JsonConvert.DeserializeObject<MiSideConfig>(json,new VectorConverter());
            }
            catch (Exception e)
            {
                config = MiSideConfig.Default();
                json = JsonConvert.SerializeObject(config, Formatting.Indented,new VectorConverter());
                File.WriteAllText(ConfigPath, json);
            }
        }
        clickCount = config.ClickCount;
        targetFrameRate = config.TargetFrameRate;
        startAnimationRange = config.StartAnimationRange;
        
    }
    
    [ContextMenu("NodOnShot")]
    public void NodOnShot()
    {
        if(noding)
            return;
        noding = true;
        var offset =headOffset;
        var q = DOTween.Sequence();
        q.AppendInterval(nodStartDelay);
        var tween = DOTween.To(()=>headOffset,x=>headOffset=x,nodEnd,nodDuration.x).SetEase(nodCurve);
        q.Append(tween);
        q.AppendInterval(waitTime);
        tween = DOTween.To(()=>headOffset,x=>headOffset=x,offset,nodDuration.y).SetEase(nodCurve);
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
        lookAtIk.solver.headTargetOffset = headOffset;
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


    public void Wink()
    {
        winkParticles.Play();
    }

    public void HideControl()
    {
        mouseControl.enabled = false;
        lookAtIk.solver.SetLookAtWeight(0);
        canControl = false;
    }
    

    public void OnStartAnimationEnd()
    {
        mouseControl.enabled = true;
        canControl = true;
        lookAtIk.solver.SetLookAtWeight(1);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!canControl)
            return;
        clickCountTimer++;
        if (clickTimer <= 0)
        {
            clickTimer = Random.Range(clickDelayRange.x, clickDelayRange.y);
        }
        if (clickCountTimer >= clickCount)
        {
            SetAnimation();
            clickCountTimer = 0;
            clickCount = Random.Range(clickRange.x, clickRange.y);
            clickTimer = 0;
        }
        clickParticle.OnClick();
        RuntimeManager.PlayOneShot(onClickMiSideAudio);
        void SetAnimation()
        {
            HideControl();
            animator.SetTrigger(OnClick);
        }
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