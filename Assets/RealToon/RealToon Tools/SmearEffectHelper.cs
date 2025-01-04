//RealToon - Smear Effect [Helper]
//MJQStudioWorks
//©2024

using UnityEngine;
using System.Collections.Generic;

namespace RealToon.Script
{

    [ExecuteAlways]
    [AddComponentMenu("RealToon/Tools/Smear Effect - Helper")]

    public class SmearEffectHelper : MonoBehaviour
    {
        Queue<Vector3> recentPositions = new Queue<Vector3>();

        [HideInInspector]
        [SerializeField]
        Transform[] SubTran;

        [HideInInspector]
        [SerializeField]
        Material[] Mat;

        [HideInInspector]
        [SerializeField]
        Transform[] attac;

        [Header("Note: Smear Effect feature will be automatically enable\nOn the materials of the object/model that uses RealToon Shader.")]

        [Space(25)]

        [SerializeField]
        [Tooltip("An object to control the smear effect.")]
        public Transform SmearController;

        [Space(10)]

        [SerializeField]
        [Tooltip("How long the distorted line trails stays on the previous position.")]
        int Delay = 15;

        [SerializeField]
        [Tooltip("How large/small the trailing noise.")]
        float NoiseSize = 100;

        [SerializeField]
        [Tooltip("How tall/short the trailing noise.")]
        float TrailSize = 1.5f;

        [Space(10)]

        [SerializeField]
        [Tooltip("Pause the current smear effect.")]
        bool PauseSmear = false;

        int coun_obj_wi_ralsha = 0;
        int coun_obj_mat = 0;
        int coun_obj_mat_arr = 0;

        [HideInInspector]
        [SerializeField]
        bool checkstart = true;

        string RT_Sha_Nam_URP = "Universal Render Pipeline/RealToon/Version 5/Default/Default";
        string RT_Sha_Nam_HDRP = "HDRP/RealToon/Version 5/Default";

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
            if (SmearController != null)
            {
                if (PauseSmear != true)
                {
                    if (Mat != null)
                    {
                        foreach (Material mate in Mat)
                        {
                            if (mate != null)
                            {
                                mate.SetVector("_ObjPosi", SmearController.position);
                                recentPositions.Enqueue(SmearController.position);

                                if (recentPositions.Count > Delay)
                                    mate.SetVector("_PrevPosition", recentPositions.Dequeue());

                                Set_Shad_Prop(mate);

                            }

                        }
                    }
                }
            }
        }
        void Reset()
        {
            if (Mat != null)
            {
                foreach (Material mate in Mat)
                {
                    if (mate != null)
                    {
                        mate.SetVector("_ObjPosi", new Vector4(0, 0, 0, 0));
                        mate.SetVector("_PrevPosition", new Vector4(0, 0, 0, 0));
                    }
                }
                recentPositions.Dequeue();
                recentPositions.Clear();
                checkstart = true;
                coun_obj_wi_ralsha = 0;
                coun_obj_mat = 0;
                coun_obj_mat_arr = 0;
                Res_Shad_Prop();
                InitStart();
                checkstart = false;
            }
        }

        void OnDisable()
        {
            recentPositions.Dequeue();
            foreach (Material mate in Mat)
            {
                if (mate != null)
                {
                    mate.SetVector("_ObjPosi", new Vector4(0, 0, 0, 0));
                    mate.SetVector("_PrevPosition", new Vector4(0, 0, 0, 0));
                }
            }
        }

        /* Remove Later
        void OnDestroy()
        {
            recentPositions.Clear();
            Res_Shad_Prop();
            foreach (Material Mate in Mat)
            {
                if (Mate != null)
                {
                    if (Mate.shader.name == RT_Sha_Nam_URP || Mate.shader.name == RT_Sha_Nam_HDRP)
                    {
                        Mate.SetVector("_ObjPosi", new Vector4(0, 0, 0, 0));
                        Mate.SetVector("_PrevPosition", new Vector4(0, 0, 0, 0));
                        Mate.SetFloat("_N_F_SE", 0.0f);
                        Mate.DisableKeyword("N_F_SE_ON");
                    }
                }
            }
        }
        */

        #region Init

        void InitStart()
        {
            if (attac == null || attac.Length == 0)
            {
                attac = this.gameObject.GetComponentsInChildren<Transform>();
            }

            if (SmearController == null)
            {
                SmearController = this.gameObject.transform;
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
            if (Mat.IsKeywordEnabled("N_F_SE_ON") == true)
            {
                Mat.SetFloat("_NoiseSize", NoiseSize);
                Mat.SetFloat("_TrailSize", TrailSize);
            }
            else if (Mat.IsKeywordEnabled("N_F_SE_ON") != true)
            {
                Mat.EnableKeyword("N_F_SE_ON");
                Mat.SetInt("_N_F_SE", 1);
            }
        }
        void Res_Shad_Prop()
        {
            NoiseSize = 100;
            TrailSize = 1.5f;
            Delay = 15;
        }

    }

}