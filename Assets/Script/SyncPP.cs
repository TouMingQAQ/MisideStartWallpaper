using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class SyncPP : MonoBehaviour
{
    public float postExposure;
    private ColorAdjustments colorAdjustments;
    public Volume volume;

    private void Awake()
    {
        volume.profile.TryGet(out colorAdjustments);

    }

    private void Update()
    {
        colorAdjustments.postExposure.value = postExposure;
    }

    private void Reset()
    {
        TryGetComponent(out volume);
    }
}
