//RealToon - Frame By Frame Rendering V1.0.0
//MJQStudioWorks
//©2018

using UnityEngine;
using System.Collections;

namespace RealToon.Tools.FrameByFrameRendering
{
    [AddComponentMenu("RealToon/Tools/Frame By Frame Rendering/Frame By Frame Rendering (Manual)")]
    public class FrameByFrameRendering_Manual : MonoBehaviour
    {
        [Header("(Frame By Frame Rendering V1.0.0)")]
        [Header("Click 'Play' & 'Render' button to start render one by one.")]

        [Space(20)]

        [SerializeField]
        [Tooltip("Current Frame Number")]
        public int FrameNumber = 0;

        [Space(5)]

        [SerializeField]
        [Tooltip("Start Render")]
        public bool Render = false;

        [Header("==============================")]
        [Header("(Settings)")]

        [Space(5)]

        [SerializeField]
        [Tooltip("Example Path: C:/TheNameOfTheFolder (Default folder name is Rendered Files and it will be created to your unity root project folder if this set to empty.")]
        public string PathFolder = "Rendered Files";

        [SerializeField]
        [Tooltip("PNG File Name")]
        public string PNGFileName = "Frame";

        [Space(15)]

        [SerializeField]
        [Tooltip("Render single frame or single image only, For Illustration or Art use.")]
        public bool PictureMode = false;

        [Header("==============================")]

        [Header("(Information [Display Only] )")]

        [Space(5)]

        [Tooltip("Display the information of the operation or rendering. (Display Only)")]
        public int LastRenderedFrame = 0;
        public string info = string.Empty;

        private string CurrentRenderedFile = string.Empty;
        private string PathFolderCont = "Rendered Files";
        private string PNGFileNameCont = "Frame";
        private int FrameNumberCont = 0;
        private bool PictureModeCont = false;
        private bool PreventRender = false;

        private System.IO.DirectoryInfo DirInfo;


        void Start()
        {

            if (PNGFileName == string.Empty)
            {
                PNGFileName = "Frame";
                info = "File Name set to 'Frame' because the field is not set or empty.";
                Debug.LogError(info);
            }

            if (PathFolder == string.Empty)
            {
                PathFolder = "Rendered Files";
                info = "Folder Path set to 'Rendered Files' and will be created to your UNITY ROOT PROJECT FOLDER because the field is not set or empty.";
                Debug.LogError(info);
            }

            PictureModeCont = PictureMode;
            PathFolderCont = PathFolder;
            PNGFileNameCont = PNGFileName;
            FrameNumberCont = FrameNumber;

            DirInfo = new System.IO.DirectoryInfo(PathFolder);

            if (!System.IO.Directory.Exists(PathFolder))
            {
                System.IO.Directory.CreateDirectory(PathFolder);
                info = "Folder '" + PathFolder + "' Has Been Created To Your Root Project Folder.";
                Debug.LogWarning(info);
            }


            if (PictureMode == false)
            {
                info = "Frame by Frame Rendering Mode";
                Debug.LogWarning(info);

                if (DirInfo.GetFiles().Length != 0)
                {
                    PreventRender = true;
                    Render = false;
                    info = "(Frame by Frame Mode) Rendering not started because there are already rendered frames or files in this folder ('" + PathFolder + "'), Please empty this folder or make another folder by changing the Path Folder.";
                    Debug.LogError(info);
                }
            }
            else
            {
                info = "Picture or Single Frame Rendering Mode";
                Debug.LogWarning(info);
            }

        }

        void Update()
        {
            PictureMode = PictureModeCont;
            PathFolder = PathFolderCont;
            PNGFileName = PNGFileNameCont;
            LastRenderedFrame = FrameNumberCont;

            if (FrameNumber <= 0)
            {
                FrameNumber = FrameNumberCont;
            }

            if (PreventRender == false)
            {
                if (Render == true)
                {
                    if (PictureMode == false)
                    {
                        string fname = string.Format("{0}/" + PNGFileNameCont + " {1:D04}.png", PathFolderCont, FrameNumber);
                        CurrentRenderedFile = fname;

#if UNITY_2017_1_OR_NEWER
                        ScreenCapture.CaptureScreenshot(fname);
#endif

#if UNITY_5_6
                    Application.CaptureScreenshot(fname);
#endif


                        FrameNumber += 1;
                        FrameNumberCont = FrameNumber;

                        info = CurrentRenderedFile;
                        Debug.LogWarning(info);
                        Render = false;
                    }
                    else
                    {
                        string fname = string.Format("{0}/" + PNGFileNameCont + " " + System.DateTime.Now.ToString("hh_mm_ss") + ".png", PathFolderCont, FrameNumber);
                        CurrentRenderedFile = fname;

#if UNITY_2017_1_OR_NEWER
                        ScreenCapture.CaptureScreenshot(fname);
#endif

#if UNITY_5_6
                    Application.CaptureScreenshot(fname);
#endif
                        info = CurrentRenderedFile;
                        Debug.LogWarning(info);
                        Render = false;
                    }
                }
            }
        }
    }

}