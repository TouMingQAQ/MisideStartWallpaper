using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealToon.Script
{
    [ExecuteAlways]
    [AddComponentMenu("RealToon/Tools/ShadowT SDF Mode - Helper")]
    public class ShadowTSDFModeHelper : MonoBehaviour
    {
        [Header("Note: ShadowT feature and SDF Mode will be automatically enable\nWhen you put a material that uses RealToon Shader on the Material slot.")]

        [Space(25)]

        [Space(10)]
        [SerializeField]
        [Tooltip("A material that uses 'RealToon - ShadowT' feature and enabled 'SDF Mode'.")]
        public Material Material = null;

        [SerializeField]
        [Tooltip("An object to follow the position.")]
        public Transform ObjectToFollow = null;

        [HideInInspector]
        [SerializeField]
        bool checkstart = true;

        string RT_Sha_Nam_URP = "Universal Render Pipeline/RealToon/Version 5/Default/Default";
        string RT_Sha_Nam_HDRP = "HDRP/RealToon/Version 5/Default";

        string RT_Sha_Nam_BiRP_DD = "RealToon/Version 5/Default/Default";
        string RT_Sha_Nam_BiRP_DFT = "RealToon/Version 5/Default/Fade Transparency";
        string RT_Sha_Nam_BiRP_DR = "RealToon/Version 5/Default/Refraction";
        string RT_Sha_Nam_BiRP_TDD = "RealToon/Version 5/Tessellation/Default";
        string RT_Sha_Nam_BiRP_TDFT = "RealToon/Version 5/Tessellation/Fade Transparency";
        string RT_Sha_Nam_BiRP_TDR = "RealToon/Version 5/Tessellation/Refraction";

        void LateUpdate()
        {

            if (Material == null || ObjectToFollow == null)
            { }
            else
            {
                if (Material.shader.name == RT_Sha_Nam_URP ||
                    Material.shader.name == RT_Sha_Nam_HDRP ||
                    Material.shader.name == RT_Sha_Nam_BiRP_DD ||
                    Material.shader.name == RT_Sha_Nam_BiRP_DFT ||
                    Material.shader.name == RT_Sha_Nam_BiRP_DR ||
                    Material.shader.name == RT_Sha_Nam_BiRP_TDD ||
                    Material.shader.name == RT_Sha_Nam_BiRP_TDFT ||
                    Material.shader.name == RT_Sha_Nam_BiRP_TDR)
                {
                    Material.SetVector("_ObjectForward", ObjectToFollow.transform.forward);
                    Material.SetVector("_ObjectRight", ObjectToFollow.transform.right);
                }
            }

        }

        void OnValidate()
        {
            if (Material == null)
            {
                checkstart = true;
            }
            else if (Material != null)
            {
                if (Material.shader.name == RT_Sha_Nam_URP ||
                    Material.shader.name == RT_Sha_Nam_HDRP ||
                    Material.shader.name == RT_Sha_Nam_BiRP_DD ||
                    Material.shader.name == RT_Sha_Nam_BiRP_DFT ||
                    Material.shader.name == RT_Sha_Nam_BiRP_DR ||
                    Material.shader.name == RT_Sha_Nam_BiRP_TDD ||
                    Material.shader.name == RT_Sha_Nam_BiRP_TDFT ||
                    Material.shader.name == RT_Sha_Nam_BiRP_TDR)
                {
                    if (checkstart == true)
                    {
                        if ( (Material.IsKeywordEnabled("N_F_ST_ON") == false && Material.IsKeywordEnabled("N_F_STSDFM_ON") == false) ||
                            (Material.IsKeywordEnabled("N_F_ST_ON") == true && Material.IsKeywordEnabled("N_F_STSDFM_ON") == false) ||
                            (Material.IsKeywordEnabled("N_F_ST_ON") == false && Material.IsKeywordEnabled("N_F_STSDFM_ON") == true) )
                        {
                            Material.EnableKeyword("N_F_ST_ON");
                            Material.SetFloat("_N_F_ST", 1.0f);

                            Material.EnableKeyword("N_F_STSDFM_ON");
                            Material.SetFloat("_N_F_STSDFM", 1.0f);
                            checkstart = false;
                        }
                    }
                }
            }
        }

        void Reset()
        {
            Material = null;
            ObjectToFollow = null;
            checkstart = true;
        }

    }

}
