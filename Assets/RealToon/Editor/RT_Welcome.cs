//MJQStudioWorks

using UnityEditor;
using System.IO;

namespace RealToon.Editor.Welcome
{

    [InitializeOnLoad]
    class RT_Welcome
    {

        #region RT_Welcome
        static readonly string rt_welcome_settings = "Assets/RealToon/Editor/RTW.sett";

        static RT_Welcome()
        {
            if (File.Exists(rt_welcome_settings))
            {
                if (File.ReadAllText(rt_welcome_settings) == "0")
                {
                    if (File.Exists(rt_welcome_settings))
                    {
                        EditorApplication.delayCall += Run_Welcome;
                    }
                }
            }
        }

        static void Run_Welcome()
        {

            if (EditorUtility.DisplayDialog(

               "Thank you for purchasing and using RealToon Shader",

               "*Before you start using RealToon, please read first the 'ReadMe - Important - Guide.txt' text file for setups and infos.\n\n" +

               "*All shaders are in the folder 'RealToon Shader Packages', just unpack the 'RealToon Shader' that correspond to your projects render pipeline.\n\n" +

               "*If you are a VRoid user, read the 'For VRoid-VRM users.txt' text file.\n\n" +

               "*For video tutorials and user guide, see the bottom part of RealToon Inspector panel.\n\n" +

               "*If you need some help/support, just send an email including the invoice number.\n" +
               "See the 'User Guide.pdf' file for the links and email support.\n\n" +

               "*PlayStation support is currently for URP and HDRP only.\n\n" +

               "Note:\nDon't move the 'RealToon' folder to other folder, it should stay in the root folder 'Asset'."

               ,

               "Ok") )
               //
            {

                File.WriteAllText(rt_welcome_settings, "1");
                AssetDatabase.Refresh();

            }

        }

        #endregion

    }

}