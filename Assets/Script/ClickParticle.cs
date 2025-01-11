using System;
using System.Collections.Generic;
using UnityEngine;
#if !UNITY_ANDROID
using UnityEngine.InputSystem;
#endif
using VInspector;
using Random = UnityEngine.Random;

public class ClickParticle : MonoBehaviour
{
    [SerializeField,ReadOnly]
    private ParticleSystem particleSystem;
    public List<ParticleSystem> clickParticles = new();
    public Canvas canvas;
    

    public void OnClick()
    {
        var randomIndex = Random.Range(0, clickParticles.Count);
        var particle = clickParticles[randomIndex];
        if(particle == null)
            return;
        if (particleSystem != null)
        {
            particleSystem.Stop();
            particleSystem = null;
        }
        particleSystem = particle;
        #if !UNITY_ANDROID
        var mousePos = Mouse.current.position.ReadValue();
        #else
        var mousePos = (Vector2)Input.mousePosition;
        #endif
        var position = canvas.worldCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, canvas.planeDistance));

        particleSystem.transform.position = position;
        particleSystem.Play();
    }
    
}
