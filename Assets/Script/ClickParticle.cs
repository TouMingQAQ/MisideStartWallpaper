using System.Collections.Generic;
using UnityEngine;
#if !UNITY_ANDROID
using UnityEngine.InputSystem; //注:非Android平台
#endif
using VInspector; 
using Random = UnityEngine.Random; 

public class ClickParticle : MonoBehaviour
{
    [SerializeField, ReadOnly] 
    private ParticleSystem currentParticleSystem; // 更改：重命名为currentParticleSystem避免与Unity组件中的属性名冲突

    public List<ParticleSystem> clickParticles = new(); 
    public Canvas canvas; 

    public void OnClick()
    {
        // 更改：增加对clickParticles是否为空的检查，防止空列表导致异常
        if (clickParticles == null || clickParticles.Count == 0) return;

        var randomIndex = Random.Range(0, clickParticles.Count);
        var particle = clickParticles[randomIndex];
        if(particle == null)
            return;

        if (currentParticleSystem != null) // 更改：使用currentParticleSystem代替particleSystem
        {
            currentParticleSystem.Stop();
            currentParticleSystem = null; 
        }
        currentParticleSystem = particle; 

#if !UNITY_ANDROID
        var mousePos = Mouse.current.position.ReadValue();
#else
        var mousePos = (Vector2)Input.mousePosition;
#endif

        // 更改：增加对canvas.worldCamera是否为null的检查，避免NullReferenceException
        if (canvas.worldCamera == null) return;

        var position = canvas.worldCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, canvas.planeDistance));

        currentParticleSystem.transform.position = position;
        currentParticleSystem.Play();
    }
}