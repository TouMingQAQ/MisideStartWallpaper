using System;
using System.Collections.Generic;
using UnityEngine;
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
        var mousePos = Input.mousePosition;
        var position = canvas.worldCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, canvas.planeDistance));

        particleSystem.transform.position = position;
        particleSystem.Play();
    }
    
}
