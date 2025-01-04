//RealToon - Perspective Adjustment [Helper]
//MJQStudioWorks
//©2024

using UnityEngine;

namespace RealToon.Script
{

    [ExecuteAlways]
    [AddComponentMenu("RealToon/Tools/Perspective Adjustment - Controller")]
    public class PerspectiveAdjustmentController : MonoBehaviour
    {
        [HideInInspector]
        [SerializeField]
        Transform[] SubTran;

        [HideInInspector]
        [SerializeField]
        Material[] Mat;

        [HideInInspector]
        [SerializeField]
        Transform[] attac;

        [Header("Note: Perspective Adjustment feature will be automatically enable\nOn the materials of the object/model that uses RealToon Shader.")]
        [Header("It is recommend to set the Clipping Planes - Near of the camera\nto 0.01 to prevent slicing when near.")]

        [Space(25)]

        [SerializeField]
        [Tooltip("This will change the perspective of an object to 2D or 3D or FOV stretch look.\nFor 2d toon/anime look, set it to 0.5 or 0.")]
        public float Perspective = 1;

        [SerializeField]
        [Tooltip("This will change the clipping on the object.\nChange this if the object is overlapping front or back.")]
        public float Clip = 1;

        [Space(15)]

        [SerializeField]
        [Tooltip("This will adjust the size of the object when the camera is closer.")]
        public float CloseUpSize = 0;

        [SerializeField]
        [Tooltip("How smooth the transition of the sizing.")]
        public float CloseUpSizeSmoothTransition = 1;

        [SerializeField]
        [Tooltip("Distance transition from the camera to the object.")]
        public float CloseUpSizeDistance = 0;

        [Space(15)]

        [SerializeField]
        [Tooltip("Can animate the values/properties or create keyframe.")]
        public bool CanAnimateIt = true;

        int coun_obj_wi_ralsha = 0;
        int coun_obj_mat = 0;
        int coun_obj_mat_arr = 0;

        string RT_Sha_Nam_URP = "Universal Render Pipeline/RealToon/Version 5/Default/Default";
        string RT_Sha_Nam_HDRP = "HDRP/RealToon/Version 5/Default";

        [HideInInspector]
        [SerializeField]
        bool checkstart = true;

        void Start()
        {
            if (checkstart == true)
            {
                InitStart();
                checkstart = false;
            }
        }

        void LateUpdate()
        {
            if (Mat != null)
            {
                if (CanAnimateIt == true)
                {
                    foreach (Material Mate in Mat)
                    {
                        if (Mate != null)
                        {
                            if (Mate.shader.name == RT_Sha_Nam_URP || Mate.shader.name == RT_Sha_Nam_HDRP)
                            {
                                Set_Shad_Prop(Mate);
                            }
                        }
                    }
                }
            }
        }

        void OnValidate()
        {
            if (Mat != null)
            {
                foreach (Material Mate in Mat)
                {
                    if (Mate != null)
                    {
                        if (Mate.shader.name == RT_Sha_Nam_URP || Mate.shader.name == RT_Sha_Nam_HDRP)
                        {
                            Set_Shad_Prop(Mate);
                        }
                    }
                }
            }
        }

        /* Remove Later
        void OnDestroy()
        {
            Res_Shad_Prop();
            foreach (Material Mate in Mat)
            {
                if (Mate != null)
                {
                    if (Mate.shader.name == RT_Sha_Nam_URP || Mate.shader.name == RT_Sha_Nam_HDRP)
                    {
                        Mate.SetFloat("_N_F_PA", 0.0f);
                        Mate.DisableKeyword("N_F_PA_ON");
                    }
                }
            }
        }
        */

        void Reset()
        {
            if (Mat != null)
            {
                checkstart = true;
                Res_Shad_Prop();

                foreach (Material Mate in Mat)
                {
                    if (Mate != null)
                    {
                        if (Mate.shader.name == RT_Sha_Nam_URP || Mate.shader.name == RT_Sha_Nam_HDRP)
                        {
                            Set_Shad_Prop(Mate);
                        }
                    }
                }

                coun_obj_wi_ralsha = 0;
                coun_obj_mat = 0;
                coun_obj_mat_arr = 0;
                InitStart();

            }
        }

        #region Init

        void InitStart()
        {
            if (attac == null || attac.Length == 0)
            {
                attac = this.gameObject.GetComponentsInChildren<Transform>();
            }

            int x = 0;
            foreach (Transform Trans in attac)
            {

                if (Trans.GetComponent<SkinnedMeshRenderer>() == true)
                {
                    if (Trans.GetComponent<SkinnedMeshRenderer>().sharedMaterial != null)
                    {
                        if (Trans.GetComponent<SkinnedMeshRenderer>().sharedMaterial.shader.name == RT_Sha_Nam_URP || Trans.GetComponent<SkinnedMeshRenderer>().sharedMaterial.shader.name == RT_Sha_Nam_HDRP)
                        {
                            coun_obj_wi_ralsha++;
                            coun_obj_mat += Trans.GetComponent<SkinnedMeshRenderer>().sharedMaterials.Length;
                        }
                    }

                }

                if (Trans.GetComponent<MeshRenderer>() == true)
                {
                    if (Trans.GetComponent<MeshRenderer>().sharedMaterial != null)
                    {
                        if (Trans.GetComponent<MeshRenderer>().sharedMaterial.shader.name == RT_Sha_Nam_URP || Trans.GetComponent<MeshRenderer>().sharedMaterial.shader.name == RT_Sha_Nam_HDRP)
                        {
                            coun_obj_wi_ralsha++;
                            coun_obj_mat += Trans.GetComponent<MeshRenderer>().sharedMaterials.Length;
                        }

                    }
                }

            }

            SubTran = new Transform[coun_obj_wi_ralsha];

            foreach (Transform Trans in attac)
            {
                if (Trans.GetComponent<SkinnedMeshRenderer>() == true)
                {
                    if (Trans.GetComponent<SkinnedMeshRenderer>().sharedMaterial != null)
                    {
                        if (Trans.GetComponent<SkinnedMeshRenderer>().sharedMaterial.shader.name == RT_Sha_Nam_URP || Trans.GetComponent<SkinnedMeshRenderer>().sharedMaterial.shader.name == RT_Sha_Nam_HDRP)
                        {
                            SubTran[x] = Trans;
                            x++;
                        }

                    }

                }

                if (Trans.GetComponent<MeshRenderer>() == true)
                {
                    if (Trans.GetComponent<MeshRenderer>().sharedMaterial != null)
                    {
                        if (Trans.GetComponent<MeshRenderer>().sharedMaterial.shader.name == RT_Sha_Nam_URP || Trans.GetComponent<MeshRenderer>().sharedMaterial.shader.name == RT_Sha_Nam_HDRP)
                        {
                            SubTran[x] = Trans;
                            x++;
                        }
                    }
                }
            }


            Mat = new Material[coun_obj_mat];


            foreach (Transform Trans in SubTran)
            {
                if (Trans.GetComponent<SkinnedMeshRenderer>() == true)
                {
                    if (Trans.GetComponent<SkinnedMeshRenderer>().sharedMaterial != null)
                    {
                        if (Trans.GetComponent<SkinnedMeshRenderer>().sharedMaterial.shader.name == RT_Sha_Nam_URP || Trans.GetComponent<SkinnedMeshRenderer>().sharedMaterial.shader.name == RT_Sha_Nam_HDRP)
                        {
                            for (int i = 0; i < Trans.GetComponent<SkinnedMeshRenderer>().sharedMaterials.Length; i++)
                            {
                                Mat[coun_obj_mat_arr] = Trans.GetComponent<SkinnedMeshRenderer>().sharedMaterials[i];
                                coun_obj_mat_arr++;
                            }
                        }

                    }

                }

                if (Trans.GetComponent<MeshRenderer>() == true)
                {
                    if (Trans.GetComponent<MeshRenderer>().sharedMaterial != null)
                    {
                        if (Trans.GetComponent<MeshRenderer>().sharedMaterial.shader.name == RT_Sha_Nam_URP || Trans.GetComponent<MeshRenderer>().sharedMaterial.shader.name == RT_Sha_Nam_HDRP)
                        {

                            for (int i = 0; i < Trans.GetComponent<MeshRenderer>().sharedMaterials.Length; i++)
                            {
                                Mat[coun_obj_mat_arr] = Trans.GetComponent<MeshRenderer>().sharedMaterials[i];
                                coun_obj_mat_arr++;
                            }
                        }
                    }
                }

            }

            foreach (Material Mat in Mat)
            {
                if (Mat != null)
                {
                    Set_Shad_Prop(Mat);
                }
            }
        }

        #endregion

        void Set_Shad_Prop(Material Mat)
        {
            if (Mat.IsKeywordEnabled("N_F_PA_ON") == true)
            {
                Mat.SetFloat("_PresAdju", Perspective);
                Mat.SetFloat("_ClipAdju", Clip);
                Mat.SetFloat("_PASize", CloseUpSize);
                Mat.SetFloat("_PASmooTrans", CloseUpSizeSmoothTransition);
                Mat.SetFloat("_PADist", CloseUpSizeDistance);
            }
            else if (Mat.IsKeywordEnabled("N_F_PA_ON") != true)
            {
                Mat.EnableKeyword("N_F_PA_ON");
                Mat.SetInt("_N_F_PA", 1);
            }
        }

        void Res_Shad_Prop()
        {
            Perspective = 1.0f;
            Clip = 1.0f;
            CloseUpSize = 0.0f;
            CloseUpSizeSmoothTransition = 1.0f;
            CloseUpSizeDistance = 0.0f;
        }

    }

}