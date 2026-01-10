#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShaderBuild : IPreprocessShaders,IPreprocessBuildWithReport,IProcessSceneWithReport
{

    public int callbackOrder { get; }
    public void OnProcessScene(Scene scene, BuildReport report)
    {
    }

    public void OnPreprocessBuild(BuildReport report)
    {
        
    }

    public void OnProcessShader(Shader shader, ShaderSnippetData snippet, IList<ShaderCompilerData> data)
    {
        
    }
}

#endif
