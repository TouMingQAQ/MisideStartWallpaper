//Swap Shader To RealToon
//MJQStudioWorks
//2024

using UnityEngine;
using UnityEditor;

namespace RealToon.Tools
{
    public class SwapShaderToRealToon : EditorWindow
    {
        #region Variables

        private string ShaderName = string.Empty;

        private Color ShaColor = Color.white;
        private Color ShaEmiColor = Color.black;
        private Color VRMShadeColor = Color.black;
        private Color VRMRimColor = Color.black;
        private Color VRMOutliColor = Color.black;
        private Color ShaSpecCol = Color.black;
        private Color RTBiPDefCol = new Color(0.6886792f, 0.6886792f, 0.6886792f);

        private float MatType = 0.0f;
        private float ShaNormalScale = 0.0f;
        private float CullingMode = 0.0f;
        private float VRMOutlineMode = 0.0f;
        private float ShaSmooth = 0.0f;
        private float ShaAlpClip = 0.0f;
        private float ShaMetal = 0.0f;
        private float ShaSpecHighEn = 0.0f;
        private float ShaRefEn = 0.0f;
        private float ShaWorFloMod = 0.0f;
        private float ShaMetaRemMinMax = 0.0f;
        private float ShaSmoRemMinMax = 0.0f;
        private float ShaDoubSid = 0.0f;
        private float ShaMatID = 0.0f;

        private Texture ShaMainTex = null;
        private Texture ShaNormalMap = null;
        private Texture ShaEmiMap = null;
        private Texture ShaMetaMap = null;
        private Texture ShaSpecMap = null;
        private Texture ShaMasMa = null;
        private Texture VRMShaOutWid = null;

        private bool Enswap = true;
        private bool SKEmi = false;
        private bool ForTrasCuto = true;
        private bool EnhaHiLighColInt = true;
        private bool IncShaCol = true;
        private bool IncEmi = false;
        private bool EnaGiSha = false;
        private bool GiFlaLo = false;
        private bool FERL = false;
        private bool FUL = false;
        private bool UsEmiMapAnColAsGloTex = false;
        private bool DisRecSha = false;
        private bool LigAffSha = false;
        static string InfoString = string.Empty;
        private string FromShader = "VRoid|VRM";
        private string UnShaType = string.Empty;
        private string ProcMat = "None";
        static string SupShaURP = string.Empty;
        static string SupShaHDRP = string.Empty;
        static string SupShaBiRP = string.Empty;
        static string SupShaVRM = string.Empty;

        private int MatNum = 0;
        static float WinHig = 700;
        static EditorWindow EdiWin;
        static Shader ShaRTURP;
        static Shader ShaRTHDRP;
        static Shader ShaRTBID;
        static Shader ShaRTBIFT;
        static Shader ShaVRM;
        static Shader ShaVRM10;
        static Shader ShaVRM10URP;
        private string RTShader = "No RealToon Shader In Your Project";

        int ToBaInt = 0;
        private string[] tobastrings = { "VRoid|VRM", "Unity" };

        private Vector2 scroll;

        #endregion

        [MenuItem("Window/RealToon/Shader Swap to RealToon")]
        static void Init()
        {
            EdiWin = GetWindow<SwapShaderToRealToon>(true);
            EdiWin.titleContent = new GUIContent("Swap Shader to RealToon");
            WinHig = 700;
            EdiWin.minSize = new Vector2(420, WinHig);
            EdiWin.maxSize = new Vector2(420, WinHig);
            ShaRTURP = Shader.Find("Universal Render Pipeline/RealToon/Version 5/Default/Default");
            ShaRTHDRP = Shader.Find("HDRP/RealToon/Version 5/Default");
            ShaRTBID = Shader.Find("RealToon/Version 5/Default/Default");
            ShaRTBIFT = Shader.Find("RealToon/Version 5/Default/Fade Transparency");
            ShaVRM = Shader.Find("VRM/MToon");
            ShaVRM10 = Shader.Find("VRM10/MToon10");
            ShaVRM10URP = Shader.Find("VRM10/Universal Render Pipeline/MToon10");

            SupShaURP = "Supported Unity URP Shaders:\n" +
            "*Complex Lit\n" +
            "*Lit\n" +
            "*Simple Lit\n" +
            "*Unlit\n" +
            "*Baked Lit\n\n";

            SupShaHDRP = "Supported Unity HDRP Shaders:\n" +
            "*Lit\n" +
            "*LitTessellation\n" +
            "*Unlit\n\n";

            SupShaBiRP = "Supported Unity Built-In Shaders:\n" +
            "*Standard\n" +
            "*Standard (Specular setup)\n" +
            "*Unlit/Color\n" +
            "*Unlit/Texture\n" +
            "*Unlit/Transparent" +
            "*Unlit/Transparent Cutout\n\n";

            SupShaVRM = "Supported VRoid|VRM Shaders:\n" +
                        "*VRM\n" +
                        "*VRM10\n" +
                        "*VRM10 [URP]\n\n";

            InfoString = SupShaVRM;

            //For Future Use
            //EditorWindow.GetWindow(typeof(SwapShaderToRealToon));
            //EdiWin.ShowTab();
        }

        void OnGUI()
        {
            Object[] mat = Selection.GetFiltered(typeof(Material), SelectionMode.Assets);
            var lblcenstyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };

            #region Checking

            if (ShaRTURP)
            {
                Enswap = true;
                RTShader = "Click To Swap To RealToon (URP) Shader";
            }
            else if (ShaRTHDRP)
            {
                Enswap = true;
                RTShader = "Click To Swap To RealToon (HDRP) Shader";
            }
            else if (ShaRTBID && ShaRTBIFT)
            {
                Enswap = true;
                RTShader = "Click To Swap To RealToon (Built-In) Shader";
            }
            else if (ShaRTURP && ShaRTHDRP && (ShaRTBID && ShaRTBIFT))
            {
                Enswap = false;
                RTShader = "Use One RealToon Shader For Each Render Pipeline";
            }
            else
            {
                Enswap = false;
                RTShader = "No RealToon Built-In/URP/HDRP Shader In Your Project";
            }

            GUILayout.Space(10);
            GUILayout.Label("From Shader:");
            EditorGUI.BeginChangeCheck();
            ToBaInt = GUILayout.Toolbar(ToBaInt, tobastrings);
            if (EditorGUI.EndChangeCheck())
            {
                FromShader = tobastrings[ToBaInt];

                if (ToBaInt == 1)
                {
                    WinHig = 550;
                    EdiWin.minSize = new Vector2(420, WinHig);
                    EdiWin.maxSize = new Vector2(420, WinHig);

                    if (Shader.Find("Universal Render Pipeline/Complex Lit") || Shader.Find("Universal Render Pipeline/Lit") || Shader.Find("Universal Render Pipeline/Simple Lit") || Shader.Find("Universal Render Pipeline/Unlit") || Shader.Find("Universal Render Pipeline/Baked Lit"))
                    {
                        UnShaType = "(URP)";
                        InfoString = SupShaURP;

                    }
                    else if (Shader.Find("HDRP/Lit") || Shader.Find("HDRP/LitTessellation") || Shader.Find("HDRP/Unlit"))
                    {
                        UnShaType = "(HDRP)";
                        InfoString = SupShaHDRP;

                    }
                    else if (Shader.Find("Standard") || Shader.Find("Standard (Specular setup)") || Shader.Find("Unlit/Color") || Shader.Find("Unlit/Texture") || Shader.Find("Unlit/Transparent") || Shader.Find("Unlit/Transparent Cutout"))
                    {
                        UnShaType = "(Built-In)";
                        InfoString = SupShaBiRP;

                    }
                    else
                    {
                        UnShaType = string.Empty;
                    }
                }
                if (ToBaInt == 0)
                {
                    WinHig = 700;
                    EdiWin.minSize = new Vector2(420, WinHig);
                    EdiWin.maxSize = new Vector2(420, WinHig);

                    UnShaType = string.Empty;
                    InfoString = SupShaVRM;
                }
            }

            #endregion

            GUILayout.Space(10);
            GUILayout.Label("Selected: " + FromShader + " Shader " + UnShaType, lblcenstyle);

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Space(10);

            #region Processing

            EditorGUI.BeginDisabledGroup(Enswap == false);
            if (GUILayout.Button(RTShader))
            {

                if (mat.Length == 0)
                {
                    InfoString = "Please Select Materials\n\n";
                }
                else
                {
                    InfoString = string.Empty;
                    MatNum = 0;

                    foreach (Material m in mat)
                    {
                        ProcMat = m.name;

                        if (ToBaInt == 0)
                        {
                            if (ShaVRM || ShaVRM10)
                            {
                                if (m.shader.name == "VRM/MToon" || m.shader.name == "VRM10/MToon10" || m.shader.name == "VRM10/Universal Render Pipeline/MToon10")
                                {
                                    ShaderName = m.shader.name;

                                    if (ShaderName == "VRM10/MToon10" || m.shader.name == "VRM10/Universal Render Pipeline/MToon10")
                                    {
                                        MatType = m.GetFloat("_AlphaMode");
                                    }
                                    else if (ShaderName == "VRM/MToon")
                                    {
                                        MatType = m.GetFloat("_BlendMode");
                                    }

                                    ShaMainTex = m.GetTexture("_MainTex");
                                    ShaNormalMap = m.GetTexture("_BumpMap");
                                    VRMShaOutWid = m.GetTexture("_OutlineWidthTex");
                                    ShaNormalScale = m.GetFloat("_BumpScale");
                                    ShaColor = m.GetColor("_Color");
                                    VRMShadeColor = m.GetColor("_ShadeColor");

                                    if (ShaderName == "VRM10/MToon10" || m.shader.name == "VRM10/Universal Render Pipeline/MToon10")
                                    {
                                        CullingMode = m.GetFloat("_DoubleSided");
                                    }
                                    else if (ShaderName == "VRM/MToon")
                                    {
                                        CullingMode = m.GetFloat("_CullMode");
                                    }

                                    ShaEmiMap = m.GetTexture("_EmissionMap");
                                    ShaEmiColor = m.GetColor("_EmissionColor");

                                    if (m.HasProperty("_RimColor"))
                                    {
                                        VRMRimColor = m.GetColor("_RimColor");
                                    }

                                    VRMOutlineMode = m.GetFloat("_OutlineWidthMode");
                                    VRMOutliColor = m.GetColor("_OutlineColor");

                                }
                                else if (m.shader.name != "VRM/MToon" || m.shader.name != "VRM10/MToon10" || m.shader.name == "VRM10/Universal Render Pipeline/MToon10")
                                {
                                    InfoString += "The selected '" + m.name + "' material, shader is not supported.\n '" + m.shader.name + "'\n\n" + SupShaVRM;
                                }

                                if (ShaderName == "VRM/MToon" || ShaderName == "VRM10/MToon10" || m.shader.name == "VRM10/Universal Render Pipeline/MToon10")
                                {

                                    if (ShaRTURP)
                                    {
                                        m.shader = ShaRTURP;
                                    }
                                    else if (ShaRTHDRP)
                                    {
                                        m.shader = ShaRTHDRP;
                                    }
                                    else if (ShaRTBID)
                                    {
                                        m.shader = ShaRTBID;
                                    }

                                    InfoString += "Processing Material: " + m.name + "\nPrevious Shader: " + ShaderName;

                                    if (FUL == true)
                                    {
                                        FERL = false;
                                        IncEmi = false;
                                        DisRecSha = false;
                                        LigAffSha = false;
                                    }

                                    m.SetFloat("_OutlineWidth", 0.12f);

                                    if (VRMShaOutWid != null)
                                    {
                                        m.SetTexture("_OutlineWidthControl", VRMShaOutWid);
                                    }

                                    if (ForTrasCuto != true)
                                    {
                                        if (MatType == 2.0f)
                                        {
                                            if (!ShaRTBID)
                                            {
                                                m.SetInt("_BleModSour", 5);
                                                m.SetInt("_BleModDest", 10);

                                                m.EnableKeyword("N_F_TRANS_ON");
                                                m.SetFloat("_TRANSMODE", 1.0f);
                                                m.renderQueue = 3000;
                                                m.SetOverrideTag("RenderType", "Transparent");

                                                if (ShaRTHDRP)
                                                {
                                                    if ((m.IsKeywordEnabled("N_F_R_ON") && (m.IsKeywordEnabled("N_F_ESSR_ON") || m.GetFloat("_N_F_ESSR") == 1.0f)) || ((m.IsKeywordEnabled("N_F_ESSGI_ON") || m.GetFloat("_N_F_ESSGI") == 1.0f)))
                                                    {
                                                        m.SetInt("_SSRefDeOn", 0);
                                                        m.SetInt("_SSRefGBu", 2);
                                                        m.SetInt("_SSRefMoVe", 32);
                                                    }

                                                    m.SetInt("_ZTeForLiOpa", 4);
                                                }
                                            }
                                            else if (ShaRTBID)
                                            {
                                                m.shader = ShaRTBIFT;
                                            }

                                        }
                                        else if (MatType == 1.0f)
                                        {
                                            if (!ShaRTBID)
                                            {
                                                m.SetInt("_BleModSour", 5);
                                                m.SetInt("_BleModDest", 10);

                                                m.EnableKeyword("N_F_TRANS_ON");
                                                m.SetFloat("_TRANSMODE", 1.0f);

                                                m.EnableKeyword("N_F_CO_ON");
                                                m.SetFloat("_N_F_CO", 1.0f);

                                                m.SetInt("_ZWrite", 1);

                                                if (ShaRTURP)
                                                {
                                                    m.SetFloat("_Cutout", 0.4f);
                                                }
                                                else if (ShaRTHDRP)
                                                {
                                                    m.SetFloat("_Cutout", 0.51f);
                                                }

                                                m.renderQueue = 2450;
                                                m.SetOverrideTag("RenderType", "TransparentCutout");

                                                if (ShaRTHDRP)
                                                {
                                                    if ((m.IsKeywordEnabled("N_F_R_ON") && (m.IsKeywordEnabled("N_F_ESSR_ON") || m.GetFloat("_N_F_ESSR") == 1.0f)) || ((m.IsKeywordEnabled("N_F_ESSGI_ON") || m.GetFloat("_N_F_ESSGI") == 1.0f)))
                                                    {
                                                        m.SetInt("_SSRefDeOn", 8);
                                                        m.SetInt("_SSRefGBu", 10);
                                                        m.SetInt("_SSRefMoVe", 40);
                                                    }

                                                    m.SetInt("_ZTeForLiOpa", 3);
                                                }

                                            }
                                            else if (ShaRTBID)
                                            {
                                                m.shader = ShaRTBID;
                                                m.EnableKeyword("N_F_CO_ON");
                                                m.SetFloat("_N_F_CO", 1.0f);
                                                m.SetFloat("_Cutout", 0.4f);

                                            }
                                        }

                                    }
                                    else if (ForTrasCuto == true)
                                    {
                                        if (MatType == 1.0f || MatType == 2.0f)
                                        {
                                            if (!ShaRTBID)
                                            {
                                                m.SetInt("_BleModSour", 5);
                                                m.SetInt("_BleModDest", 10);

                                                m.EnableKeyword("N_F_TRANS_ON");
                                                m.SetFloat("_TRANSMODE", 1.0f);

                                                m.EnableKeyword("N_F_CO_ON");
                                                m.SetFloat("_N_F_CO", 1.0f);

                                                m.SetInt("_ZWrite", 1);

                                                if (ShaRTURP)
                                                {
                                                    m.SetFloat("_Cutout", 0.4f);
                                                }
                                                else if (ShaRTHDRP)
                                                {
                                                    m.SetFloat("_Cutout", 0.51f);
                                                }

                                                m.renderQueue = 2450;
                                                m.SetOverrideTag("RenderType", "TransparentCutout");

                                                if (ShaRTHDRP)
                                                {
                                                    if ((m.IsKeywordEnabled("N_F_R_ON") && (m.IsKeywordEnabled("N_F_ESSR_ON") || m.GetFloat("_N_F_ESSR") == 1.0f)) || ((m.IsKeywordEnabled("N_F_ESSGI_ON") || m.GetFloat("_N_F_ESSGI") == 1.0f)))
                                                    {
                                                        m.SetInt("_SSRefDeOn", 8);
                                                        m.SetInt("_SSRefGBu", 10);
                                                        m.SetInt("_SSRefMoVe", 40);
                                                    }

                                                    m.SetInt("_ZTeForLiOpa", 3);
                                                }

                                            }
                                            else if (ShaRTBID)
                                            {
                                                m.shader = ShaRTBID;
                                                m.EnableKeyword("N_F_CO_ON");
                                                m.SetFloat("_N_F_CO", 1.0f);
                                                m.SetFloat("_Cutout", 0.4f);

                                            }
                                        }
                                    }

                                    if (ShaRTURP)
                                    {
                                        if (EnhaHiLighColInt == true)
                                        {
                                            m.SetFloat("_HighlightColorPower", 0.7f);
                                            m.SetFloat("_OverallShadowColorPower", 0.7f);
                                        }
                                    }
                                    else if (ShaRTHDRP)
                                    {
                                        if (EnhaHiLighColInt == true)
                                        {
                                            m.SetFloat("_HighlightColorPower", 1.7f);
                                        }
                                    }
                                    else if (ShaRTBID)
                                    {
                                        if (PlayerSettings.colorSpace != ColorSpace.Gamma)
                                        {
                                            if (EnhaHiLighColInt == true)
                                            {
                                                if (m.GetColor("_MainColor") == RTBiPDefCol)
                                                {
                                                    m.SetColor("_MainColor", new Color(1.0f, 1.0f, 1.0f));
                                                    m.SetFloat("_HighlightColorPower", 0.8f);
                                                }
                                                else
                                                {
                                                    m.SetFloat("_HighlightColorPower", 0.8f);
                                                }
                                            }
                                        }
                                    }

                                    if (ShaNormalMap != null)
                                    {
                                        m.EnableKeyword("N_F_NM_ON");
                                        m.SetFloat("_N_F_NM", 1.0f);
                                        m.SetTexture("_NormalMap", ShaNormalMap);
                                        m.SetFloat("_NormalMapIntensity", ShaNormalScale);
                                    }

                                    if (!ShaRTBID)
                                    {
                                        if (ShaColor != Color.white)
                                        {
                                            m.SetColor("_MainColor", ShaColor * ShaColor);
                                        }
                                        if (VRMShadeColor != Color.black)
                                        {
                                            m.SetColor("_OverallShadowColor", VRMShadeColor * VRMShadeColor);
                                            if (IncShaCol == false)
                                            {
                                                m.SetColor("_OverallShadowColor", new Color(0.4f, 0.4f, 0.4f));
                                            }
                                        }
                                    }
                                    else if (ShaRTBID)
                                    {
                                        if (ShaColor != Color.white)
                                        {
                                            if (PlayerSettings.colorSpace == ColorSpace.Gamma)
                                            {
                                                m.SetColor("_MainColor", ShaColor * RTBiPDefCol);
                                            }
                                            else
                                            {
                                                m.SetColor("_MainColor", ShaColor);
                                            }
                                        }

                                        if (VRMShadeColor != Color.black && IncShaCol == true)
                                        {
                                            if (PlayerSettings.colorSpace == ColorSpace.Gamma)
                                            {
                                                m.SetColor("_OverallShadowColor", VRMShadeColor * RTBiPDefCol);
                                            }
                                            else
                                            {
                                                m.SetColor("_OverallShadowColor", VRMShadeColor);
                                            }
                                        }
                                        else if (IncShaCol == false)
                                        {
                                            m.SetColor("_OverallShadowColor", new Color(0.4f, 0.4f, 0.4f));
                                        }

                                        if ((ShaColor == Color.white) && EnhaHiLighColInt == false)
                                        {
                                            m.SetColor("_MainColor", RTBiPDefCol);
                                        }
                                    }

                                    if (!ShaRTBID)
                                    {
                                        if (ShaderName == "VRM10/MToon10" || m.shader.name == "VRM10/Universal Render Pipeline/MToon10")
                                        {
                                            if (CullingMode != 0.0f)
                                            {
                                                if (CullingMode == 1.0)
                                                {
                                                    m.SetFloat("_Culling", 0);
                                                }
                                            }
                                        }
                                        else if (ShaderName == "VRM/MToon")
                                        {
                                            if (CullingMode != 2.0f)
                                            {
                                                m.SetFloat("_Culling", CullingMode);
                                            }
                                        }
                                    }
                                    else if (ShaRTBID)
                                    {
                                        if (ShaderName == "VRM10/MToon10" || m.shader.name == "VRM10/Universal Render Pipeline/MToon10")
                                        {
                                            if (CullingMode == 1.0)
                                            {
                                                m.SetFloat("_Culling", 0);
                                            }
                                            else if (CullingMode == 0.0)
                                            {
                                                m.SetFloat("_Culling", 2);
                                            }
                                        }
                                        else if (ShaderName == "VRM/MToon")
                                        {
                                            if (CullingMode == 2.0f || CullingMode == 1.0f)
                                            {
                                                m.SetFloat("_DoubleSided", 2);
                                            }
                                            else if (CullingMode == 0.0f)
                                            {
                                                m.SetFloat("_DoubleSided", 0);
                                            }
                                        }
                                    }

                                    if (IncEmi == true)
                                    {
                                        if (ShaEmiMap != null || ShaEmiColor != Color.black)
                                        {
                                            m.EnableKeyword("N_F_SL_ON");
                                            m.SetFloat("_N_F_SL", 1.0f);
                                            m.SetFloat("_SelfLitIntensity", 1.0f);
                                            m.SetFloat("_SelfLitHighContrast", 0.0F);
                                            m.SetFloat("_SelfLitPower", 20.0f);
                                            m.SetTexture("_MaskSelfLit", ShaEmiMap);
                                            if (!ShaRTBID)
                                            {
                                                m.SetColor("_SelfLitColor", ShaEmiColor * ShaEmiColor);
                                            }
                                            else if (ShaRTBID)
                                            {
                                                if (PlayerSettings.colorSpace == ColorSpace.Gamma)
                                                {
                                                    m.SetColor("_SelfLitColor", ShaEmiColor * RTBiPDefCol);
                                                }
                                                else
                                                {
                                                    m.SetColor("_SelfLitColor", ShaEmiColor);
                                                }
                                            }
                                        }
                                    }

                                    if (FERL == false)
                                    {
                                        if ((VRMRimColor != Color.black && m.HasProperty("_RimColor")) || m.HasProperty("_RimColor"))
                                        {
                                            m.EnableKeyword("N_F_RL_ON");
                                            m.SetFloat("_N_F_RL", 1.0f);
                                            if (!ShaRTBID)
                                            {
                                                m.SetColor("_RimLightColor", VRMRimColor * VRMRimColor);
                                            }
                                            else if (ShaRTBID)
                                            {
                                                if (PlayerSettings.colorSpace == ColorSpace.Gamma)
                                                {
                                                    m.SetColor("_RimLightColor", VRMRimColor * RTBiPDefCol);
                                                }
                                                else
                                                {
                                                    m.SetColor("_RimLightColor", VRMRimColor);
                                                }
                                            }
                                        }
                                    }
                                    else if (FERL == true)
                                    {
                                        m.EnableKeyword("N_F_RL_ON");
                                        m.SetFloat("_N_F_RL", 1.0f);
                                        m.SetFloat("_RimLightSoftness", 0.0f);
                                        m.SetFloat("_RimLightUnfill", 2.3f);

                                        if (ShaRTHDRP || ShaRTURP)
                                        {
                                            m.SetFloat("_RimLigInt", 0.2f);
                                        }

                                        m.SetColor("_RimLightColor", new Color(1.0f, 1.0f, 1.0f));

                                    }

                                    if (VRMOutlineMode == 1 || VRMOutlineMode == 2)
                                    {
                                        if (!ShaRTBID)
                                        {
                                            m.SetColor("_OutlineColor", VRMOutliColor * VRMOutliColor);
                                        }
                                        else if (ShaRTBID)
                                        {
                                            if (PlayerSettings.colorSpace == ColorSpace.Gamma)
                                            {
                                                m.SetColor("_OutlineColor", VRMOutliColor * RTBiPDefCol);
                                            }
                                            else
                                            {
                                                m.SetColor("_OutlineColor", VRMOutliColor);
                                            }
                                        }
                                    }

                                    if (UsEmiMapAnColAsGloTex == true)
                                    {
                                        if (ShaEmiMap != null)
                                        {
                                            m.EnableKeyword("N_F_GLO_ON");
                                            m.SetFloat("_N_F_GLO", 1.0f);
                                            m.EnableKeyword("N_F_GLOT_ON");
                                            m.SetFloat("_N_F_GLOT", 1.0f);
                                            m.SetTexture("_GlossTexture", ShaEmiMap);
                                            m.SetTextureOffset("_GlossTexture", new Vector2(0.0f, 0.28f));

                                            if (ShaRTHDRP)
                                            {
                                                m.SetFloat("_GlossIntensity", 0.35f);
                                            }
                                            else if (ShaRTURP)
                                            {
                                                m.SetFloat("_GlossIntensity", 0.055f);
                                            }

                                            if (ShaRTBID)
                                            {
                                                m.SetFloat("_GlossColorPower", 100.0f);
                                            }

                                            if (ShaEmiColor != Color.black)
                                            {
                                                if (!ShaRTBID)
                                                {
                                                    m.SetColor("_GlossColor", ShaEmiColor * ShaEmiColor);
                                                }
                                                else
                                                {
                                                    if (PlayerSettings.colorSpace == ColorSpace.Gamma)
                                                    {
                                                        m.SetColor("_GlossColor", ShaEmiColor * RTBiPDefCol);
                                                    }
                                                    else
                                                    {
                                                        m.SetColor("_GlossColor", ShaEmiColor);
                                                    }
                                                }
                                            }
                                        }

                                    }

                                    if (EnaGiSha == true)
                                    {
                                        m.SetFloat("_GIShadeThreshold", 1.0f);
                                    }

                                    if (EnaGiSha == true && GiFlaLo == true)
                                    {
                                        m.SetFloat("_GIFlatShade", 1.0f);
                                    }

                                    m.SetFloat("_OutlineWidth", 0.12f);

                                    if (FUL == true)
                                    {
                                        if (ShaRTBID)
                                        {
                                            m.EnableKeyword("N_F_SL_ON");
                                            m.SetFloat("_N_F_SL", 1.0f);

                                            if (m.GetColor("_MainColor") == new Color(0.6886792f, 0.6886792f, 0.6886792f))
                                            {
                                                m.SetFloat("_SelfLitPower", 3);
                                            }
                                            else
                                            {
                                                m.SetFloat("_SelfLitPower", 0.2f);
                                            }

                                            m.SetFloat("_SelfLitIntensity", 1.0f);
                                            m.SetFloat("_SelfLitHighContrast", 0.0f);

                                            m.DisableKeyword("N_F_SS_ON");
                                            m.SetFloat("_N_F_SS", 0.0f);

                                            m.DisableKeyword("N_F_RELGI_ON");
                                            m.SetFloat("_RELG", 0.0f);

                                            m.DisableKeyword("N_F_RL_ON");
                                            m.SetFloat("_N_F_RL", 0.0f);

                                            m.DisableKeyword("N_F_NM_ON");
                                            m.SetFloat("_N_F_NM", 0.0f);

                                            if (m.HasProperty("_N_F_HDLS") && m.HasProperty("_N_F_HPSS"))
                                            {
                                                m.EnableKeyword("N_F_HDLS_ON");
                                                m.SetFloat("_N_F_HDLS", 1.0f);

                                                m.EnableKeyword("N_F_HPSS_ON");
                                                m.SetFloat("_N_F_HPSS", 1.0f);
                                            }
                                        }

                                        if (ShaRTURP)
                                        {
                                            m.EnableKeyword("N_F_SL_ON");
                                            m.SetFloat("_N_F_SL", 1.0f);
                                            m.SetFloat("_SelfLitPower", 0.3f);
                                            m.SetFloat("_SelfLitIntensity", 1.0f);
                                            m.SetFloat("_SelfLitHighContrast", 0.0f);

                                            m.DisableKeyword("N_F_SS_ON");
                                            m.SetFloat("_N_F_SS", 0.0f);

                                            m.DisableKeyword("N_F_RELGI_ON");
                                            m.SetFloat("_RELG", 0.0f);

                                            m.DisableKeyword("N_F_EAL_ON");
                                            m.SetFloat("_N_F_EAL", 0.0f);

                                            m.EnableKeyword("N_F_USETLB_ON");
                                            m.SetFloat("_UseTLB", 1.0f);

                                            m.DisableKeyword("N_F_NM_ON");
                                            m.SetFloat("_N_F_NM", 0.0f);

                                            m.EnableKeyword("N_F_HDLS_ON");
                                            m.SetFloat("_N_F_HDLS", 1.0f);

                                            m.EnableKeyword("N_F_HPSS_ON");
                                            m.SetFloat("_N_F_HPSS", 1.0f);
                                        }

                                        if (ShaRTHDRP)
                                        {
                                            m.EnableKeyword("N_F_SL_ON");
                                            m.SetFloat("_N_F_SL", 1.0f);
                                            m.SetFloat("_SelfLitPower", 6.5f);
                                            m.SetFloat("_SelfLitIntensity", 1.0f);
                                            m.SetFloat("_SelfLitHighContrast", 0.0f);

                                            m.DisableKeyword("N_F_SS_ON");
                                            m.SetFloat("_N_F_SS", 0.0f);

                                            m.DisableKeyword("N_F_RELGI_ON");
                                            m.SetFloat("_RELG", 0.0f);

                                            m.DisableKeyword("N_F_PAL_ON");
                                            m.SetFloat("_N_F_PAL", 0.0f);

                                            m.EnableKeyword("N_F_USETLB_ON");
                                            m.SetFloat("_UseTLB", 1.0f);

                                            m.DisableKeyword("N_F_NM_ON");
                                            m.SetFloat("_N_F_NM", 0.0f);

                                            m.EnableKeyword("N_F_HDLS_ON");
                                            m.SetFloat("_N_F_HDLS", 1.0f);

                                            m.EnableKeyword("N_F_HPSAS_ON");
                                            m.SetFloat("_N_F_HPSAS", 1.0f);
                                        }
                                    }

                                    if (ShaRTBID)
                                    {
                                        if (LigAffSha == true)
                                        {
                                            m.SetFloat("_LightAffectShadow", 1.0f);
                                        }

                                        if (m.HasProperty("_N_F_HDLS") && m.HasProperty("_N_F_HPSS"))
                                        {
                                            if (DisRecSha == true)
                                            {
                                                m.EnableKeyword("N_F_HDLS_ON");
                                                m.SetFloat("_N_F_HDLS", 1.0f);

                                                m.EnableKeyword("N_F_HPSS_ON");
                                                m.SetFloat("_N_F_HPSS", 1.0f);
                                            }
                                        }
                                    }

                                    if (ShaRTURP)
                                    {
                                        if (LigAffSha == true)
                                        {
                                            m.SetFloat("_LightAffectShadow", 1.0f);
                                        }

                                        if (DisRecSha == true)
                                        {
                                            m.EnableKeyword("N_F_HDLS_ON");
                                            m.SetFloat("_N_F_HDLS", 1.0f);

                                            m.EnableKeyword("N_F_HPSS_ON");
                                            m.SetFloat("_N_F_HPSAS", 1.0f);
                                        }
                                    }

                                    if (ShaRTHDRP)
                                    {
                                        if (LigAffSha == true)
                                        {
                                            m.SetFloat("_LightAffectShadow", 1.0f);
                                        }

                                        if (DisRecSha == true)
                                        {
                                            m.EnableKeyword("N_F_HDLS_ON");
                                            m.SetFloat("_N_F_HDLS", 1.0f);

                                            m.EnableKeyword("N_F_HPSAS_ON");
                                            m.SetFloat("_N_F_HPSAS", 1.0f);
                                        }
                                    }

                                    ShaderName = string.Empty;
                                    InfoString += "\n[Done]\n\n";

                                }
                            }
                            else if (!ShaVRM || !ShaVRM10)
                            {
                                InfoString = "Can't proceed, No VRoid|VRM shaders in your project";
                            }

                        }
                        else if (ToBaInt == 1)
                        {

                            if (UnShaType == "(URP)")
                            {

                                if (m.shader.name == "Universal Render Pipeline/Complex Lit" || m.shader.name == "Universal Render Pipeline/Lit" || m.shader.name == "Universal Render Pipeline/Simple Lit")
                                {
                                    ShaderName = m.shader.name;
                                    ShaMainTex = m.GetTexture("_BaseMap");
                                    ShaColor = m.GetColor("_BaseColor");
                                    ShaNormalMap = m.GetTexture("_BumpMap");
                                    ShaNormalScale = m.GetFloat("_BumpScale");
                                    ShaEmiColor = m.GetColor("_EmissionColor");
                                    ShaEmiMap = m.GetTexture("_EmissionMap");
                                    ShaSmooth = m.GetFloat("_Smoothness");
                                    MatType = m.GetFloat("_Surface");
                                    CullingMode = m.GetFloat("_Cull");
                                    ShaAlpClip = m.GetFloat("_AlphaClip");
                                    SKEmi = m.IsKeywordEnabled("_EMISSION");
                                    ShaSpecHighEn = m.GetFloat("_SpecularHighlights");

                                    if (m.HasProperty("_EnvironmentReflections"))
                                    {
                                        ShaRefEn = m.GetFloat("_EnvironmentReflections");
                                    }

                                    if (m.HasProperty("_SpecColor"))
                                    {
                                        ShaSpecCol = m.GetColor("_SpecColor");
                                    }

                                    if (m.HasProperty("_WorkflowMode"))
                                    {
                                        ShaWorFloMod = m.GetFloat("_WorkflowMode");
                                    }

                                    if (m.HasProperty("_Metallic"))
                                    {
                                        ShaMetal = m.GetFloat("_Metallic");
                                    }

                                    if (m.HasProperty("_MetallicGlossMap"))
                                    {
                                        ShaMetaMap = m.GetTexture("_MetallicGlossMap");
                                    }

                                    if (m.HasProperty("_SpecGlossMap"))
                                    {
                                        ShaSpecMap = m.GetTexture("_SpecGlossMap");
                                    }
                                }
                                else if (m.shader.name == "Universal Render Pipeline/Unlit")
                                {
                                    ShaderName = m.shader.name;
                                    ShaMainTex = m.GetTexture("_BaseMap");
                                    ShaColor = m.GetColor("_BaseColor");
                                    MatType = m.GetFloat("_Surface");
                                    CullingMode = m.GetFloat("_Cull");
                                    ShaAlpClip = m.GetFloat("_AlphaClip");
                                }
                                else if (m.shader.name == "Universal Render Pipeline/Baked Lit")
                                {
                                    ShaderName = m.shader.name;
                                    ShaMainTex = m.GetTexture("_BaseMap");
                                    ShaColor = m.GetColor("_BaseColor");
                                    ShaNormalMap = m.GetTexture("_BumpMap");
                                    MatType = m.GetFloat("_Surface");
                                    CullingMode = m.GetFloat("_Cull");
                                    ShaAlpClip = m.GetFloat("_AlphaClip");
                                }
                                else if (m.shader.name != "Universal Render Pipeline/Complex Lit" || m.shader.name != "Universal Render Pipeline/Lit" || m.shader.name != "Universal Render Pipeline/Simple Lit" || m.shader.name != "Universal Render Pipeline/Unlit" || m.shader.name != "Universal Render Pipeline/Baked Lit")
                                {
                                    InfoString += "The selected '" + m.name + "' material, shader is not supported.\n '" + m.shader.name + "'\n\n" + SupShaURP;
                                }

                                if (m.shader.name == "Universal Render Pipeline/Complex Lit" || m.shader.name == "Universal Render Pipeline/Lit" || m.shader.name == "Universal Render Pipeline/Simple Lit" || m.shader.name == "Universal Render Pipeline/Unlit" || m.shader.name == "Universal Render Pipeline/Baked Lit")
                                {
                                    if (ShaRTURP)
                                    {
                                        m.shader = ShaRTURP;
                                    }

                                    InfoString += "Processing Material: " + m.name + "\nPrevious Shader: " + ShaderName;

                                    if (ShaMainTex != null)
                                    {
                                        m.SetTexture("_MainTex", ShaMainTex);
                                    }

                                    if (ShaderName != "Universal Render Pipeline/Unlit" || ShaderName != "Universal Render Pipeline/Baked Lit")
                                    {
                                        m.SetColor("_OverallShadowColor", new Color(0.2f, 0.2f, 0.2f));
                                    }

                                    m.SetFloat("_OutlineWidth", 0.12f);

                                    if (MatType == 1.0f)
                                    {
                                        m.SetInt("_BleModSour", 5);
                                        m.SetInt("_BleModDest", 10);

                                        m.EnableKeyword("N_F_TRANS_ON");
                                        m.SetFloat("_TRANSMODE", 1.0f);
                                        m.renderQueue = 3000;
                                        m.SetOverrideTag("RenderType", "Transparent");
                                        m.SetFloat("_Opacity", ShaColor.a);

                                    }

                                    if (ShaAlpClip == 1.0f)
                                    {
                                        m.SetInt("_BleModSour", 5);
                                        m.SetInt("_BleModDest", 10);

                                        m.EnableKeyword("N_F_TRANS_ON");
                                        m.SetFloat("_TRANSMODE", 1.0f);

                                        m.EnableKeyword("N_F_CO_ON");
                                        m.SetFloat("_N_F_CO", 1.0f);
                                        m.SetFloat("_Cutout", 0.4f);
                                        m.renderQueue = 2450;
                                        m.SetOverrideTag("RenderType", "TransparentCutout");
                                    }

                                    if (ShaderName != "Universal Render Pipeline/Unlit")
                                    {
                                        if (ShaNormalMap != null)
                                        {
                                            m.EnableKeyword("N_F_NM_ON");
                                            m.SetFloat("_N_F_NM", 1.0f);
                                            m.SetTexture("_NormalMap", ShaNormalMap);
                                            m.SetFloat("_NormalMapIntensity", ShaNormalScale);
                                        }

                                    }

                                    if (ShaColor != Color.white)
                                    {
                                        m.SetColor("_MainColor", ShaColor * ShaColor);
                                    }

                                    if (CullingMode == 2.0f)
                                    {
                                        m.SetFloat("_Culling", 2.0f);
                                    }
                                    else if (CullingMode == 1.0f)
                                    {
                                        m.SetFloat("_Culling", 1.0f);
                                    }
                                    else if (CullingMode == 0.0)
                                    {
                                        m.SetFloat("_Culling", 0.0f);
                                    }

                                    if (ShaderName != "Universal Render Pipeline/Unlit" || ShaderName != "Universal Render Pipeline/Baked Lit")
                                    {
                                        if (SKEmi)
                                        {
                                            m.EnableKeyword("N_F_SL_ON");
                                            m.SetFloat("_N_F_SL", 1.0f);
                                            m.SetFloat("_SelfLitPower", 10);
                                            m.SetFloat("_SelfLitIntensity", 1.0f);
                                            m.SetTexture("_MaskSelfLit", ShaEmiMap);
                                            m.SetColor("_SelfLitColor", ShaEmiColor * ShaEmiColor);
                                        }
                                    }

                                    if (ShaderName != "Universal Render Pipeline/Unlit" || ShaderName != "Universal Render Pipeline/Baked Lit")
                                    {
                                        if (ShaSpecHighEn == 1 || ShaSpecHighEn == 0 && ShaderName == "Universal Render Pipeline/Simple Lit")
                                        {
                                            if (ShaSmooth >= 0.5)
                                            {
                                                m.EnableKeyword("N_F_GLO_ON");
                                                m.SetFloat("_N_F_GLO", 1.0f);
                                                m.SetFloat("_Glossiness", 0.6f);
                                            }

                                            if ((ShaWorFloMod == 0 && ShaderName != "Universal Render Pipeline/Simple Lit") || ShaderName == "Universal Render Pipeline/Simple Lit")
                                            {
                                                if (ShaSpecMap != null)
                                                {
                                                    m.SetTexture("_MaskGloss", ShaSpecMap);
                                                }
                                                else
                                                {
                                                    m.SetColor("_GlossColor", ShaSpecCol * ShaSpecCol);
                                                }
                                            }
                                            else if ((ShaWorFloMod == 1 && ShaderName != "Universal Render Pipeline/Simple Lit"))
                                            {
                                                if (ShaMetaMap != null)
                                                {
                                                    m.SetTexture("_MaskGloss", ShaMetaMap);
                                                }
                                            }
                                        }
                                    }

                                    if (ShaderName != "Universal Render Pipeline/Unlit" || ShaderName != "Universal Render Pipeline/Baked Lit")
                                    {
                                        if (ShaWorFloMod == 1 && ShaderName != "Universal Render Pipeline/Simple Lit")
                                        {
                                            if (ShaRefEn == 1.0f)
                                            {
                                                if (ShaMetal != 0.0f && ShaMetaMap == null)
                                                {
                                                    m.EnableKeyword("N_F_R_ON");
                                                    m.SetFloat("_N_F_R", 1.0f);
                                                    m.SetFloat("_ReflectionIntensity", ShaMetal);
                                                    m.SetFloat("_ReflectionRoughtness", 1.0f - ShaSmooth);
                                                    m.SetFloat("_RefMetallic", 0.65f);
                                                }
                                                else if (ShaMetaMap != null)
                                                {
                                                    m.EnableKeyword("N_F_R_ON");
                                                    m.SetFloat("_N_F_R", 1.0f);
                                                    m.SetFloat("_ReflectionIntensity", 1f);
                                                    m.SetFloat("_ReflectionRoughtness", 1.0f - ShaSmooth);
                                                    m.SetTexture("_MaskReflection", ShaMetaMap);
                                                    m.SetFloat("_RefMetallic", 0.65f);
                                                }
                                            }
                                        }

                                    }

                                    if (ShaderName == "Universal Render Pipeline/Baked Lit")
                                    {
                                        m.EnableKeyword("N_F_OFLMB_ON");
                                        m.SetFloat("_N_F_OFLMB", 1.0f);

                                        m.DisableKeyword("N_F_EAL_ON");
                                        m.SetFloat("_N_F_EAL", 0.0f);

                                        m.EnableKeyword("N_F_USETLB_ON");
                                        m.SetFloat("_UseTLB", 1.0f);

                                        m.EnableKeyword("N_F_HDLS_ON");
                                        m.SetFloat("_N_F_HDLS", 1.0f);

                                        m.EnableKeyword("N_F_HPSS_ON");
                                        m.SetFloat("_N_F_HPSS", 1.0f);

                                        m.DisableKeyword("N_F_SS_ON");
                                        m.SetFloat("_N_F_SS", 0.0f);

                                        m.SetFloat("_GIShadeThreshold", 1.0f);
                                    }

                                    if (ShaderName == "Universal Render Pipeline/Unlit")
                                    {
                                        m.EnableKeyword("N_F_SL_ON");
                                        m.SetFloat("_N_F_SL", 1.0f);
                                        m.SetFloat("_SelfLitPower", 0.3f);
                                        m.SetFloat("_SelfLitIntensity", 1.0f);

                                        m.DisableKeyword("N_F_SS_ON");
                                        m.SetFloat("_N_F_SS", 0.0f);

                                        m.DisableKeyword("N_F_RELGI_ON");
                                        m.SetFloat("_RELG", 0.0f);

                                        m.DisableKeyword("N_F_EAL_ON");
                                        m.SetFloat("_N_F_EAL", 0.0f);

                                        m.EnableKeyword("N_F_USETLB_ON");
                                        m.SetFloat("_UseTLB", 1.0f);

                                        m.EnableKeyword("N_F_HDLS_ON");
                                        m.SetFloat("_N_F_HDLS", 1.0f);

                                        m.EnableKeyword("N_F_HPSS_ON");
                                        m.SetFloat("_N_F_HPSS", 1.0f);
                                    }

                                    if (LigAffSha == true)
                                    {
                                        m.SetFloat("_LightAffectShadow", 1.0f);
                                    }

                                    ShaderName = string.Empty;
                                    InfoString += "\n[Done]\n\n";
                                }

                            }

                            if (UnShaType == "(HDRP)")
                            {

                                if (m.shader.name == "HDRP/Lit" || m.shader.name == "HDRP/LitTessellation")
                                {
                                    ShaderName = m.shader.name;
                                    ShaMainTex = m.GetTexture("_BaseColorMap");
                                    ShaColor = m.GetColor("_BaseColor");
                                    ShaNormalMap = m.GetTexture("_NormalMap");
                                    ShaNormalScale = m.GetFloat("_NormalScale");
                                    ShaEmiColor = m.GetColor("_EmissiveColor");
                                    ShaEmiMap = m.GetTexture("_EmissiveColorMap");
                                    ShaSmooth = m.GetFloat("_Smoothness");
                                    MatType = m.GetFloat("_SurfaceType");
                                    CullingMode = m.GetFloat("_CullMode");
                                    ShaAlpClip = m.GetFloat("_AlphaCutoffEnable");
                                    ShaMasMa = m.GetTexture("_MaskMap");
                                    ShaMetal = m.GetFloat("_Metallic");
                                    ShaMetaRemMinMax = Mathf.Clamp01(m.GetFloat("_MetallicRemapMin") + m.GetFloat("_MetallicRemapMax"));
                                    ShaSmoRemMinMax = m.GetFloat("_SmoothnessRemapMin") + m.GetFloat("_SmoothnessRemapMax");
                                    ShaDoubSid = m.GetFloat("_DoubleSidedEnable");
                                    ShaMatID = m.GetFloat("_MaterialID");
                                    ShaSpecMap = m.GetTexture("_SpecularColorMap");
                                    ShaSpecCol = m.GetColor("_SpecularColor");
                                }
                                else if (m.shader.name == "HDRP/Unlit")
                                {
                                    ShaderName = m.shader.name;
                                    ShaMainTex = m.GetTexture("_MainTex");
                                    ShaColor = m.GetColor("_Color");
                                    MatType = m.GetFloat("_SurfaceType");
                                    CullingMode = m.GetFloat("_CullMode");
                                    ShaAlpClip = m.GetFloat("_AlphaCutoffEnable");
                                    ShaDoubSid = m.GetFloat("_DoubleSidedEnable");
                                }
                                else if (m.shader.name != "HDRP/Lit" || m.shader.name != "HDRP/LitTessellation" || m.shader.name != "HDRP/Unlit")
                                {
                                    InfoString += "The selected '" + m.name + "' material, shader is not supported.\n '" + m.shader.name + "'\n\n" + SupShaHDRP;
                                }

                                if (m.shader.name == "HDRP/Lit" || m.shader.name == "HDRP/LitTessellation" || m.shader.name == "HDRP/Unlit")
                                {
                                    if (ShaRTHDRP)
                                    {
                                        m.shader = ShaRTHDRP;
                                    }

                                    InfoString += "Processing Material: " + m.name + "\nPrevious Shader: " + ShaderName;

                                    if (ShaMainTex != null)
                                    {
                                        m.SetTexture("_MainTex", ShaMainTex);
                                    }

                                    if (ShaderName != "HDRP/Unlit")
                                    {
                                        m.SetColor("_OverallShadowColor", new Color(0.2f, 0.2f, 0.2f));
                                    }

                                    m.SetFloat("_OutlineWidth", 0.12f);

                                    if (MatType == 1.0f)
                                    {
                                        m.SetInt("_BleModSour", 5);
                                        m.SetInt("_BleModDest", 10);

                                        m.EnableKeyword("N_F_TRANS_ON");
                                        m.SetFloat("_TRANSMODE", 1.0f);

                                        m.renderQueue = 3000;
                                        m.SetOverrideTag("RenderType", "Transparent");

                                        m.SetFloat("_Opacity", ShaColor.a);

                                        if ((m.IsKeywordEnabled("N_F_R_ON") && (m.IsKeywordEnabled("N_F_ESSR_ON") || m.GetFloat("_N_F_ESSR") == 1.0f)) || ((m.IsKeywordEnabled("N_F_ESSGI_ON") || m.GetFloat("_N_F_ESSGI") == 1.0f)))
                                        {
                                            m.SetInt("_SSRefDeOn", 0);
                                            m.SetInt("_SSRefGBu", 2);
                                            m.SetInt("_SSRefMoVe", 32);
                                        }

                                        m.SetInt("_ZTeForLiOpa", 4);
                                    }

                                    if (ShaAlpClip == 1.0f)
                                    {
                                        m.SetInt("_BleModSour", 5);
                                        m.SetInt("_BleModDest", 10);

                                        m.EnableKeyword("N_F_TRANS_ON");
                                        m.SetFloat("_TRANSMODE", 1.0f);

                                        m.EnableKeyword("N_F_CO_ON");
                                        m.SetFloat("_N_F_CO", 1.0f);
                                        m.SetFloat("_Cutout", 0.51f);
                                        m.renderQueue = 2450;

                                        m.SetOverrideTag("RenderType", "TransparentCutout");

                                        if ((m.IsKeywordEnabled("N_F_R_ON") && (m.IsKeywordEnabled("N_F_ESSR_ON") || m.GetFloat("_N_F_ESSR") == 1.0f)) || ((m.IsKeywordEnabled("N_F_ESSGI_ON") || m.GetFloat("_N_F_ESSGI") == 1.0f)))
                                        {
                                            m.SetInt("_SSRefDeOn", 8);
                                            m.SetInt("_SSRefGBu", 10);
                                            m.SetInt("_SSRefMoVe", 40);
                                        }

                                        m.SetInt("_ZTeForLiOpa", 3);
                                    }

                                    if (ShaderName != "HDRP/Unlit")
                                    {
                                        if (ShaNormalMap != null)
                                        {
                                            m.EnableKeyword("N_F_NM_ON");
                                            m.SetFloat("_N_F_NM", 1.0f);
                                            m.SetTexture("_NormalMap", ShaNormalMap);
                                            m.SetFloat("_NormalMapIntensity", ShaNormalScale);
                                        }
                                    }

                                    if (ShaColor != Color.white)
                                    {
                                        m.SetColor("_MainColor", ShaColor * ShaColor);
                                    }

                                    if (ShaDoubSid == 1.0f)
                                    {
                                        m.SetFloat("_Culling", 0);
                                    }
                                    else if (ShaDoubSid == 0.0f)
                                    {
                                        if (CullingMode != 2.0f)
                                        {
                                            m.SetFloat("_Culling", CullingMode);
                                        }
                                    }

                                    if (ShaderName != "HDRP/Unlit")
                                    {
                                        if (ShaEmiColor != Color.black)
                                        {
                                            m.EnableKeyword("N_F_SL_ON");
                                            m.SetFloat("_N_F_SL", 1.0f);
                                            m.SetFloat("_SelfLitPower", 50);
                                            m.SetFloat("_SelfLitIntensity", 1.0f);

                                            if (ShaEmiMap != null)
                                            {
                                                m.SetTexture("_MaskSelfLit", ShaEmiMap);
                                            }

                                            m.SetColor("_SelfLitColor", ShaEmiColor * ShaEmiColor);
                                        }
                                    }

                                    if (ShaderName != "HDRP/Unlit")
                                    {
                                        if (ShaSmooth >= 0.5)
                                        {
                                            m.EnableKeyword("N_F_GLO_ON");
                                            m.SetFloat("_N_F_GLO", 1.0f);
                                            m.SetFloat("_Glossiness", 0.6f);
                                        }
                                        else
                                        {
                                            m.DisableKeyword("N_F_GLO_ON");
                                            m.SetFloat("_N_F_GLO", 0.0f);
                                        }
                                    }

                                    if (ShaderName != "HDRP/Unlit")
                                    {

                                        if (ShaMatID == 4.0f)
                                        {
                                            if (ShaSpecMap != null)
                                            {
                                                m.SetTexture("_MaskGloss", ShaSpecMap);
                                            }

                                            m.SetColor("_GlossColor", ShaSpecCol * ShaSpecCol);
                                        }

                                        if (ShaMasMa != null)
                                        {
                                            m.SetTexture("_MaskGloss", ShaMasMa);
                                        }

                                    }

                                    if (ShaderName != "HDRP/Unlit")
                                    {
                                        if (ShaMetal != 0.0f && ShaMasMa == null)
                                        {
                                            m.EnableKeyword("N_F_R_ON");
                                            m.SetFloat("_N_F_R", 1.0f);
                                            m.SetFloat("_ReflectionIntensity", ShaMetal);
                                            m.SetFloat("_ReflectionRoughtness", ShaSmooth);
                                            m.SetFloat("_RefMetallic", 0.65f);

                                            if ((m.IsKeywordEnabled("N_F_R_ON") && m.IsKeywordEnabled("N_F_ESSR_ON")) || m.IsKeywordEnabled("N_F_ESSGI_ON"))
                                            {

                                                m.SetInt("_SSRefDeOn", 8);
                                                m.SetInt("_SSRefGBu", 10);
                                                m.SetInt("_SSRefMoVe", 40);

                                            }
                                            else if (!m.IsKeywordEnabled("N_F_R_ON"))
                                            {
                                                m.SetInt("_SSRefDeOn", 0);
                                                m.SetInt("_SSRefGBu", 2);
                                                m.SetInt("_SSRefMoVe", 32);
                                            }

                                            if (m.IsKeywordEnabled("N_F_TRANS_ON") && !m.IsKeywordEnabled("N_F_CO_ON"))
                                            {
                                                m.SetInt("_SSRefDeOn", 0);
                                                m.SetInt("_SSRefGBu", 2);
                                                m.SetInt("_SSRefMoVe", 32);
                                            }

                                        }
                                        else if (ShaMasMa != null)
                                        {
                                            ShaSmooth = ShaSmoRemMinMax;
                                            m.EnableKeyword("N_F_R_ON");
                                            m.SetFloat("_N_F_R", 1.0f);
                                            m.SetFloat("_ReflectionIntensity", ShaMetaRemMinMax);
                                            m.SetFloat("_ReflectionRoughtness", ShaSmooth);
                                            m.SetTexture("_MaskReflection", ShaMasMa);
                                            m.SetFloat("_RefMetallic", 0.65f);

                                            if ((m.IsKeywordEnabled("N_F_R_ON") && m.IsKeywordEnabled("N_F_ESSR_ON")) || m.IsKeywordEnabled("N_F_ESSGI_ON"))
                                            {

                                                m.SetInt("_SSRefDeOn", 8);
                                                m.SetInt("_SSRefGBu", 10);
                                                m.SetInt("_SSRefMoVe", 40);

                                            }
                                            else if (!m.IsKeywordEnabled("N_F_R_ON"))
                                            {
                                                m.SetInt("_SSRefDeOn", 0);
                                                m.SetInt("_SSRefGBu", 2);
                                                m.SetInt("_SSRefMoVe", 32);
                                            }

                                            if (m.IsKeywordEnabled("N_F_TRANS_ON") && !m.IsKeywordEnabled("N_F_CO_ON"))
                                            {
                                                m.SetInt("_SSRefDeOn", 0);
                                                m.SetInt("_SSRefGBu", 2);
                                                m.SetInt("_SSRefMoVe", 32);
                                            }
                                        }
                                    }

                                    if (ShaderName == "HDRP/Unlit")
                                    {
                                        m.EnableKeyword("N_F_SL_ON");
                                        m.SetFloat("_N_F_SL", 1.0f);
                                        m.SetFloat("_SelfLitPower", 6.5f);
                                        m.SetFloat("_SelfLitIntensity", 1.0f);
                                        m.SetFloat("_SelfLitHighContrast", 0.0f);

                                        m.DisableKeyword("N_F_SS_ON");
                                        m.SetFloat("_N_F_SS", 0.0f);

                                        m.DisableKeyword("N_F_RELGI_ON");
                                        m.SetFloat("_RELG", 0.0f);

                                        m.DisableKeyword("N_F_PAL_ON");
                                        m.SetFloat("_N_F_PAL", 0.0f);

                                        m.EnableKeyword("N_F_USETLB_ON");
                                        m.SetFloat("_UseTLB", 1.0f);

                                        m.EnableKeyword("N_F_HDLS_ON");
                                        m.SetFloat("_N_F_HDLS", 1.0f);

                                        m.EnableKeyword("N_F_HPSAS_ON");
                                        m.SetFloat("_N_F_HPSAS", 1.0f);
                                    }

                                    if (LigAffSha == true)
                                    {
                                        m.SetFloat("_LightAffectShadow", 1.0f);
                                    }

                                    ShaderName = string.Empty;
                                    InfoString += "\n[Done]\n\n";
                                }

                            }

                            if (UnShaType == "(Built-In)")
                            {

                                if (m.shader.name == "Standard" || m.shader.name == "Standard (Specular setup)")
                                {
                                    ShaderName = m.shader.name;
                                    ShaMainTex = m.GetTexture("_MainTex");
                                    ShaColor = m.GetColor("_Color");
                                    ShaNormalMap = m.GetTexture("_BumpMap");
                                    ShaNormalScale = m.GetFloat("_BumpScale");
                                    ShaEmiColor = m.GetColor("_EmissionColor");
                                    ShaEmiMap = m.GetTexture("_EmissionMap");
                                    ShaSmooth = m.GetFloat("_Glossiness");
                                    MatType = m.GetFloat("_Mode");
                                    SKEmi = m.IsKeywordEnabled("_EMISSION");
                                    ShaSpecHighEn = m.GetFloat("_SpecularHighlights");
                                    ShaRefEn = m.GetFloat("_GlossyReflections");

                                    if (m.HasProperty("_SpecGlossMap"))
                                    {
                                        ShaSpecMap = m.GetTexture("_SpecGlossMap");
                                    }

                                    if (m.HasProperty("_Metallic"))
                                    {
                                        ShaMetal = m.GetFloat("_Metallic");
                                    }

                                    if (m.HasProperty("_MetallicGlossMap"))
                                    {
                                        ShaMetaMap = m.GetTexture("_MetallicGlossMap");
                                    }

                                    if (m.HasProperty("_SpecColor"))
                                    {
                                        ShaSpecCol = m.GetColor("_SpecColor");
                                    }
                                }
                                else if (m.shader.name == "Unlit/Color" || m.shader.name == "Unlit/Texture" || m.shader.name == "Unlit/Transparent" || m.shader.name == "Unlit/Transparent Cutout")
                                {
                                    ShaderName = m.shader.name;

                                    if (m.HasProperty("_MainTex"))
                                    {
                                        ShaMainTex = m.GetTexture("_MainTex");
                                    }

                                    if (m.shader.name == "Unlit/Color")
                                    {
                                        ShaColor = m.GetColor("_Color");
                                    }
                                }
                                else if (m.shader.name != "Standard" || m.shader.name != "Standard (Specular setup)" || m.shader.name != "Unlit/Color" || m.shader.name != "Unlit/Texture" || m.shader.name != "Unlit/Transparent" || m.shader.name != "Unlit/Transparent Cutout")
                                {
                                    InfoString += "The selected '" + m.name + "' material, shader is not supported.\n '" + m.shader.name + "'\n\n" + SupShaBiRP;
                                }

                                if (m.shader.name == "Standard" || m.shader.name == "Standard (Specular setup)" || m.shader.name == "Unlit/Color" || m.shader.name == "Unlit/Texture" || m.shader.name == "Unlit/Transparent" || m.shader.name == "Unlit/Transparent Cutout")
                                {
                                    InfoString += "Processing Material: " + m.name + "\nPrevious Shader: " + ShaderName;
                                }

                                if (m.shader.name == "Standard" || m.shader.name == "Standard (Specular setup)")
                                {

                                    if (MatType == 0.0f || MatType == 1.0f)
                                    {
                                        m.shader = ShaRTBID;
                                    }
                                    else if (MatType == 2.0f || MatType == 3.0f)
                                    {
                                        m.shader = ShaRTBIFT;
                                        m.SetFloat("_Opacity", ShaColor.a);
                                    }

                                    m.SetFloat("_OutlineWidth", 0.2f);

                                    if (MatType == 1.0f)
                                    {
                                        m.EnableKeyword("N_F_CO_ON");
                                        m.SetFloat("_N_F_CO", 1.0f);
                                        m.SetFloat("_Cutout", 0.4f);
                                    }

                                    if (ShaMainTex != null)
                                    {
                                        m.SetTexture("_MainTex", ShaMainTex);
                                    }

                                    m.SetColor("_OverallShadowColor", new Color(0.2f, 0.2f, 0.2f));

                                    if (ShaNormalMap != null)
                                    {
                                        m.EnableKeyword("N_F_NM_ON");
                                        m.SetFloat("_N_F_NM", 1.0f);
                                        m.SetTexture("_NormalMap", ShaNormalMap);
                                        m.SetFloat("_NormalMapIntensity", ShaNormalScale);
                                    }

                                    if (ShaColor != Color.white)
                                    {
                                        m.SetColor("_MainColor", ShaColor);
                                    }

                                    if (SKEmi)
                                    {
                                        m.EnableKeyword("N_F_SL_ON");
                                        m.SetFloat("_N_F_SL", 1.0f);
                                        m.SetFloat("_SelfLitPower", 10);
                                        m.SetFloat("_SelfLitIntensity", 1.0f);
                                        m.SetTexture("_MaskSelfLit", ShaEmiMap);
                                        m.SetColor("_SelfLitColor", ShaEmiColor);
                                    }

                                    if (ShaSpecHighEn == 1.0f)
                                    {
                                        if (ShaSmooth >= 0.5)
                                        {
                                            m.EnableKeyword("N_F_GLO_ON");
                                            m.SetFloat("_N_F_GLO", 1.0f);
                                            m.SetFloat("_Glossiness", 0.6f);
                                        }
                                        else
                                        {
                                            m.DisableKeyword("N_F_GLO_ON");
                                            m.SetFloat("_N_F_GLO", 0.0f);
                                        }
                                    }

                                    if (ShaderName == "Standard (Specular setup)")
                                    {
                                        if (ShaSpecMap != null)
                                        {
                                            m.SetTexture("_MaskGloss", ShaSpecMap);
                                        }
                                        else
                                        {
                                            if (ShaSpecHighEn == 1.0f)
                                            {
                                                m.SetColor("_GlossColor", ShaSpecCol * ShaSpecCol);
                                            }
                                        }
                                    }

                                    if (ShaderName == "Standard")
                                    {
                                        if (ShaRefEn == 1.0f)
                                        {
                                            if (ShaMetal != 0.0f && ShaMetaMap == null)
                                            {
                                                m.EnableKeyword("N_F_R_ON");
                                                m.SetFloat("_N_F_R", 1.0f);
                                                m.SetFloat("_ReflectionIntensity", ShaMetal);
                                                m.SetFloat("_ReflectionRoughtness", 1.0f - ShaSmooth);
                                                m.SetFloat("_RefMetallic", 0.65f);
                                            }
                                            else if (ShaMetaMap != null)
                                            {
                                                m.EnableKeyword("N_F_R_ON");
                                                m.SetFloat("_N_F_R", 1.0f);
                                                m.SetFloat("_ReflectionIntensity", 1f);
                                                m.SetFloat("_ReflectionRoughtness", 1.0f - ShaSmooth);
                                                m.SetTexture("_MaskReflection", ShaMetaMap);
                                                m.SetFloat("_RefMetallic", 0.65f);
                                            }
                                        }
                                    }

                                    ShaderName = string.Empty;
                                    InfoString += "\n[Done]\n\n";

                                }

                                if (m.shader.name == "Unlit/Color" || m.shader.name == "Unlit/Texture" || m.shader.name == "Unlit/Transparent" || m.shader.name == "Unlit/Transparent Cutout")
                                {

                                    if (m.shader.name == "Unlit/Color" || m.shader.name == "Unlit/Texture" || m.shader.name == "Unlit/Transparent Cutout")
                                    {
                                        m.shader = ShaRTBID;
                                    }
                                    else if (m.shader.name == "Unlit/Transparent")
                                    {
                                        m.shader = ShaRTBIFT;
                                    }

                                    m.EnableKeyword("N_F_SL_ON");
                                    m.SetFloat("_N_F_SL", 1.0f);
                                    m.SetFloat("_SelfLitPower", 10);
                                    m.SetFloat("_SelfLitIntensity", 1.0f);

                                    m.DisableKeyword("N_F_SS_ON");
                                    m.SetFloat("_N_F_SS", 0.0f);

                                    m.DisableKeyword("N_F_RELGI_ON");
                                    m.SetFloat("_RELG", 0.0f);

                                    if (m.shader.name != "Unlit/Transparent")
                                    {
                                        m.EnableKeyword("N_F_HDLS_ON");
                                        m.SetFloat("_N_F_HDLS", 1.0f);

                                        m.EnableKeyword("N_F_HPSS_ON");
                                        m.SetFloat("_N_F_HPSS", 1.0f);
                                    }

                                    if (m.shader.name != "Unlit/Texture" || m.shader.name != "Unlit/Transparent" || m.shader.name != "Unlit/Transparent Cutout")
                                    {
                                        m.SetColor("_MainColor", ShaColor);
                                    }

                                    if (m.shader.name == "Unlit/Texture" || m.shader.name == "Unlit/Transparent" || m.shader.name == "Unlit/Transparent Cutout")
                                    {
                                        if (ShaMainTex != null)
                                        {
                                            m.SetTexture("_MainTex", ShaMainTex);
                                        }
                                    }

                                    if (m.shader.name == "Unlit/Transparent Cutout")
                                    {
                                        m.EnableKeyword("N_F_CO_ON");
                                        m.SetFloat("_N_F_CO", 1.0f);
                                        m.SetFloat("_Cutout", 0.4f);
                                    }

                                    if (LigAffSha == true)
                                    {
                                        m.SetFloat("_LightAffectShadow", 1.0f);
                                    }

                                    ShaderName = string.Empty;
                                    InfoString += "\n[Done]\n\n";

                                }

                            }

                        }

                        MatNum++;
                    }

                }

            }
            EditorGUI.EndDisabledGroup();
            #endregion

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Material: " + "[" + MatNum + ": " + ProcMat + "]");
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            #region Other Settings

            if (ToBaInt == 0)
            {
                GUILayout.Space(10);
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.BeginVertical();

                        FUL = GUILayout.Toggle(FUL, new GUIContent("Force Unlit", "This will disable all lighting and shadows,\nOnly Main Texture and Main Color are set."));
                        ForTrasCuto = GUILayout.Toggle(ForTrasCuto, "Force Transparent Material to Cutout");

                        EditorGUI.BeginDisabledGroup(FUL == true);
                        
                            EditorGUI.BeginDisabledGroup(ShaRTBID && PlayerSettings.colorSpace == ColorSpace.Gamma);
                                EnhaHiLighColInt = GUILayout.Toggle(EnhaHiLighColInt, new GUIContent("Enhance Light Highlight Color Intensity", "Not available if project color space is Gamma."));
                            EditorGUI.EndDisabledGroup();
                        
                            IncShaCol = GUILayout.Toggle(IncShaCol, "Include Shade/Shadow Color");
                            LigAffSha = GUILayout.Toggle(LigAffSha, new GUIContent("Light Affect Shadows", "Light's intensity and color will affect shadows.\nIf not enabled, The light will not affect the shadow and it will prevent overexpose shadow color when there are more lights on the scene and high intensity light value."));
                            DisRecSha = GUILayout.Toggle(DisRecSha, new GUIContent("Disable Received Shadows", "This will disable received shadows from other objects including received self cast shadows."));
                        
                            EditorGUI.BeginDisabledGroup(UsEmiMapAnColAsGloTex == true);
                                IncEmi = GUILayout.Toggle(IncEmi, "Include Emission");
                            EditorGUI.EndDisabledGroup();

                            EditorGUI.BeginDisabledGroup(IncEmi == true);
                                UsEmiMapAnColAsGloTex = GUILayout.Toggle(UsEmiMapAnColAsGloTex, new GUIContent("Use Emission Map And Color As Gloss", "Mostly useful for hair materials.\nThis will use the VRoid Emission map as a gloss and use the selected Emission color."));
                            EditorGUI.EndDisabledGroup();

                            FERL = GUILayout.Toggle(FERL, "Force Enable Rim Light And Use White Color");

                            EditorGUILayout.BeginHorizontal();
                                EnaGiSha = GUILayout.Toggle(EnaGiSha, "Enable Global Illumination Shade");

                                EditorGUI.BeginDisabledGroup(EnaGiSha == false);
                                    GiFlaLo = GUILayout.Toggle(GiFlaLo, new GUIContent("Global Illumination Flat Shade", "This will make the Global Illumination shade into flat/cel shade."));
                                EditorGUI.EndDisabledGroup();

                            EditorGUILayout.EndHorizontal();

                        EditorGUI.EndDisabledGroup();

                    EditorGUILayout.EndVertical();

                EditorGUILayout.EndHorizontal();
            }
            else if (ToBaInt == 1)
            {
                GUILayout.Space(10);
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(135);
                    LigAffSha = GUILayout.Toggle(LigAffSha, new GUIContent("Light Affect Shadows", "Light's intensity and color will affect shadows.\nIf not enabled, The light will not affect the shadow,\nthis will also prevent overexpose shadow color when there are more lights on the scene."));
                EditorGUILayout.EndHorizontal();
            }

            #endregion

            #region Info

            GUILayout.Space(10);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Info:");

            EditorGUILayout.BeginVertical("TextArea", GUILayout.Height(90));
            scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.Height(120));
            GUILayout.TextArea(InfoString);
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

            GUILayout.Space(10);
            GUILayout.Label(mat.Length.ToString() + " Selected Materials.");

            GUILayout.Space(10);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            GUILayout.Label("Note:");
            GUILayout.Label("*This tool supports RealToon Built-In, URP and HDRP shaders.\n" +
                "*Supported Unity Shader to swap are Built-In, URP and HDRP shaders.\n" +
                "*Read the included  documentation/user guide for more info.\n" +
                "*After the swap, click the 'Refresh Settings' on the RealToon Inspector.\n" +
                " -This will refresh and re-apply the settings properly. (URP/HDRP)");

            #endregion
        }

    }

}
