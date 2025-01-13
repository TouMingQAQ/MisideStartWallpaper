using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using VInspector;
using MitaMisiteAction.Nod;

public class AudioAnimation : MonoBehaviour
{
    [SerializeField]
    private MitaNodController nodController;

    [SerializeField,ReadOnly]
    public MiSideStart miside;

    private void Awake()
    {
        nodController = new MitaNodController(startListen: true)
    }

    private void Update()
    {
        if(!MiSideStart.config.MusicHead)
            return;
        if (nodController.nod)
        {
            miside.NodOnShot();
        }
    }

}
